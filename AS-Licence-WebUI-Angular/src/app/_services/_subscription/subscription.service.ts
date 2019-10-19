import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Websetting } from 'src/app/_viewmodel/settings/websetting';
import { Subscription } from 'src/app/_viewmodel/subscription/subscription';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  env: { baseUrl: string; };
  baseUrl = ''; // = 'Subscription/';

  constructor(private authService: AuthService, private http: HttpClient) {
    this.env = Websetting;
    this.baseUrl = this.env.baseUrl + 'Subscription/';
  }

  saveSubscription(subscription: Subscription): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken()
      })
    };


    return this.http.post<any>(this.baseUrl + 'SaveSubscription', subscription, httpOptions)
      .pipe();

  }


  deleteSubscription(subscriptionId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken(),
      })
    };

    return this.http.delete<any>(this.baseUrl + 'DeleteSubscriptionBySubscriptionId/' + subscriptionId, httpOptions)
      .pipe();

  }

  GetSubscriptionBySubscriptionId(subscriptionId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken()
      })
    };

    return this.http.get<any>(this.baseUrl + 'GetSubscriptionBySubscriptionId/?subscriptionId=' + subscriptionId, httpOptions)
      .pipe();

  }

  getSubscriptionSummaryListByCustomerId(customerId: number): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken()
      })
    };

    return this.http.get<any>(this.baseUrl + 'GetSubscriptionSummaryListByCustomerId/?customerId=' + customerId, httpOptions)
      .pipe();

  }

}