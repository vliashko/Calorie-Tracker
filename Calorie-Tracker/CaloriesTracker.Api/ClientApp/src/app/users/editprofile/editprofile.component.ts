import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/model/user';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-editprofile',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofileComponent implements OnInit {

  id!: string;
  user?: User;
  formGroup!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public usersService: UsersService,
              private route: ActivatedRoute,
              private router: Router) {  }

  ngOnInit(): void {
    this.createForm();
    this.id = this.route.snapshot.params.userId;
    this.usersService.userById(this.id).subscribe((data: User) => {
      this.user = data;
      this.formGroup.patchValue(data);
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
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

  // tslint:disable-next-line:typedef
  onSubmit(formData: any) {
    this.usersService.apiUsersIdPut(this.id, formData).subscribe(res => {
      this.router.navigateByUrl('profile');
    });
  }
}
