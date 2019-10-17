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

  getCustomerLists(): Observable<any> {

    let url = this.baseUrl + 'GetCustomerLists';

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.authService.getToken()
      })
    };
    return this.http.get<any>(url, httpOptions).pipe();
  }
}
