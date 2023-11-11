import { Component, ViewChild, ElementRef } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParamsOptions,
  HttpResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';
import { UserInfo } from 'src/app/models/user-info';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
})
export class UserPageComponent {
  // response: string = '';
  user?: UserInfo;
  emailValue: string = '';
  addressValue: string = '';
  passwordValue: string = '';
  success: boolean = true;
  errorMessage: string = '';
  showOutput: boolean = false;
  loginUrl: string = 'login';

  constructor(private http: HttpClient, private router: Router) {
    const token: string | null = localStorage.getItem('token');
    if (token != null) {
      this.getUserData();
    } else {
      this.router.navigateByUrl(this.loginUrl);
    }
  }

  getUserData() {
    // Make the POST request
    const email: string | null = localStorage.getItem('email');
    const headers = new HttpHeaders().set(
      'Authorization',
      `${localStorage.getItem('token')!}`
      // '8f1b6599-ce6e-41c0-b9e8-2806d7205b79'
    );
    this.http
      .get(`${environment.API_HOST}${endpoints.users}/${email}`, {
        headers: headers,
        observe: 'response',
        // responseType: 'text' as 'json',
      })
      .subscribe(
        (response: any) => {
          this.user = new UserInfo(
            response.body.email,
            response.body.address,
            response.body.password
          );
          this.emailValue = this.user.email;
          this.addressValue = this.user.address;
          this.passwordValue = this.user.password;
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          this.errorMessage = error.error.message;
          this.success = false;
          this.showOutput = true;
          this.router.navigateByUrl('login');
        }
      );
  }

  save() {
    const postData = {
      email: this.emailValue,
      address: this.addressValue,
      password: this.passwordValue,
    };

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `${localStorage.getItem('token')}`);

    this.http
      .put(
        `${environment.API_HOST}${endpoints.users}/${localStorage.getItem(
          'email'
        )}`,
        postData,
        {
          headers,
          observe: 'response',
        }
      )
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Ok) {
            this.success = true;
            localStorage.setItem('email', this.emailValue);
            this.showOutput = true;
          }
        },
        (error: HttpErrorResponse) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
          error = JSON.parse(error.error);
          this.errorMessage = error.message;
          this.success = false;
          this.showOutput = true;
        }
      );
  }

  logout() {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `${localStorage.getItem('token')}`);
    this.http
      .delete(`${environment.API_HOST}${endpoints.session}`, {
        headers,
        // observe: 'response',
      })
      .subscribe(
        (response: any) => {
          localStorage.removeItem('token');
          localStorage.removeItem('email');
          this.router.navigateByUrl('login');
        },
        (error: HttpErrorResponse) => {
          // console.error('POST Request Error:', error);
          // // Handle any errors here
          // error = JSON.parse(error.error);
          this.errorMessage = error.message;
          this.success = false;
          this.showOutput = true;
        }
      );
  }
}
