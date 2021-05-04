import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { User } from 'src/app/model/user';
import { UserProfile } from 'src/app/model/userProfile';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-editprofile',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofileComponent implements OnInit {

  id!: string;
  user: any;
  formGroup!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public usersService: UsersService,
              private route: ActivatedRoute,
              private router: Router,
              public authService: AuthenticationService,
              public userPr: UserProfilesService) {  }

  ngOnInit(): void {
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    const mainId = decoded.userId;
    this.userPr.apiUserprofilesGet(mainId).subscribe(res => {
      this.id = res.id;
      res.gender === 0 ? res.gender = 'Male' : res.gender = 'Female';
      this.formGroup.patchValue(res);
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
    formData.dateOfBirth = formatDate(formData.dateOfBirth, 'MM/dd/yyyy', 'en-US');
    this.usersService.apiUsersIdPut(this.id, formData).subscribe(res => {
      this.router.navigateByUrl('profile');
    });
  }
}
