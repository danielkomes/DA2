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
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
})
export class RegisterPageComponent {
  response: string = '';
  emailValue: string = '';
  addressValue: string = '';
  passwordValue: string = '';
  success: boolean = true;
  errorMessage: string = '';
  loginUrl: string = 'login';

  constructor(private http: HttpClient, private router: Router) {}

  signup() {
    const postData = {
      email: this.emailValue,
      address: this.addressValue,
      password: this.passwordValue,
    };
    console.log(postData);

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .post(`${ApiConfig.route}${ApiConfig.signup}`, postData, {
        headers,
        observe: 'response',
        responseType: 'text' as 'json',
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Created) {
            this.success = true;
            this.router.navigate([ApiConfig.shoppingCart]);
          }
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          this.errorMessage = error.error.message;
          this.success = false;
        }
      );
  }
}
