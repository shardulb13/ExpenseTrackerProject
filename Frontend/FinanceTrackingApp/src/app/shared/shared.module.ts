import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageNavComponent } from './landing-page-nav/landing-page-nav.component';
import { ProfilePageNavComponent } from './profile-page-nav/profile-page-nav.component';
import { LoaderComponent } from './loader/loader.component';



@NgModule({
  declarations: [
    LandingPageNavComponent,
    ProfilePageNavComponent,
    LoaderComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    ProfilePageNavComponent,
    LandingPageNavComponent,
    LoaderComponent
  ]
})
export class SharedModule { }
