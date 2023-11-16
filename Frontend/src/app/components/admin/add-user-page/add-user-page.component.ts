import { HttpClient, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { EUserRole } from 'src/app/types/EUserRole';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-add-user-page',
  templateUrl: './add-user-page.component.html',
  styleUrls: ['./add-user-page.component.css'],
})
export class AddUserPageComponent {
  response: string = '';
  emailValue: string = '';
  addressValue: string = '';
  passwordValue: string = '';
  rolesValue: EUserRole[] = [];
  role0: string = 'Admin';
  role1: string = 'Customer';
  role0Checked: boolean = false;
  role1Checked: boolean = true;
  selectedRole: string = '';
  success: boolean = true;
  errorMessage: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  getRoles(): EUserRole[] {
    let roles: EUserRole[] = [];
    if (this.role0Checked) roles.push(EUserRole.Admin);
    if (this.role1Checked) roles.push(EUserRole.Customer);
    return roles;
  }

  addUser() {
    this.success = true;
    this.rolesValue = this.getRoles();
    if (
      this.emailValue == '' ||
      this.addressValue == '' ||
      this.passwordValue == '' ||
      this.rolesValue.length == 0
    ) {
      this.errorMessage = 'Fields must be filled';
      this.success = false;
      return;
    }
    const postData = {
      email: this.emailValue,
      address: this.addressValue,
      password: this.passwordValue,
      roles: this.rolesValue,
    };

    // Define the HTTP headers if needed (e.g., for authentication)
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', `${localStorage.getItem('token')}`);

    // Make the POST request
    this.http
      .post(`${environment.API_HOST}${endpoints.users}`, postData, {
        headers,
      })
      .subscribe(
        (response: any) => {
          if (response.status == HttpStatusCode.Created) {
            this.success = true;
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
