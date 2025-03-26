import { Component } from '@angular/core';
import { FormsComponent } from "../../components/forms/forms.component";
import { UserService } from '../../services/user.service';
import { UserList } from '../../models/user';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit',
  imports: [FormsComponent, CommonModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})

export class EditComponent {

  user!: UserList;
  btnAction = "Edit";
  descriptionTitle = "Edit User";


  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    
    const id = this.route.snapshot.paramMap.get('id') as string;

    this.userService.GetUserById(id).subscribe(response => {
      this.user = response.data;
    });
  }

  editUser(user:UserList)
  {
    this.userService.EditUser(user).subscribe(response => {
      this.user = response.data;
    });
  }
}
