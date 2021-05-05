import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserProfileForCreateDto } from 'src/app/model/userProfileForCreateDto';
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

  constructor(private authService: AuthenticationService,
              private usersService: UsersService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }
    // tslint:disable-next-line:typedef
    createForm() {
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
        this.usersService.apiUsersPost(userPr).subscribe(() => {
          this.router.navigateByUrl('/profile/edit');
        });
      });
    });
  }

}
