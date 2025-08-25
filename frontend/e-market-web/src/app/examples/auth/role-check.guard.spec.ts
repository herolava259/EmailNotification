import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { roleCheckGuard } from './role-check.guard';

describe('roleCheckGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => roleCheckGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
