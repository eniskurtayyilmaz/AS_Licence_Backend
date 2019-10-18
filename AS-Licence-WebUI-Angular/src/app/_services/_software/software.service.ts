import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SoftwareService {


  baseUrl = 'http://lisans.codeapp.co/api/Software/';

  constructor(private authService: AuthService, private http: HttpClient) {

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


}
