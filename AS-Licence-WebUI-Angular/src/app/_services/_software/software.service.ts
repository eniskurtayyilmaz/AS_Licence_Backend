import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Websetting } from 'src/app/_viewmodel/settings/websetting';
import { Software } from 'src/app/_viewmodel/software/software';

@Injectable({
  providedIn: 'root'
})
export class SoftwareService {


  env: { baseUrl: any; };
  baseUrl = '';

  constructor(private authService: AuthService, private http: HttpClient) {
    this.env = Websetting;
    this.baseUrl = this.env.baseUrl + 'Software/';
  }


  getSoftwareLists(softwareId: number): Observable<any> {


    if (softwareId) {
      return this.http.get<any>(this.baseUrl + 'GetSoftwareById/?softwareId=' + softwareId)
        .pipe();
    } else {
      return this.http.get<any>(this.baseUrl + 'GetSoftwareLists')
        .pipe();
    }
  }

  saveSoftware(software: Software) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken()
      })
    };


    return this.http.post<any>(this.baseUrl + 'SaveSoftware', software, httpOptions)
      .pipe();
  }


  deleteSoftware(softwareId: number) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.authService.getToken()
      })
    };

    return this.http.delete<any>(this.baseUrl + 'DeleteSoftware/' + softwareId, httpOptions)
      .pipe();
  }
}
