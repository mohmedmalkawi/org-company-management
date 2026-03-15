export interface Organization {
  id: string;
  name: string;
  code: string;
  country: string;
  phone: string;
  fullAddress: string;
  creationDate: string;
  updatingDate: string;
}

export interface OrganizationSearchResult {
  id: string;
  code: string;
  name: string;
  phone: string;
}

export interface OrganizationFormValue {
  name: string;
  code: string;
  country: string;
  phone: string;
  fullAddress: string;
}

export interface OrganizationSearchFilters {
  name?: string;
  code?: string;
  country?: string;
}
