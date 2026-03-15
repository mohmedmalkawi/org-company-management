import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, startWith, map } from 'rxjs';
import { CompanyService } from '../../services/company.service';
import { OrganizationService } from '../../../organization/services/organization.service';
import { SnackbarService } from '../../../shared/services/snackbar.service';
import {
  ConfirmationDialogComponent,
  ConfirmationDialogData,
} from '../../../shared/components/confirmation-dialog/confirmation-dialog.component';

interface OrgOption {
  id: string;
  name: string;
}

@Component({
  selector: 'app-add-company-page',
  standalone: false,
  templateUrl: './add-company-page.component.html',
  styleUrls: ['./add-company-page.component.scss'],
})
export class AddCompanyPageComponent implements OnInit {
  form!: FormGroup;
  organizationOptions: OrgOption[] = [];
  countryOptions: string[] = [];
  filteredCountryOptions$!: Observable<string[]>;
  editId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private organizationService: OrganizationService,
    private snackbar: SnackbarService,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.editId = this.route.snapshot.paramMap.get('id');
    this.companyService.getOrganizations().subscribe((orgs) => {
      this.organizationOptions = orgs;
    });
    this.organizationService.getCountries().subscribe({
      next: (countries) => {
        this.countryOptions = countries;
        this.filteredCountryOptions$ = this.form.get('country')!.valueChanges.pipe(
          startWith(''),
          map((value) => {
            const lower = (value ?? '').toLowerCase();
            return this.countryOptions.filter((opt) => opt.toLowerCase().includes(lower));
          })
        );
      },
      error: () => {},
    });
    if (this.editId) {
      this.companyService.getById(this.editId).subscribe((company) => {
        if (company) {
          this.form.patchValue({
            organizationId: company.organizationId,
            name: company.name,
            code: company.code,
            country: company.country,
            phone: company.phone,
            fullAddress: company.fullAddress,
            creationDate: company.creationDate?.slice(0, 16),
            updatingDate: company.updatingDate?.slice(0, 16),
          });
        }
      });
    }
  }

  private buildForm(): void {
    this.form = this.fb.group({
      organizationId: ['', Validators.required],
      name: ['', Validators.required],
      code: ['', Validators.required],
      country: ['', Validators.required],
      phone: ['', Validators.required],
      fullAddress: ['', Validators.required],
      creationDate: [{ value: '', disabled: true }],
      updatingDate: [{ value: '', disabled: true }],
    });
  }

  getOrganizationDetails(): void {
    const orgId = this.form.get('organizationId')?.value;
    if (!orgId) return;
    const hasData =
      this.form.get('phone')?.value ||
      this.form.get('country')?.value ||
      this.form.get('fullAddress')?.value;
    if (hasData) {
      const data: ConfirmationDialogData = {
        title: 'Override company details',
        message:
          'The system will override all company details according to the predefined information on the organization level, Are you sure you want to continue ?',
        confirmLabel: 'Fill organization details',
        cancelLabel: 'Cancel',
      };
      const ref = this.dialog.open(ConfirmationDialogComponent, {
        data,
        width: '480px',
      });
      ref.afterClosed().subscribe((confirmed) => {
        if (confirmed) this.fillOrganizationDetails(orgId);
      });
    } else {
      this.fillOrganizationDetails(orgId);
    }
  }

  private fillOrganizationDetails(organizationId: string): void {
    this.companyService.getOrganizationDetails(organizationId).subscribe({
      next: (details) => {
        if (details) {
          this.form.patchValue({
            phone: details.phone,
            country: details.country,
            fullAddress: details.fullAddress,
          });
        }
      },
      error: () => this.snackbar.error('Could not load organization details'),
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const raw = this.form.getRawValue();
    const payload = {
      organizationId: raw.organizationId,
      name: raw.name,
      code: raw.code,
      country: raw.country,
      phone: raw.phone,
      fullAddress: raw.fullAddress,
    };
    if (this.editId) {
      this.companyService.update(this.editId, payload).subscribe({
        next: () => {
          this.snackbar.success('Company saved successfully');
          this.router.navigate(['/companies/search']);
        },
        error: () => this.snackbar.error('Failed to update company'),
      });
    } else {
      this.companyService.create(payload).subscribe({
        next: () => {
          this.snackbar.success('Company saved successfully');
          this.router.navigate(['/companies/search']);
        },
        error: () => this.snackbar.error('Failed to save company'),
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/companies/search']);
  }
}
