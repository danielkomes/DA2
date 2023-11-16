import { Injectable } from '@angular/core';
import { IUserService } from '../interfaces/user-service.interface';
import { Observable } from 'rxjs';
import { Session } from '../types/Session';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { endpoints } from '../networking/endpoints';
import { UserLogin } from '../models/user-login';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService implements IUserService {
  constructor(private _httpService: HttpClient) {}

  public login(user: UserLogin): Observable<Session> {
    return this._httpService.post<Session>(
      `${environment.API_HOST}${endpoints.users}/${user.email}`,
      user.password
    );
  }
}
