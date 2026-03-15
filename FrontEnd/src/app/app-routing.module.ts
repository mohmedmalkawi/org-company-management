import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: '/organizations/search', pathMatch: 'full' },
  {
    path: 'organizations',
    loadChildren: () =>
      import('./organization/organization.module').then((m) => m.OrganizationModule),
  },
  {
    path: 'companies',
    loadChildren: () =>
      import('./company/company.module').then((m) => m.CompanyModule),
  },
  { path: '**', redirectTo: '/organizations/search' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
