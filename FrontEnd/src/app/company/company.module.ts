import { NgModule } from '@angular/core';
import { CompanyRoutingModule } from './company-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AddCompanyPageComponent } from './pages/add-company-page/add-company-page.component';
import { CompanySearchPageComponent } from './pages/company-search-page/company-search-page.component';

@NgModule({
  declarations: [
    AddCompanyPageComponent,
    CompanySearchPageComponent,
  ],
  imports: [SharedModule, CompanyRoutingModule],
})
export class CompanyModule {}
