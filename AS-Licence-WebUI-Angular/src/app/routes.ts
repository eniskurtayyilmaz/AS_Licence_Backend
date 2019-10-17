import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { SoftwareListComponent } from './software-list/software-list.component';
export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'customer-list', component: CustomerListComponent, canActivate: [AuthGuard] },
            { path: 'software-list', component: SoftwareListComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
]