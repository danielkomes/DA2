import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { endpoints } from 'src/app/networking/endpoints';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css'],
})
export class UsersListComponent {
  users!: User[];

  constructor(private http: HttpClient, private router: Router) {
    this.getUsers();
  }

  getUsers() {
    const token: string | null = localStorage.getItem('token');
    if (token == null) {
      this.router.navigateByUrl('products');
    }
    const headers = new HttpHeaders().set('Authorization', token!);
    this.http
      .get(`${environment.API_HOST}${endpoints.users}`, {
        headers: headers,
      })
      .subscribe(
        (response: any) => {
          this.users = response;
        },
        (error) => {
          console.log(error);
          this.router.navigateByUrl('products');
        }
      );
  }
}
