import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ExercisesService } from 'src/app/exercises/exercises.service';
import { ActivityForUpdateDto } from 'src/app/model/activityForUpdateDto';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { ActivitiesService } from '../activities.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {


  formGroup!: any;
  post: any = '';
  id!: string;
  activity!: ActivityForUpdateDto;

  public exercisesWithReps: any = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public activitiesService: ActivitiesService,
              public exercisesService: ExercisesService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router,
              private route: ActivatedRoute) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.activitiesService.getActivity(this.id, this.route.snapshot.params.activityId)
        .subscribe(act => {
          this.exercisesWithReps = this.exercisesService.apiExercisesGet()
          .pipe(map(data => data.map(((x: any) => ({...x, numberOfRepetitions: 0, numberOfSets: 0})))));
          this.exercisesWithReps.subscribe(
            (x: any) => {
              this.addGroupToForm(x, x.length, act);
            }
          );
        });
    });
  }
  // tslint:disable-next-line:typedef
  addGroupToForm(x: any, k: number, act: any) {
    const exercisesForm = this.formGroup.get('exercisesWithReps') as FormArray;
    for (let index = 0; index < k; index++) {
      exercisesForm.push(this.formBuilder.group({
        description: [''],
        caloriesSpent: [''],
        id: [''],
        name: [''],
        numberOfRepetitions: [''],
        numberOfSets: ['']
      }));
    }
    for (let index = 0; index < k; index++) {
      // tslint:disable-next-line:prefer-for-of
      for (let j = 0; j < act.exercisesWithReps.length; j++) {
        if (x[index].id === act.exercisesWithReps[j].exercise.id) {
          x[index].numberOfRepetitions = act.exercisesWithReps[j].numberOfRepetitions;
          x[index].numberOfSets = act.exercisesWithReps[j].numberOfSets;
        }
      }
    }
    act.moment = act.moment.split('T')[1].split('+')[0].slice(0, -3);
    this.formGroup.patchValue(act);
    exercisesForm.patchValue(x);
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      moment: [null, Validators.required],
      exercisesWithReps: this.formBuilder.array([])
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  // tslint:disable-next-line:typedef
  onSubmit(post: any) {
    this.post = post;
    this.activity = {
      name: post.name,
      moment: post.moment,
      exercisesWithReps: []
    };
    // tslint:disable-next-line:prefer-for-of
    for (let index = 0; index < post.exercisesWithReps.length; index++) {
      if (post.exercisesWithReps[index].numberOfRepetitions > 0 && post.exercisesWithReps[index].numberOfSets > 0) {
        this.activity.exercisesWithReps.push({
          exerciseId: post.exercisesWithReps[index].id,
          numberOfRepetitions: post.exercisesWithReps[index].numberOfRepetitions,
          numberOfSets: post.exercisesWithReps[index].numberOfSets
        });
      }
    }
    this.activitiesService.apiUsersUserIdActivitiesActivityIdPut(this.id, this.route.snapshot.params.activityId, this.activity)
      .subscribe(() => { this.router.navigateByUrl('activities/list');
  });
  }
}
