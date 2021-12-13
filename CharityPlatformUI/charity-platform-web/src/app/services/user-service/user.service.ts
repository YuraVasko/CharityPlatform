import { UserDto, AuthUserDto } from 'src/app/models/user';
import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserDto[]>("http://localhost:4200/api/users");
  }

  deleteUser(id: string) {
    return this.http.delete("http://localhost:4200/api/users/" + id);
  }

  getUser(id: string) {
    return this.http.get<UserDto>("http://localhost:4200/api/users/" + id);
  }

  register(
    firstName: string,
    lastName: string,
    userName: string,
    password: string) {
    return this.http.post<UserDto>("http://localhost:4200/api/users/register",
      {
        userName: userName,
        password: password,
        firstName: firstName,
        lastName: lastName
      });
  }

  login(
    userName: string,
    password: string) {
    return this.http.post<AuthUserDto>("http://localhost:4200/api/users/authenticate",
      {
        userName: userName,
        password: password
      });
  }

  signOut() {

  }

  @Output() loggedEmmit: EventEmitter<any> = new EventEmitter();
}
