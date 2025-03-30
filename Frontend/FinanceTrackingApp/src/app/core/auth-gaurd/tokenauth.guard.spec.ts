import { TestBed } from '@angular/core/testing';

import { TokenauthGuard } from './tokenauth.guard';

describe('TokenauthGuard', () => {
  let guard: TokenauthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(TokenauthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
