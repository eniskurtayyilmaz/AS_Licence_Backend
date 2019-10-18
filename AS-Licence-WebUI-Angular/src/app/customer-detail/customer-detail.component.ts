import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/_customer/customer.service';
import { AlertifyService } from '../_services/alertify.service';
import { Customer } from '../_viewmodel/customer/customer';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  constructor(private customerService: CustomerService, private alertify: AlertifyService, private activatedRoute: ActivatedRoute) { }

  customer: Customer;

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.getCustomer(params.customerId);
    });
  }

  getCustomer(customerId: number) {

    this.customerService.getCustomerLists(customerId).subscribe(next => {
      // alert('login ok');
      this.customer = next.data;
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });

  }

}
