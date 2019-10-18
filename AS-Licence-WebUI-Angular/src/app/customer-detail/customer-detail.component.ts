import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/_customer/customer.service';
import { AlertifyService } from '../_services/alertify.service';
import { Customer } from '../_viewmodel/customer/customer';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { SubscriptionSummary } from '../_viewmodel/subscription/subscriptionSummary';
import { SubscriptionService } from '../_services/_subscription/subscription.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  constructor(
    private customerService: CustomerService,
    private subscriptionService: SubscriptionService,
    private alertify: AlertifyService,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }


  subscriptionSummaries: SubscriptionSummary[] = [];
  customer: Customer = new Customer();
  customerId: number;

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getCustomer(params.customerId);
      this.getSubscriptionSummaryListByCustomerId();
    });
  }


  getSubscriptionSummaryListByCustomerId() {

    this.subscriptionService.getSubscriptionSummaryListByCustomerId(this.customerId).subscribe(next => {
      // alert('login ok');
      this.subscriptionSummaries = next.data;
    }, error => {
      this.alertify.error(error);
    });
  }

  deleteSubscription(subscriptionId: number) {
    if (subscriptionId > 0) {
      this.subscriptionService.deleteSubscription(subscriptionId).subscribe(next => {
        // alert('login ok');
        this.alertify.success('Silme işlemi başarılı');
        this.getSubscriptionSummaryListByCustomerId();
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  getCustomer(customerId: number) {

    this.customerId = customerId;

    if (customerId > 0) {
      this.customerService.getCustomerLists(customerId).subscribe(next => {
        // alert('login ok');
        this.customer = next.data;
      }, error => {
        this.customerId = 0;
        this.alertify.error(error);
      });
    }
  }

  saveCustomer() {
    this.customerService.saveCustomer(this.customer).subscribe(next => {
      // alert('login ok');

      this.alertify.success('Kayıt başarılı');


      this.customer = next.data;
      this.router.navigate(['/customer-detail', this.customer.customerId]);
    }, error => {
      this.alertify.error(error);
    });
  }

}
