export interface Company {
  id: string;
  organizationId: string;
  name: string;
  code: string;
  country: string;
  phone: string;
  fullAddress: string;
  creationDate: string;
  updatingDate: string;
}

export interface CompanySearchResult {
  id: string;
  organizationId: string;
  organizationName: string;
  companyCode: string;
  companyName: string;
  phone: string;
}

export interface CompanySearchFilters {
  organizationName?: string;
  companyName?: string;
  country?: string;
  companyCode?: string;
}
