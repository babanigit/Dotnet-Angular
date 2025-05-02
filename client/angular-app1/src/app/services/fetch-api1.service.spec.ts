import { TestBed } from '@angular/core/testing';

import { FetchApi1Service } from './fetch-api1.service';

describe('FetchApi1Service', () => {
  let service: FetchApi1Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FetchApi1Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
