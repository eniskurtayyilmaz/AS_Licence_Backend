import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Customer } from 'src/app/_viewmodel/customer/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  baseUrl = 'http://lisans.codeapp.co/api/Customer/';

  constructor(private authService: AuthService, private http: HttpClient) {


  }

  saveCustomer(customer: Customer): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };


    return this.http.post<any>(this.baseUrl + 'SaveCustomer', customer, httpOptions)
      .pipe();

  }


  getCustomerLists(customerId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };


    if (customerId) {
      return this.http.get<any>(this.baseUrl + 'GetCustomerById/?customerId=' + customerId, httpOptions)
        .pipe();
    }
    else {
      return this.http.get<any>(this.baseUrl + 'GetCustomerLists', httpOptions)
        .pipe();
    }

  }
}
