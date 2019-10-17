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


  getSoftwareLists(): Observable<any> {

    let url = this.baseUrl + 'GetSoftwareLists';

    return this.http.get<any>(url)
      .pipe();
  }


}
