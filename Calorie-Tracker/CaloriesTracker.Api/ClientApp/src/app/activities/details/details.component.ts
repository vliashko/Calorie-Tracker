import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { Activity } from 'src/app/model/activity';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { ActivitiesService } from '../activities.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id!: string;
  idActivity!: string;
  activity: Activity = {
    id: '',
    name: '',
    moment: new Date(),
    userProfileId: '',
    totalCaloriesSpent: 0,
    exercisesWithReps: []
  };
  editForm!: FormGroup;
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public activityService: ActivitiesService,
              public authService: AuthenticationService,
              public userPr: UserProfilesService,
              private route: ActivatedRoute,
              private router: Router) {  }
   ngOnInit(): void {
    this.displayedColumns = ['name', 'description', 'caloriesSpent', 'numberOfRepetitions', 'numberOfSets'];
    this.createForm();
    this.idActivity = this.route.snapshot.params.activityId;
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.activityService.getActivity(this.idActivity, res.id).subscribe(activity => {
        this.activity = activity;
        this.editForm.patchValue(activity);
      });
    });
  }
  createForm() {
    this.editForm = this.formBuilder.group({
      name: [null],
      description: [null],
      caloriesSpent: [null],
      numberOfRepetitions: [null],
      numberOfSets: [null]
    });
  }
}
