import { Component, ViewChild, ElementRef } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';
import { UserServiceService as UserService } from 'src/app/services/user.service';
import { UserLogin } from 'src/app/models/user-login';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  response: string = '';
  emailValue: string = '';
  passwordValue: string = '';
  authenticationSuccess: boolean = true;
  errorMessage: string = '';
  registerUrl: string = 'register';

  // constructor(private http: HttpClient, private router: Router) {}
  constructor(
    private userService: UserService,
    private http: HttpClient,
    private router: Router
  ) {}

  login() {
    const postData = `\"${this.passwordValue}\"`;
    const user: UserLogin = new UserLogin(this.emailValue, this.passwordValue);
    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .post(
        `${environment.API_HOST}${endpoints.session}/${this.emailValue}`,
        postData,
        {
          headers,
          observe: 'response',
        }
      )
      // this.userService.login(user)
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.authenticationSuccess = true;
            localStorage.setItem('token', response.body.token);
            localStorage.setItem('email', this.emailValue);
            this.router.navigate([endpoints.shoppingCart]);
          }
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          this.errorMessage = error.error.message;
          this.authenticationSuccess = false;
        }
      );
  }
}
