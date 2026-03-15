import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOrganizationPageComponent } from './pages/add-organization-page/add-organization-page.component';
import { OrganizationSearchPageComponent } from './pages/organization-search-page/organization-search-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'search', pathMatch: 'full' },
  { path: 'add', component: AddOrganizationPageComponent },
  { path: 'search', component: OrganizationSearchPageComponent },
  { path: 'edit/:id', component: AddOrganizationPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrganizationRoutingModule {}
