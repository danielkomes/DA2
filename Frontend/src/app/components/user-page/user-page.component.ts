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
import { User } from 'src/app/models/user';
import { EUserRole } from 'src/app/types/EUserRole';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
})
export class UserPageComponent {
  // response: string = '';
  user?: User;
  emailValue: string = '';
  addressValue: string = '';
  passwordValue: string = '';
  isAdmin: boolean = false;
  success: boolean = true;
  errorMessage: string = '';
  showOutput: boolean = false;
  loginUrl: string = 'login';
  adminUrl: string = 'admin';

  constructor(private http: HttpClient, private router: Router) {
    this.getUserData();
  }

  getUserData() {
    // Make the POST request
    const token: string | null = localStorage.getItem('token');
    const email: string | null = localStorage.getItem('email');
    if (token == null || email == null || email == '') {
      this.router.navigateByUrl(this.loginUrl);
      return;
    }
    const headers = new HttpHeaders().set('Authorization', `${token}`);
    this.http
      .get<User>(`${environment.API_HOST}${endpoints.users}/${email}`, {
        headers: headers,
      })
      .subscribe(
        (response: User) => {
          this.user = response;
          this.emailValue = this.user!.email;
          this.addressValue = this.user!.address;
          this.passwordValue = this.user!.password;
          this.isAdmin = this.user!.roles.includes(EUserRole.Admin);
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
          error = error.error;
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
      })
      .subscribe(
        (response: any) => {
          localStorage.removeItem('token');
          localStorage.removeItem('email');
          this.router.navigateByUrl('login');
        },
        (error: HttpErrorResponse) => {
          console.error('POST Request Error:', error);
          // // Handle any errors here
          this.errorMessage = error.message;
          this.success = false;
          this.showOutput = true;
        }
      );
  }
}
