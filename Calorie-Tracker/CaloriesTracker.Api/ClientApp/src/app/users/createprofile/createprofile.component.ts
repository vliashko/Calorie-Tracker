import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-createprofile',
  templateUrl: './createprofile.component.html',
  styleUrls: ['./createprofile.component.css']
})
export class CreateprofileComponent implements OnInit {

  formGroup!: FormGroup;
  titleAlert = 'This field is required';
  post: any = '';

  constructor(private formBuilder: FormBuilder,
              public usersService: UsersService,
              private router: Router) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createForm();
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      weight: [null, [Validators.required, Validators.min(35), Validators.max(350)]],
      height: [null, [Validators.required, Validators.min(120), Validators.max(250)]],
      gender: [null, Validators.required],
      dateOfBirth: [null, Validators.required]
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  // tslint:disable-next-line:typedef
  onSubmit(post: any) {
    post.dateOfBirth = JSON.parse(JSON.stringify(post.dateOfBirth)).split('T')[0];
    this.post = post;
    this.usersService.apiUsersPost(post)
      .subscribe(() => {
        this.router.navigateByUrl('/');
  });
  }
}
