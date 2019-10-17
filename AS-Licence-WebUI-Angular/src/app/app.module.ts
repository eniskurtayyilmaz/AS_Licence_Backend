import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';

import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { SoftwareListComponent } from './software-list/software-list.component';
import { SoftwareFilterPipe } from './_viewmodel/software/software.pipe';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerFilterPipe } from './_viewmodel/customer/customer.pipe';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      CustomerListComponent,
      SoftwareListComponent,
      CustomerListComponent,

      SoftwareFilterPipe,
      CustomerFilterPipe,

   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
