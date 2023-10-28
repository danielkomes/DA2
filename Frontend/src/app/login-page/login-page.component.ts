import { Component, ViewChild, ElementRef } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiConfig } from './../../ApiConfig';

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
  errorMessage: string = 'test';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    const postData = `\"${this.passwordValue}\"`;
    console.log(postData);

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .post(
        `${ApiConfig.route}${ApiConfig.session}/${this.emailValue}`,
        postData,
        {
          headers,
          observe: 'response',
        }
      )
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.authenticationSuccess = true;
            this.router.navigate([ApiConfig.shoppingCart]);
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
