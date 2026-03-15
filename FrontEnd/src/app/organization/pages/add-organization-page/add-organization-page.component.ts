import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, startWith, map } from 'rxjs';
import { OrganizationService } from '../../services/organization.service';
import { SnackbarService } from '../../../shared/services/snackbar.service';

@Component({
  selector: 'app-add-organization-page',
  standalone: false,
  templateUrl: './add-organization-page.component.html',
  styleUrls: ['./add-organization-page.component.scss'],
})
export class AddOrganizationPageComponent implements OnInit {
  form!: FormGroup;
  countryOptions: string[] = [];
  filteredCountryOptions$!: Observable<string[]>;
  editId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private organizationService: OrganizationService,
    private snackbar: SnackbarService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.editId = this.route.snapshot.paramMap.get('id');
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
      this.organizationService.getById(this.editId).subscribe((org) => {
        if (org) {
          this.form.patchValue({
            name: org.name,
            code: org.code,
            country: org.country,
            phone: org.phone,
            fullAddress: org.fullAddress,
            creationDate: org.creationDate?.slice(0, 16),
            updatingDate: org.updatingDate?.slice(0, 16),
          });
        }
      });
    }
  }

  private buildForm(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      country: ['', Validators.required],
      phone: ['', Validators.required],
      fullAddress: ['', Validators.required],
      creationDate: [{ value: '', disabled: true }],
      updatingDate: [{ value: '', disabled: true }],
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const raw = this.form.getRawValue();
    const payload = {
      name: raw.name,
      code: raw.code,
      country: raw.country,
      phone: raw.phone,
      fullAddress: raw.fullAddress,
    };
    if (this.editId) {
      this.organizationService.update(this.editId, payload).subscribe({
        next: () => {
          this.snackbar.success('Organization saved successfully');
          this.router.navigate(['/organizations/search']);
        },
        error: () => this.snackbar.error('Failed to update organization'),
      });
    } else {
      this.organizationService.create(payload).subscribe({
        next: () => {
          this.snackbar.success('Organization saved successfully');
          this.router.navigate(['/organizations/search']);
        },
        error: () => this.snackbar.error('Failed to save organization'),
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/organizations/search']);
  }
}
