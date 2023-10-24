import { Component } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
} from '@angular/common/http';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  constructor(private http: HttpClient) {}
  createPerson() {
    const postData = {
      password: 'John Doe',
    };

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    // Make the POST request
    this.http.post('https://jsonplaceholder.typicode.com/posts', postData, {
      headers,
    });
  }
}
