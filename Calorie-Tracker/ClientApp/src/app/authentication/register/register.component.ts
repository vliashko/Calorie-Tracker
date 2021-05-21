import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserProfileForCreateDto } from 'src/app/model/userProfileForCreateDto';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { UsersService } from 'src/app/users/users.service';
import { AuthenticationService } from '../authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form!: FormGroup;
  hide = true;
  isError = false;
  error = '';

  constructor(private authService: AuthenticationService,
              private usersService: UserProfilesService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }
    createForm(): void {
      this.form = this.formBuilder.group({
        username: [null, Validators.required],
        email: [null, [Validators.required, Validators.email]],
        password: [null, [Validators.required, Validators.minLength(10)]],
      });
    }
    public checkError = (controlName: string, errorName: string) => {
      return this.form.controls[controlName].hasError(errorName);
    }
  onSubmit(result: any): void {
    this.authService.apiAuthenticationPost(result).subscribe(() => {
      const login: any = {
        username: result.username,
        password: result.password
      };
      this.authService.apiAuthenticationLoginPost(login).subscribe(res => {
        localStorage.setItem('access_token', JSON.stringify(res));
        const userPr: UserProfileForCreateDto = {
          weight: 35,
          height: 120,
          gender: 0,
          dateOfBirth: new Date()
        };
        const token = this.authService.getToken();
        const decoded: any = jwtDecode(token);
        this.usersService.apiUserprofilesIdPost(userPr).subscribe(() => {
          this.router.navigateByUrl('/profile/edit');
        });
      });
    },
    (error) => {
      this.error = error.error;
      this.isError = true;
    });
  }

}
