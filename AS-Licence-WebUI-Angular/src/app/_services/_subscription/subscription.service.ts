import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  baseUrl = 'http://lisans.codeapp.co/api/Subscription/';

  constructor(private authService: AuthService, private http: HttpClient) {


  }
  /*
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
    */


  deleteSubscription(subscriptionId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };

    return this.http.delete<any>(this.baseUrl + 'DeleteSubscriptionBySubscriptionId/?subscriptionId=' + subscriptionId, httpOptions)
      .pipe();

  }

  getSubscriptionSummaryListByCustomerId(customerId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };

    return this.http.get<any>(this.baseUrl + 'GetSubscriptionSummaryListByCustomerId/?customerId=' + customerId, httpOptions)
      .pipe();

  }

}