import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page-nav',
  templateUrl: './landing-page-nav.component.html',
  styleUrls: ['./landing-page-nav.component.scss']
})
export class LandingPageNavComponent implements OnInit {

  constructor(private route: Router) { }

  ngOnInit(): void {
  }

  login(){
    this.route.navigate(['login']);
  }
  signUp(){
    this.route.navigate(['register']);
  }

}
