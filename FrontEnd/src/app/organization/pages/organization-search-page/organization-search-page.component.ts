import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, startWith, map } from 'rxjs';
import { OrganizationSearchResult } from '../../models/organization.model';
import { OrganizationService } from '../../services/organization.service';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import {
  ConfirmationDialogComponent,
  ConfirmationDialogData,
} from '../../../shared/components/confirmation-dialog/confirmation-dialog.component';
import {
  TableColumn,
  TableAction,
} from '../../../shared/components/reusable-table/reusable-table.component';

@Component({
  selector: 'app-organization-search-page',
  standalone: false,
  templateUrl: './organization-search-page.component.html',
  styleUrls: ['./organization-search-page.component.scss'],
})
export class OrganizationSearchPageComponent implements OnInit {
  searchForm!: FormGroup;
  countryOptions: string[] = [];
  filteredCountryOptions$!: Observable<string[]>;
  results: OrganizationSearchResult[] = [];
  loading = false;

  tableColumns: TableColumn<OrganizationSearchResult>[] = [
    { key: 'code', header: 'Code', sortable: true },
    { key: 'name', header: 'Organization Name', sortable: true },
    { key: 'phone', header: 'Phone', sortable: true },
  ];

  tableActions: TableAction<OrganizationSearchResult>[] = [
    {
      icon: 'edit',
      label: 'Edit',
      type: 'edit',
      emit: (row) => this.editOrganization(row),
    },
    {
      icon: 'delete',
      label: 'Delete',
      type: 'delete',
      emit: (row) => this.deleteOrganization(row),
    },
  ];

  constructor(
    private fb: FormBuilder,
    private organizationService: OrganizationService,
    private snackbar: SnackbarService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      name: [''],
      code: [''],
      country: [''],
    });
    this.organizationService.getCountries().subscribe({
      next: (c) => {
        this.countryOptions = ['All Countries', ...c];
        this.setupCountryFilter();
      },
      error: () => {},
    });
    this.search();
  }

  private setupCountryFilter(): void {
    this.filteredCountryOptions$ = this.searchForm.get('country')!.valueChanges.pipe(
      startWith(''),
      map((value) => this.filterCountries(value ?? ''))
    );
  }

  private filterCountries(value: string): string[] {
    const lower = value.toLowerCase();
    return this.countryOptions.filter((opt) =>
      opt.toLowerCase().includes(lower)
    );
  }

  search(): void {
    this.loading = true;
    this.results = [];
    const filters = this.searchForm.value;
    this.organizationService.search(filters).subscribe({
      next: (organizations) => {
        this.results = organizations;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackbar.error('Search failed');
      },
    });
  }

  editOrganization(org: OrganizationSearchResult): void {
    this.router.navigate(['/organizations', 'edit', org.id]);
  }

  deleteOrganization(org: OrganizationSearchResult): void {
    const data: ConfirmationDialogData = {
      title: 'Delete Organization',
      message: `Are you sure you want to delete "${org.name}"?`,
      confirmLabel: 'Delete',
    };
    const ref = this.dialog.open(ConfirmationDialogComponent, {
      data,
      width: '400px',
    });
    ref.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.organizationService.delete(org.id).subscribe({
          next: () => {
            this.snackbar.success('Organization deleted');
            this.search();
          },
          error: () => this.snackbar.error('Delete failed'),
        });
      }
    });
  }
}
