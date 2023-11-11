import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
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

  @Input() userReceived!: User;

  constructor(private router: Router) {}

  ngOnInit() {
    this.user = this.userReceived;
    this.email = this.user.email;
    this.address = this.user.address;
    this.password = this.user.password;
    this.user.roles.forEach((r) =>
      this.roles.push(r == 0 ? 'Admin' : 'Customer')
    );
  }
}
