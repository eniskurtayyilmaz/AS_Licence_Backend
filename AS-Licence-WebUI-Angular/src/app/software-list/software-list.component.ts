import { Component, OnInit } from '@angular/core';
import { SoftwareService } from '../_services/_software/software.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { Software } from '../_viewmodel/software/software';

@Component({
  selector: 'app-software-list',
  templateUrl: './software-list.component.html',
  styleUrls: ['./software-list.component.css']
})
export class SoftwareListComponent implements OnInit {

  constructor(private softwareService: SoftwareService, private alertify: AlertifyService, private router: Router) { }

  softwares: Software[] = [];

  ngOnInit() {
    this.getSoftwareLists();
  }

  getSoftwareLists() {
    this.softwareService.getSoftwareLists().subscribe(next => {
      // alert('login ok');
      this.softwares = next.data;
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });
  }



}
