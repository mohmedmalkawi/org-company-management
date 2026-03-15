import { NgModule } from '@angular/core';
import { OrganizationRoutingModule } from './organization-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AddOrganizationPageComponent } from './pages/add-organization-page/add-organization-page.component';
import { OrganizationSearchPageComponent } from './pages/organization-search-page/organization-search-page.component';

@NgModule({
  declarations: [
    AddOrganizationPageComponent,
    OrganizationSearchPageComponent,
  ],
  imports: [SharedModule, OrganizationRoutingModule],
})
export class OrganizationModule {}
