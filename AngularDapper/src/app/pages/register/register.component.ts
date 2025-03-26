import { Component } from '@angular/core';
import { FormsComponent } from '../../components/forms/forms.component';
import { UserList } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent {

  btnAction = "Register";
  descriptionTitle = "Register new user";

  constructor(private userService: UserService, private router: Router) { }

  registerUser(user: UserList)
  {
    this.userService.RegisterUser(user).subscribe( response => {
      console.log(response);
      this.router.navigate(['/']);
    });
  }
}
