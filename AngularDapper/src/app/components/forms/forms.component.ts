import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms'
import { UserList } from '../../models/user';

@Component({
  selector: 'app-forms',
  imports: [RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './forms.component.html',
  styleUrl: './forms.component.css'
})

export class FormsComponent implements OnInit {

  @Output() onSubmit = new EventEmitter<UserList>();

  userForm!: FormGroup;

  ngOnInit(): void {
    this.userForm = new FormGroup({
      id: new FormControl(0),
      fullName: new FormControl(''),
      email: new FormControl(''),
      jobTitle: new FormControl(''),
      salary: new FormControl(0),
      cpf: new FormControl(''),
      status: new FormControl(true),
      password: new FormControl('')
    });
  }

  submit() {
    this.onSubmit.emit(this.userForm.value);
  }
}
