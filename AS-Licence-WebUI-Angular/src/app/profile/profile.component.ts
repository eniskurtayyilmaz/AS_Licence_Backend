import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_viewmodel/user/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {


  FormSpinnerProfile: boolean;
  user: User = new User();
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.FormSpinnerProfile = false;
    
  }

  changeUserPassword() {

    this.FormSpinnerProfile = true;

    this.authService.ChangeUserPassword(this.user).subscribe(next => {

      this.alertify.success('İşlem başarılı');
      localStorage.removeItem('token');
      this.router.navigate(['/home']);
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerProfile = false;
    });
  }
}
