import { Pipe, PipeTransform } from '@angular/core';
import { Software } from './software';

@Pipe({
  name: 'softwareFilter'
})
export class SoftwareFilterPipe implements PipeTransform {

  transform(value: Software[], filterText: string): Software[] {
    if (!filterText) {
      return value;
    }
    else {
      return value.filter(p => p.softwareName.toLocaleLowerCase().indexOf(filterText.toLocaleLowerCase()) !== -1);
    }
  }

}
