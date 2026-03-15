import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCompanyPageComponent } from './pages/add-company-page/add-company-page.component';
import { CompanySearchPageComponent } from './pages/company-search-page/company-search-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'search', pathMatch: 'full' },
  { path: 'add', component: AddCompanyPageComponent },
  { path: 'search', component: CompanySearchPageComponent },
  { path: 'edit/:id', component: AddCompanyPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CompanyRoutingModule {}
