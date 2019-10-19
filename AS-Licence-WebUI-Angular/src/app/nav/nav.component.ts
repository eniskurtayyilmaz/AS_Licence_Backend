import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  loginModel: any = {};


  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.loginModel).subscribe(next => {
      // alert('login ok');

      console.log(next);
    }, error => {
      this.alertify.error(error);
      console.log(error);
    }, () => {
      this.router.navigate(['/customer-list']);
    });
  }

  loggedIn() {

    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/home']);
  }

}
