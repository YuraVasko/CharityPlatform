import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DonorsListComponent } from './donors-list/donors-list.component';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { OrganizationListComponent } from './organization-list/organization-list.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { RegisterOrganizationComponent } from './register-organization/register-organization.component';
import { RegisterProjectComponent } from './register-project/register-project.component';
import { ReportsComponent } from './reports/reports.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { UserListComponent } from './user-list/user-list.component';
import { PaymentFormComponent } from './payment-form/payment-form.component';

const routes: Routes = [
  { path: 'sign-up', component: SignUpComponent },
  { path: '', component: MainComponent },
  { path: 'user-list', component: UserListComponent },
  { path: 'register-project', component: RegisterProjectComponent },
  { path: 'register-organization', component: RegisterOrganizationComponent },
  { path: 'project-list', component: ProjectListComponent },
  { path: 'organization-list', component: OrganizationListComponent },
  { path: 'donors', component: DonorsListComponent },
  { path: 'log-in', component: LoginComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'submit-payment', component: PaymentFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
