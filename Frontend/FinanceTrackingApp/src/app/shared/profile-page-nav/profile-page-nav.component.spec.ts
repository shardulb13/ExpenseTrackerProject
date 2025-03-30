import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePageNavComponent } from './profile-page-nav.component';

describe('ProfilePageNavComponent', () => {
  let component: ProfilePageNavComponent;
  let fixture: ComponentFixture<ProfilePageNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilePageNavComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePageNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
