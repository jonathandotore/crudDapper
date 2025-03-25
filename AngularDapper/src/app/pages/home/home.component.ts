import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserList } from '../../models/user';
import { cwd } from 'node:process';


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
      this.users = response.data;
      this.allUsers = response.data;
    })
  }

  search(event: Event)
  {
    const target = event.target as HTMLInputElement;
    const value = target.value.toLocaleLowerCase();

    this.users = this.allUsers.filter(user => {
      return user.fullName.toLocaleLowerCase().includes(value);
    });
  }
}
