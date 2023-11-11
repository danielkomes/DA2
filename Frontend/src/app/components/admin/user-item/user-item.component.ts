import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AdminUserOperationsService } from 'src/app/services/admin-user-operations.service';
import { EUserRole } from 'src/app/types/EUserRole';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.css'],
})
export class UserItemComponent {
  user!: User;
  email!: string;
  address!: string;
  password!: string;
  roles: string[] = [];
  adminUserDetailsUrl!: string;

  @Input() userReceived!: User;

  constructor(
    private router: Router,
    private adminUserOperationsService: AdminUserOperationsService
  ) {}

  ngOnInit() {
    this.user = this.userReceived;
    this.email = this.user.email;
    this.address = this.user.address;
    this.password = this.user.password;
    this.user.roles.forEach((r) =>
      this.roles.push(r == 0 ? 'Admin' : 'Customer')
    );
    this.adminUserDetailsUrl = `admin/users/${this.email}`;
  }

  editUser() {
    this.adminUserOperationsService.setSelectedUser(this.user);
    this.router.navigateByUrl(this.adminUserDetailsUrl);
  }
}
