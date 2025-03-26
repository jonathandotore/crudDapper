import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Input() userData : UserList | null = null;
  @Input() btnAction!:string;
  @Input() descriptionTitle!: string;

  userForm!: FormGroup;

  ngOnInit(): void {

    this.userForm = new FormGroup({
      id: new FormControl(this.userData ? this.userData.id : 0),
      fullName: new FormControl(this.userData ? this.userData.fullName : ''),
      email: new FormControl(this.userData ? this.userData.email : ''),
      jobTitle: new FormControl(this.userData ? this.userData.jobTitle : ''),
      salary: new FormControl(this.userData ? this.userData.salary : 0),
      cpf: new FormControl(this.userData ? this.userData.cpf : ''),
      status: new FormControl(this.userData ? this.userData.status : true),
      password: new FormControl(this.userData ? this.userData.password : '')
    });
  }

  submit() {
    this.onSubmit.emit(this.userForm.value);
  }
}
