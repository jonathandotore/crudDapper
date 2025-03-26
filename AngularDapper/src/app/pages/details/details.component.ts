import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { UserService } from '../../services/user.service';
import { UserList } from '../../models/user';

@Component({
  selector: 'app-details',
  imports: [RouterModule],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})

export class DetailsComponent implements OnInit {

  user!:UserList;

  constructor(private userService: UserService, private route: ActivatedRoute) { }
  
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id') as string;

    this.userService.GetUserById(id).subscribe(response => {
      this.user = response.data;
    });
  }

}
