import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { SoftwareListComponent } from './software-list/software-list.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { SoftwareDetailComponent } from './software-detail/software-detail.component';
import { ProfileComponent } from './profile/profile.component';
export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'customer-list', component: CustomerListComponent, canActivate: [AuthGuard] },
            { path: 'customer-detail/:customerId', component: CustomerDetailComponent, canActivate: [AuthGuard] },
            { path: 'software-detail/:softwareId', component: SoftwareDetailComponent, canActivate: [AuthGuard] },
            { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
        ]
    },
    { path: 'software-list', component: SoftwareListComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
]