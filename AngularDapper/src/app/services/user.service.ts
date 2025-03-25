import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserList } from '../models/user';
import { Response } from '../models/response'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  ApiUrl = environment.UrlApi;

  constructor(private http : HttpClient) { }

  GetUsers() : Observable<Response<UserList[]>>
  {
    return this.http.get<Response<UserList[]>> (`${this.ApiUrl}User`)
  }

  DeleteUser(id:string) : Observable<Response<UserList[]>>
  {
    return this.http.delete<Response<UserList[]>> (`${this.ApiUrl}User/?id=${id}`);
  }
}
