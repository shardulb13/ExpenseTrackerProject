import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  loggedInUser:any;
  constructor(private authSevice: AuthenticationService, private route: Router) { }

  ngOnInit(): void {
    this.authSevice.getCurrentUserDetails().subscribe(res=>{
      this.loggedInUser = res;
    })
  }

  updateProfile(id:string){
    this.route.navigate([`user/profile/update/${id}`]);
  }

}
