import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, of } from 'rxjs';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.scss']
})
export class UpdateProfileComponent implements OnInit {
  loggedInUser:any;
  updateForm!: any;
  barwidth: number = 0;
  id!:any;
  constructor(private route: Router, private authService: AuthenticationService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.updateForm = new FormGroup({
      Firstname: new FormControl('', [Validators.required]),
      Lastname: new FormControl('', [Validators.required]),
      UserName: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/)]),
      File: new FormControl('', Validators.required),
    });

    this.id = this.activatedRoute.snapshot.paramMap.get('id');

    this.authService.getCurrentUserDetails().subscribe(res=>{
      this.loggedInUser = res;
      if(this.id == this.loggedInUser.id){
        this.updateForm.controls.Firstname.setValue(this.loggedInUser.firstName);
        this.updateForm.controls.Lastname.setValue(this.loggedInUser.lastName);
        this.updateForm.controls.UserName.setValue(this.loggedInUser.userName);
        this.updateForm.controls.Email.setValue(this.loggedInUser.email);
      }
    })
  }

  get form() {
    return this.updateForm.controls;
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      console.log(event.target.files[0]);
      this.updateForm.patchValue({
        File: event.target.files[0]
      });
    }
  }

  update() {
    if (!this.updateForm.valid) {
      alert("Please fill up the form details")
    }
    else {
      const formData: any = new FormData();
      formData.append('File', this.updateForm.get('File')?.value);
      formData.append('Firstname', this.updateForm.get('Firstname')?.value);
      formData.append('Lastname', this.updateForm.get('Lastname')?.value);
      formData.append('UserName', this.updateForm.get('UserName')?.value);
      formData.append('Email', this.updateForm.get('Email')?.value);
      formData.append('Password', this.updateForm.get('Password')?.value);
      this.authService.UpdateUserProfile(formData).pipe(
        map(events => {
          switch (events.type) {
            case HttpEventType.UploadProgress:
              this.barwidth = Math.round(events.loaded / events.total! * 100);
              break;
            case HttpEventType.Response:
              this.toastrService.success("Image Uploaded Successfully");
              setTimeout(() => {
                this.barwidth = 0;
              }, 2500);
              this.toastrService.success("Successfully Registered");
              this.route.navigate(['user/profile']);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.toastrService.error("Registration Unsuccessful");
          return of(error);
        })
      ).subscribe(res => {
      });
    }
  }

}
