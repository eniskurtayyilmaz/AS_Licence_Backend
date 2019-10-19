import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Websetting } from '../_viewmodel/settings/websetting';
import { User } from '../_viewmodel/user/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  env: { baseUrl: string; };
  baseUrl = '';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient, private alertify: AlertifyService) {
    this.env = Websetting;
    this.baseUrl = this.env.baseUrl + 'Auth/';
  }


  ChangeUserPassword(user: User) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.getToken()
      })
    };


    return this.http.post<any>(this.baseUrl + 'ChangeUserPassword', user, httpOptions)
      .pipe();
  }


  login(loginModel: any) {

    let url = this.baseUrl + 'Login';

    return this.http.post(url, loginModel)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);

            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.alertify.success(user.message);
          }
        })
      );
  }


  register(registerModel: any) {
    return this.http.post(this.baseUrl + 'Register', registerModel);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  getUserName() {
    return this.jwtHelper.decodeToken(this.getToken()).unique_name;

  }

  getToken() {

    const token = localStorage.getItem('token');
    return token;

  }
}
