import { Observable } from 'rxjs';
import { Session } from '../types/Session';
import { UserLogin } from '../models/user-login';

export interface IUserService {
  login(user: UserLogin): Observable<Session>;
}
