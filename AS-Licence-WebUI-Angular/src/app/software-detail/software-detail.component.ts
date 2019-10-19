import { Component, OnInit } from '@angular/core';
import { Software } from '../_viewmodel/software/software';
import { AlertifyService } from '../_services/alertify.service';
import { SoftwareService } from '../_services/_software/software.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-software-detail',
  templateUrl: './software-detail.component.html',
  styleUrls: ['./software-detail.component.css']
})
export class SoftwareDetailComponent implements OnInit {

  FormSpinnerSoftware: boolean;

  software: Software = new Software();
  constructor(
    private alertify: AlertifyService,
    private softwareService: SoftwareService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {

  }



  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getSoftware(params.softwareId);
    });
  }

  getSoftware(softwareId: number) {
    if (softwareId > 0) {
      this.FormSpinnerSoftware = true;

      this.softwareService.getSoftwareLists(softwareId).subscribe(next => {

        this.software = next.data;
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.FormSpinnerSoftware = false;
      });
    }
  }

  saveSoftware() {
    this.FormSpinnerSoftware = true;

    this.softwareService.saveSoftware(this.software).subscribe(next => {

      this.alertify.success('Kayıt başarılı');
      this.router.navigate(['/software-list']);
      this.FormSpinnerSoftware = false;
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerSoftware = false;
    });
  }

  deleteSoftware() {
    this.FormSpinnerSoftware = true;

    this.softwareService.deleteSoftware(this.software.softwareId).subscribe(next => {

      this.alertify.success('Silme işlemi başarılı');
      this.router.navigate(['/software-list']);
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerSoftware = false;
    }, () => {
      this.FormSpinnerSoftware = false;
    });
  }

}
