import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ExercisesService } from 'src/app/exercises/exercises.service';
import { ActivityForCreateDto } from 'src/app/model/activityForCreateDto';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { ActivitiesService } from '../activities.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  formGroup!: any;
  post: any = '';
  id!: string;
  activity!: ActivityForCreateDto;

  public exercisesWithReps: any = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public activitiesService: ActivitiesService,
              public exercisesService: ExercisesService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      });
    this.exercisesWithReps = this.exercisesService.apiExercisesGet()
    .pipe(map(data => data.map(((x: any) => ({...x, numberOfRepetitions: 0, numberOfSets: 0})))));
    this.exercisesWithReps.subscribe(
      (x: any) => this.addGroupToForm(x, x.length)
    );
  }
  // tslint:disable-next-line:typedef
  addGroupToForm(x: any, k: number) {
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
    exercisesForm.patchValue(x);
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      start: [null, Validators.required],
      finish: [null, Validators.required],
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
      start: post.start,
      finish: post.finish,
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
    this.activitiesService.apiUsersUserIdActivitiesPost(this.id, this.activity)
      .subscribe(() => { this.router.navigateByUrl('activities/list');
  });
  }

}
