export class UserInfo {
  email: string;
  address: string;
  password: string;

  constructor(email: string, address: string, password: string) {
    this.email = email;
    this.address = address;
    this.password = password;
  }
}
