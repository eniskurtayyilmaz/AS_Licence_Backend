import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/_customer/customer.service';
import { AlertifyService } from '../_services/alertify.service';
import { Customer } from '../_viewmodel/customer/customer';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { SubscriptionSummary } from '../_viewmodel/subscription/subscriptionSummary';
import { SubscriptionService } from '../_services/_subscription/subscription.service';
import { Subscription } from '../_viewmodel/subscription/subscription';
import { Software } from '../_viewmodel/software/software';
import { SoftwareService } from '../_services/_software/software.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  FormSpinnerCustomer: boolean;
  FormSpinnerSubscription: boolean;
  FormSpinnerSubscriptionDetail: boolean;

  constructor(
    private customerService: CustomerService,
    private subscriptionService: SubscriptionService,
    private softwareService: SoftwareService,
    private alertify: AlertifyService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.FormSpinnerCustomer = true;
    this.FormSpinnerSubscription = true;
    this.FormSpinnerSubscriptionDetail = true;
  }

  customerId: number;
  customer: Customer = new Customer();
  subscriptionSummaries: SubscriptionSummary[] = [];
  softwares: Software[] = [];
  subscription: Subscription = new Subscription();

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getCustomer(params.customerId);
      this.getSubscriptionSummaryListByCustomerId();
      this.getSoftwareLists();
      this.FormSpinnerSubscriptionDetail = false;
    });
  }
  getSoftwareLists() {
    this.softwareService.getSoftwareLists(0).subscribe(next => {
      // alert('login ok');

      this.softwares = next.data;

    }, error => {
      this.alertify.error(error);
    });
  }

  saveSubscription() {
    this.FormSpinnerSubscriptionDetail = true;

    this.subscriptionService.saveSubscription(this.subscription).subscribe(next => {
      // alert('login ok');

      this.alertify.success('Kayıt başarılı');

      this.getSubscriptionSummaryListByCustomerId();
      this.FormSpinnerSubscriptionDetail = false;

      document.getElementById('toggle-subscription-list').click();
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerSubscriptionDetail = false;
    });

  }


  getSubscriptionSummaryListByCustomerId() {
    this.FormSpinnerSubscription = true;
    this.subscriptionService.getSubscriptionSummaryListByCustomerId(this.customerId).subscribe(next => {
      // alert('login ok');
      this.subscriptionSummaries = next.data;
      this.FormSpinnerSubscription = false;
      this.clearSubscription();
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerSubscription = false;

    });
  }

  clearSubscription() {
    this.subscription = new Subscription();
    this.subscription.customerId = this.customerId;
    this.subscription.softwareId = 0;
    this.subscription.subscriptionId = 0;
    this.subscription.subScriptionStartDate = '';
    this.subscription.subScriptionEndDate = '';
    this.subscription.subScriptionLicenceCount = 0;
    this.subscription.subscriptionIsActive = true;
  }

  getDate(dateString: string) {
    let days: number = parseInt(dateString.substring(8, 10));
    let months: number = parseInt(dateString.substring(5, 7));
    let years: number = parseInt(dateString.substring(0, 5));
    let goodDate: Date = new Date(years + "/" + months + "/" + days);
    goodDate.setDate(goodDate.getDate() + 1);
    return goodDate.toISOString().substring(0, 10);
  }

  getSubscription(subscriptionId: number) {
    this.FormSpinnerSubscriptionDetail = true;
    if (subscriptionId > 0) {

      this.subscriptionService.GetSubscriptionBySubscriptionId(subscriptionId).subscribe(next => {
        // alert('login ok');
        this.subscription = next.data;

        this.subscription.subScriptionStartDate = this.getDate(this.subscription.subScriptionStartDate);
        this.subscription.subScriptionEndDate = this.getDate(this.subscription.subScriptionEndDate);

        document.getElementById('toggle-subscription-detail').click();
        this.FormSpinnerSubscriptionDetail = false;
      }, error => {
        this.alertify.error(error);
        this.FormSpinnerSubscriptionDetail = false;
      }, () => {
        this.FormSpinnerSubscriptionDetail = false;
      });
    } else {
      this.clearSubscription();
      this.FormSpinnerSubscriptionDetail = false;
      document.getElementById('toggle-subscription-detail').click();
    }
  }


  deleteSubscription(subscriptionId: number) {
    this.FormSpinnerSubscription = true;
    if (subscriptionId > 0) {
      this.subscriptionService.deleteSubscription(subscriptionId).subscribe(next => {
        // alert('login ok');
        this.alertify.success('Silme işlemi başarılı');

        this.getSubscriptionSummaryListByCustomerId();
        this.FormSpinnerSubscription = false;

      }, error => {
        this.alertify.error(error);
        this.FormSpinnerSubscription = false;
      });
    }
  }




  /************************************************************************/
  getCustomer(customerId: number) {
    this.FormSpinnerCustomer = true;
    this.customerId = customerId;

    if (customerId > 0) {
      this.customerService.getCustomerLists(customerId).subscribe(next => {
        // alert('login ok');
        this.customer = next.data;
        this.FormSpinnerCustomer = false;
      }, error => {
        this.customerId = 0;
        this.alertify.error(error);
        this.FormSpinnerCustomer = false;
      });
    }
    else {
      this.FormSpinnerCustomer = false;
    }

  }

  saveCustomer() {
    this.FormSpinnerCustomer = true;
    this.customerService.saveCustomer(this.customer).subscribe(next => {
      // alert('login ok');

      this.alertify.success('Kayıt başarılı');


      this.customer = next.data;
      this.router.navigate(['/customer-detail', this.customer.customerId]);
      this.FormSpinnerCustomer = false;
    }, error => {
      this.alertify.error(error);
      this.FormSpinnerCustomer = false;
    });
  }

}
