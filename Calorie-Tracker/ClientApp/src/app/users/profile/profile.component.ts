import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { ActivitiesService } from 'src/app/activities/activities.service';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { EatingsService } from 'src/app/eatings/eatings.service';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  id!: string;
  formGroup!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public eatingsService: EatingsService,
              public activitesService: ActivitiesService,
              public usersService: UsersService,
              public authService: AuthenticationService,
              private router: Router,
              public userPr: UserProfilesService) {  }

  ngOnInit(): void {
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.activitesService.apiActivitiesCountDaysGet(1, res.id).subscribe(data1 => {
        this.eatingsService.apiEatingsCountDaysGet(1, res.id).subscribe(data2 => {
          res.currentCalories = data2[0].currentCalories - data1[0].currentCalories;
          res.dateOfBirth = formatDate(res.dateOfBirth, 'MM/dd/yyyy', 'en-US');
          res.gender === 0 ? res.gender = 'Male' : res.gender = 'Female';
          this.formGroup.patchValue(res);
        });
      });
    });
  }
  createForm(): void {
    this.formGroup = this.formBuilder.group({
      weight: [null],
      height: [null],
      gender: [null],
      dateOfBirth: [null],
      currentCalories: [null],
      calories: [null]
    });
  }
}
