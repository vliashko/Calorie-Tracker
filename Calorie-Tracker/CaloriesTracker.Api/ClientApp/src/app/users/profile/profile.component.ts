import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { UserProfile } from 'src/app/model/userProfile';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user!: UserProfile;
  height!: number;

  constructor(private formBuilder: FormBuilder,
              public usersService: UsersService,
              public authService: AuthenticationService,
              private router: Router) {  }

  ngOnInit(): void {
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    const mainId = decoded.userId;
    this.getUser(mainId);
    console.log(this.user);
  }
  // tslint:disable-next-line:typedef
  getUser(mainId: string) {
    this.usersService.apiUsersGet().subscribe(res => {
      res.filter((x: { userId: string; }) => x.userId === mainId);
      this.user = res[0];
    });
  }
}
