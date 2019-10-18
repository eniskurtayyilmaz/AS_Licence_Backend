import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  baseUrl = 'http://lisans.codeapp.co/api/Customer/';

  constructor(private authService: AuthService, private http: HttpClient) {


  }

  getCustomerLists(customerId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };


    if (customerId) {
      return this.http.get<any>(this.baseUrl + 'GetCustomerId/?customerId=' + customerId, httpOptions)
        .pipe();
    }
    else {
      return this.http.get<any>(this.baseUrl + 'GetCustomerLists', httpOptions)
        .pipe();
    }

  }
}
