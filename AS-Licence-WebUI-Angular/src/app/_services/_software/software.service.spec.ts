/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SoftwareService } from './software.service';

describe('Service: Software', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SoftwareService]
    });
  });

  it('should ...', inject([SoftwareService], (service: SoftwareService) => {
    expect(service).toBeTruthy();
  }));
});
