import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserList } from '../../models/user';


@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  users: UserList[] = [];
  allUsers: UserList[] = [];
  constructor(private userService:UserService){
  }
    
  ngOnInit(): void {
    this.userService.GetUsers().subscribe(response => {
      this.users = response.dados;
      this.allUsers = response.dados;
      console.log(response);
    })
  }
}
