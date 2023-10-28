import { Component, ViewChild, ElementRef } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
} from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  response: string = '';
  emailValue: string = '';
  passwordValue: string = '';
  @ViewChild('inputEmail') emailButton!: ElementRef;

  constructor(private http: HttpClient) {}

  ngAfterViewInit() {
    //   this.emailButton.nativeElement.addEventListener('click', () => {
    // this.login();
    //   });
  }

  login() {
    // const postData = {
    //   password: this.passwordValue,
    // };
    const postData = `\"${this.passwordValue}\"`;
    console.log(postData);
    var emailElement = document.getElementById('inputEmail');
    const email = emailElement?.innerText;
    const password = document.getElementById('inputPassword')?.textContent;

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    headers.set('Access-Control-Allow-Origin', 'true');

    // Make the POST request
    this.http
      .post('https://localhost:5001/api/session/' + this.emailValue, postData, {
        headers,
      })
      .subscribe(
        (response: any) => {
          console.log('POST Request Successful:', response);
          this.response = JSON.stringify(response, null, 2);
          // Handle the response data here
        },
        (error) => {
          console.error('POST Request Error:', error);
          // Handle any errors here
        }
      );
  }
}
