import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css'],
})
export class AdminPanelComponent {
  usersUrl: string = 'admin/users';
  purchasesUrl: string = 'admin/purchases';

  constructor(private http: HttpClient, private router: Router) {
    this.confirmAdminRole();
  }

  confirmAdminRole() {
    const token: string | null = localStorage.getItem('token');
    const email: string | null = localStorage.getItem('email');
    if (token == null || email == null) {
      this.router.navigateByUrl('products');
    }
    const headers = new HttpHeaders().set('Authorization', token!);
    this.http
      .get(`${environment.API_HOST}${endpoints.users}/${email}`, {
        headers: headers,
      })
      .subscribe(
        (response) => {
          this.router.navigateByUrl('admin/users');
        },
        (error) => {
          console.log(error);
          this.router.navigateByUrl('products');
        }
      );
  }
}
