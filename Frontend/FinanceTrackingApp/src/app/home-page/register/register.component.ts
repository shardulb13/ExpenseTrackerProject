import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { catchError, map, of } from 'rxjs';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: any;
  barwidth: number = 0;
  constructor(private route: Router, private authService: AuthenticationService, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      Firstname: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      Lastname: new FormControl('', [Validators.required, Validators.maxLength(20)],),
      UserName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      Email: new FormControl('', [Validators.required, Validators.pattern(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/)]),
      Password: new FormControl('', [Validators.required, Validators.pattern(/(?:[^a-z])(?=.*[0-9])(?=.*[?!@#$%^&*\/\\])(?=.*[a-z])[a-zA-Z0-9- ?!@#$%^&*\/\\]/),
      Validators.maxLength(14), Validators.minLength(8)]),
      File: new FormControl('', Validators.required),
    });
  }

  get form() {
    return this.registerForm.controls;
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      console.log(event.target.files[0]);
      this.registerForm.patchValue({
        File: event.target.files[0]
      });
    }
  }

  login() {
    this.route.navigate(['login']);
  }

  register() {
    if (!this.registerForm.valid) {
      alert("Please fill up the form details")
    }
    else {
      const formData: any = new FormData();
      formData.append('File', this.registerForm.get('File')?.value);
      formData.append('Firstname', this.registerForm.get('Firstname')?.value);
      formData.append('Lastname', this.registerForm.get('Lastname')?.value);
      formData.append('UserName', this.registerForm.get('UserName')?.value);
      formData.append('Email', this.registerForm.get('Email')?.value);
      formData.append('Password', this.registerForm.get('Password')?.value);
      this.authService.regsiter(formData).pipe(
        map(events => {
          switch (events.type) {
            case HttpEventType.UploadProgress:
              this.barwidth = Math.round(events.loaded / events.total * 100);
              break;
            case HttpEventType.Response:
              this.toastrService.success("Image Uploaded Successfully");
              setTimeout(() => {
                this.barwidth = 0;
              }, 2500);
              this.toastrService.success("Successfully Registered");
              this.route.navigate(['login']);
          }
        }),
        catchError((error: HttpErrorResponse) => {
          this.toastrService.error("Registration Unsuccessful");
          return of(error);
        })
      ).subscribe(res => {
        // setTimeout(() => {
        //   this.toastrService.success("Successfully Registered");
        //   this.route.navigate(['login']);
        // }, 5000);
      });
    }
    console.log("form Value", this.registerForm.value);
  }
}
