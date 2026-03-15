import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Organization,
  OrganizationSearchResult,
  OrganizationSearchFilters,
} from '../models/organization.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class OrganizationService {
  private readonly baseUrl = `${environment.apiUrl}/organizations`;
  private readonly countriesUrl = `${environment.apiUrl}/countries`;

  constructor(private http: HttpClient) {}

  getCountries(): Observable<string[]> {
    return this.http.get<string[]>(this.countriesUrl);
  }

  create(organization: Partial<Organization>): Observable<string> {
    return this.http.post<string>(this.baseUrl, organization);
  }

  search(filters: OrganizationSearchFilters): Observable<OrganizationSearchResult[]> {
    let params = new HttpParams();
    if (filters.name) {
      params = params.set('Name', filters.name);
    }
    if (filters.code) {
      params = params.set('Code', filters.code);
    }
    if (filters.country && filters.country !== 'All Countries') {
      params = params.set('Country', filters.country);
    }
    return this.http.get<OrganizationSearchResult[]>(this.baseUrl, { params });
  }

  getById(id: string): Observable<Organization> {
    return this.http.get<Organization>(`${this.baseUrl}/${id}`);
  }

  getDetails(id: string): Observable<{ phone: string; country: string; fullAddress: string }> {
    return this.http.get<{ phone: string; country: string; fullAddress: string }>(
      `${this.baseUrl}/${id}/details`
    );
  }

  update(id: string, organization: Partial<Organization>): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, organization);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  getAll(): Observable<OrganizationSearchResult[]> {
    return this.http.get<OrganizationSearchResult[]>(this.baseUrl);
  }
}
