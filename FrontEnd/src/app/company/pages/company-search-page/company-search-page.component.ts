import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, startWith, map } from 'rxjs';
import { CompanySearchResult } from '../../models/company.model';
import { CompanyService } from '../../services/company.service';
import { OrganizationService } from '../../../organization/services/organization.service';
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
  selector: 'app-company-search-page',
  standalone: false,
  templateUrl: './company-search-page.component.html',
  styleUrls: ['./company-search-page.component.scss'],
})
export class CompanySearchPageComponent implements OnInit {
  searchForm!: FormGroup;
  organizationOptions: string[] = [];
  countryOptions: string[] = [];
  filteredOrgOptions$!: Observable<string[]>;
  filteredCountryOptions$!: Observable<string[]>;
  results: CompanySearchResult[] = [];
  loading = false;

  tableColumns: TableColumn<CompanySearchResult>[] = [
    { key: 'organizationName', header: 'Organization Name', sortable: true },
    { key: 'companyCode', header: 'Code', sortable: true },
    { key: 'companyName', header: 'Company Name', sortable: true },
    { key: 'phone', header: 'Phone', sortable: true },
  ];

  tableActions: TableAction<CompanySearchResult>[] = [
    {
      icon: 'edit',
      label: 'Edit',
      type: 'edit',
      emit: (row) => this.editCompany(row),
    },
    {
      icon: 'delete',
      label: 'Delete',
      type: 'delete',
      emit: (row) => this.deleteCompany(row),
    },
  ];

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private organizationService: OrganizationService,
    private snackbar: SnackbarService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      organizationName: [''],
      companyName: [''],
      country: [''],
      companyCode: [''],
    });
    this.companyService.getOrganizations().subscribe((orgs) => {
      this.organizationOptions = orgs.map((o) => o.name);
      this.filteredOrgOptions$ = this.searchForm.get('organizationName')!.valueChanges.pipe(
        startWith(''),
        map((v) => this.filterList(this.organizationOptions, v))
      );
    });
    this.search();
    this.organizationService.getCountries().subscribe({
      next: (c) => {
        this.countryOptions = ['All Countries', ...c];
        this.filteredCountryOptions$ = this.searchForm.get('country')!.valueChanges.pipe(
          startWith(''),
          map((v) => this.filterList(this.countryOptions, v))
        );
      },
      error: () => {},
    });
  }

  search(): void {
    this.loading = true;
    this.results = [];
    const filters = this.searchForm.value;
    this.companyService.search(filters).subscribe({
      next: (companies) => {
        this.results = companies;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.snackbar.error('Search failed');
      },
    });
  }

  editCompany(company: CompanySearchResult): void {
    this.router.navigate(['/companies', 'edit', company.id]);
  }

  private filterList(options: string[], value: string | null): string[] {
    const lower = (value ?? '').toLowerCase();
    return options.filter((opt) => opt.toLowerCase().includes(lower));
  }

  deleteCompany(company: CompanySearchResult): void {
    const data: ConfirmationDialogData = {
      title: 'Delete Company',
      message: `Are you sure you want to delete "${company.companyName}"?`,
      confirmLabel: 'Delete',
    };
    const ref = this.dialog.open(ConfirmationDialogComponent, {
      data,
      width: '400px',
    });
    ref.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.companyService.delete(company.id).subscribe({
          next: () => {
            this.snackbar.success('Company deleted');
            this.search();
          },
          error: () => this.snackbar.error('Delete failed'),
        });
      }
    });
  }
}
