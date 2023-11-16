import { EUserRole } from '../types/EUserRole';

export class User {
  email: string;
  address: string;
  password: string;
  roles: EUserRole[];

  constructor(
    email: string,
    address: string,
    password: string,
    roles: EUserRole[]
  ) {
    this.email = email;
    this.address = address;
    this.password = password;
    this.roles = roles;
  }
}
