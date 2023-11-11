import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpStatusCode,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { endpoints } from 'src/app/networking/endpoints';
import { AdminUserOperationsService } from 'src/app/services/admin-user-operations.service';
import { EUserRole } from 'src/app/types/EUserRole';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-edit-user-page',
  templateUrl: './edit-user-page.component.html',
  styleUrls: ['./edit-user-page.component.css'],
})
export class EditUserPageComponent {
  // response: string = '';
  user!: User;
  emailValue: string = '';
  addressValue: string = '';
  passwordValue: string = '';
  isAdmin: boolean = false;
  success: boolean = true;
  errorMessage: string = '';
  showOutput: boolean = false;
  loginUrl: string = 'login';
  adminUrl: string = 'admin';

  constructor(
    private http: HttpClient,
    private router: Router,
    private adminUserDetailsService: AdminUserOperationsService
  ) {}

  ngOnInit() {
    let user: User | undefined = this.adminUserDetailsService.getSelectedUser();
    if (user == undefined) {
      this.router.navigateByUrl('admin/users');
    }
    this.user = user!;
    this.emailValue = this.user.email;
    this.addressValue = this.user.address;
    this.passwordValue = this.user.password;
  }

  getUserData() {
    // Make the POST request
    const email: string | null = localStorage.getItem('email');
    const headers = new HttpHeaders().set(
      'Authorization',
      `${localStorage.getItem('token')!}`
    );
    this.http
      .get(`${environment.API_HOST}${endpoints.users}/${email}`, {
        headers: headers,
        observe: 'response',
      })
      .subscribe(
        (response: any) => {
          this.user = new User(
            response.body.email,
            response.body.address,
            response.body.password,
            response.body.roles
          );
          this.emailValue = this.user.email;
          this.addressValue = this.user.address;
          this.passwordValue = this.user.password;
          this.isAdmin = this.user.roles.includes(EUserRole.Admin);
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
        `${environment.API_HOST}${endpoints.users}/${this.user.email}`,
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
}
