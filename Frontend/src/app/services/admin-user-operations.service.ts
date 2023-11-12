import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdminUserOperationsService {
  private user?: User;
  constructor(private http: HttpClient) {}

  getSelectedUser() {
    return this.user;
  }
  setSelectedUser(user: User) {
    this.user = user;
  }

  deleteUser() {}
}
