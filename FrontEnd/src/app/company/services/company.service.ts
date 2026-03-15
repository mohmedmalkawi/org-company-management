import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Company, CompanySearchResult, CompanySearchFilters } from '../models/company.model';
import { OrganizationService } from '../../organization/services/organization.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  private readonly baseUrl = `${environment.apiUrl}/companies`;

  constructor(
    private http: HttpClient,
    private organizationService: OrganizationService
  ) {}

  getOrganizations(): Observable<{ id: string; name: string }[]> {
    return this.organizationService.getAll();
  }

  getOrganizationDetails(
    organizationId: string
  ): Observable<{ phone: string; country: string; fullAddress: string }> {
    return this.organizationService.getDetails(organizationId);
  }

  create(payload: {
    organizationId: string;
    name: string;
    code: string;
    country: string;
    phone: string;
    fullAddress: string;
  }): Observable<string> {
    return this.http.post<string>(this.baseUrl, payload);
  }

  search(filters: CompanySearchFilters): Observable<CompanySearchResult[]> {
    let params = new HttpParams();
    if (filters.organizationName) {
      params = params.set('organizationName', filters.organizationName);
    }
    if (filters.companyName) {
      params = params.set('companyName', filters.companyName);
    }
    if (filters.country && filters.country !== 'All Countries') {
      params = params.set('country', filters.country);
    }
    if (filters.companyCode) {
      params = params.set('companyCode', filters.companyCode);
    }
    return this.http.get<CompanySearchResult[]>(this.baseUrl, { params });
  }

  getById(id: string): Observable<Company> {
    return this.http.get<Company>(`${this.baseUrl}/${id}`);
  }

  update(
    id: string,
    payload: {
      organizationId: string;
      name: string;
      code: string;
      country: string;
      phone: string;
      fullAddress: string;
    }
  ): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
