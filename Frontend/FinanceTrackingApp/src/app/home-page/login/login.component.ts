import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { retry } from 'rxjs';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { TokenService } from 'src/app/core/services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  constructor(private route: Router, private fb: FormBuilder, private auth: AuthenticationService, private tokenService: TokenService, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      UserName: ['', [Validators.required]],
      Password: ['', [Validators.required]]
    });
  }

  get form() {
    return this.loginForm.controls;
  }
  signUp() {
    this.route.navigate(['register']);
  }

  login() {
    if (!this.loginForm.valid) {
      alert("Please enter your login credentials")
    }
    else {
      this.auth.login(this.loginForm.value).subscribe(res => {
        this.toastrService.success("Login Successful");
        if (res) {
          this.tokenService.setToken(res.token);
          this.route.navigate(['user/dashboard']);
        }
      },
        err => {
          this.toastrService.error("Invalid Login Details");
        })
    }
  }
}


