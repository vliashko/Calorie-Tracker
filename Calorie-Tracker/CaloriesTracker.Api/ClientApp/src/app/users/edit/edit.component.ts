import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  isError = false;
  error: string = '';
  formGroup!: FormGroup;

  constructor(public usersService: UsersService,
              public route: ActivatedRoute,
              public router: Router,
              private formBuilder: FormBuilder) {  }

  ngOnInit(): void {
    this.createForm();
    this.usersService.userById(this.route.snapshot.params.userId).subscribe(res => {
      res.userProfile.gender === 0 ? res.userProfile.gender = 'Male' : res.userProfile.gender = 'Female';
      const patch = {
        id: res.id,
        userName: res.userName,
        email: res.email,
        weight: res.userProfile.weight,
        height: res.userProfile.height,
        gender: res.userProfile.gender,
        dateOfBirth: res.userProfile.dateOfBirth
      };
      this.formGroup.patchValue(patch);
    });
  }
  createForm() {
    this.formGroup = this.formBuilder.group({
      id: [null],
      userName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      weight: [null, [Validators.required, Validators.min(35), Validators.max(350)]],
      height: [null, [Validators.required, Validators.min(120), Validators.max(250)]],
      gender: [null, Validators.required],
      dateOfBirth: [null, Validators.required]
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  onSubmit(update: any) {
    update.dateOfBirth = formatDate(update.dateOfBirth, 'MM/dd/yyyy', 'en-US');
    const putData = {
      userName: update.userName,
      email: update.email,
      userProfile: {
        weight: update.weight,
        height: update.height,
        gender: update.gender,
        dateOfBirth: update.dateOfBirth
      }
    };
    this.usersService.apiUsersIdPut(update.id, putData).subscribe(() => {
      this.router.navigateByUrl('/users/list');
    },
    error => {
      this.error = error.error;
      this.isError = true;
    });
  }
}
