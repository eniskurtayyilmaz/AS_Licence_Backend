import { Pipe, PipeTransform } from '@angular/core';
import { Customer } from './customer';

@Pipe({
  name: 'customerFilter'
})
export class CustomerFilterPipe implements PipeTransform {

  transform(value: Customer[], filterText: string): Customer[] {
    if (!filterText) {
      return value;
    }
    else {
      // tslint:disable-next-line: max-line-length
      return value.filter(p => p.customerEMail.toLocaleLowerCase().indexOf(filterText.toLocaleLowerCase()) !== -1 || p.customerName.toLocaleLowerCase().indexOf(filterText.toLocaleLowerCase()) !== -1);
    }
  }

}
