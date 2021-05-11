import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ExercisesService } from 'src/app/exercises/exercises.service';
import { ActivityForUpdateDto } from 'src/app/model/activityForUpdateDto';
import { Exercise } from 'src/app/model/exercise';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
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
  loading = true;
  visible = false;
  event: any;
  searchName = '';

  dataSourceForForm: MatTableDataSource<Exercise> = new MatTableDataSource();
  dataSource: MatTableDataSource<Exercise> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  public exercisesWithReps: any = [];
  public exercises: any = [];
  displayedColumnsForForm: string[] = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public activitiesService: ActivitiesService,
              public exercisesService: ExercisesService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router,
              private route: ActivatedRoute) {  }

  ngOnInit(): void {
    this.displayedColumnsForForm = ['name', 'caloriesSpent', 'numberOfRepetitions', 'numberOfSets', 'actions'];
    this.displayedColumns = ['name', 'caloriesSpent', 'actions'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.activitiesService.getActivity(this.route.snapshot.params.activityId, this.id).subscribe(response => {
        response.moment = response.moment.split('T')[1].split('+')[0].slice(0, -3);
        this.formGroup.patchValue(response);
        this.addGroupToFormUpdate(response.exercisesWithReps, response.exercisesWithReps.length);
        this.dataSourceForForm = new MatTableDataSource(this.exercisesWithReps);
      })
    });
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number): void {
    this.exercisesService.apiExercisesPageNumberSizePageSizeParamsGet(pageSize, page, this.searchName).subscribe((res: any) => {
      this.loading = false;
      this.exercises = res.objects;
      this.exercises.length = res.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: res.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(this.exercises);
      this.paginator._intl.itemsPerPageLabel = 'Exercises Per Page:';
      this.dataSource.paginator = this.paginator;
    });
  }
  filterData() {
    this.getData(1, 5);
  }
  addGroupToFormUpdate(x: any, k: number): void {
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
    const tempArray = [];
    for (let index = 0; index < x.length; index++) {
    let obj: any = {};
    tempArray.push(obj = {
      id: x[index].exercise.id,
      caloriesSpent: x[index].exercise.caloriesSpent,
      description: x[index].exercise.description,
      name: x[index].exercise.name,
      numberOfRepetitions: x[index].numberOfRepetitions,
      numberOfSets: x[index].numberOfSets
    }) 
  }
    this.exercisesWithReps.push(...tempArray);
    if(this.exercisesWithReps.length > 0) {
      this.visible = true;
    }
    exercisesForm.patchValue(this.exercisesWithReps);
  }
  addGroupToForm(x: any, k: number): void {
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
    this.exercisesWithReps.push(x);
    exercisesForm.patchValue(this.exercisesWithReps);
  }
  pageChanged(event: any): void {
    this.event = event;
    this.loading = true;
    this.getNextData(event.pageSize * event.pageIndex, event.pageSize, event.pageIndex);
  }
  getNextData(currentSize: number, pageSize: number, page: number): void {
    this.exercisesService.apiExercisesPageNumberSizePageSizeParamsGet(pageSize, ++page, this.searchName).subscribe(response => {
      this.loading = false;
      this.exercises.length = currentSize;
      this.exercises.push(...response.objects);
      this.exercises.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.exercises);
      this.dataSource.paginator = this.paginator;
    });
  }
  addExercise(id: string): void {
    this.exercisesService.exerciseById(id).subscribe(response => {
      response = {
        id: response.id,
        name: response.name,
        description: response.description,
        caloriesSpent: response.caloriesSpent,
        numberOfRepetitions: 10,
        numberOfSets: 3
      };
      if (this.exercisesWithReps.find((x: any) => x.id === id) === undefined) {
        this.addGroupToForm(response, 1);
        this.dataSourceForForm = new MatTableDataSource(this.exercisesWithReps);
        this.visible = true;
      }
    });
  }
  deleteExercise(id: string): void {
    const exercisesForm = this.formGroup.get('exercisesWithReps') as FormArray;
    exercisesForm.removeAt(this.exercisesWithReps.findIndex((x: any) => x.id === id));
    this.exercisesWithReps = this.exercisesWithReps.filter((x: any) => x.id !== id);
    if (this.exercisesWithReps.length === 0) {
      this.visible = false;
    }
    this.dataSourceForForm = new MatTableDataSource(this.exercisesWithReps);
  }
  createForm(): void {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      moment: [null, Validators.required],
      exercisesWithReps: this.formBuilder.array([])
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  onSubmit(post: any): void {
    this.post = post;
    const date = new Date();
    const hours = this.post.moment.split(':')[0];
    const minutes = this.post.moment.split(':')[1];
    date.setHours(hours);
    date.setMinutes(minutes);
    date.setSeconds(0);
    this.activity = {
      name: post.name,
      moment: date,
      exercisesWithReps: []
    };
    for (let index = 0; index < post.exercisesWithReps.length; index++) {
      if (post.exercisesWithReps[index].numberOfRepetitions > 0 && post.exercisesWithReps[index].numberOfSets > 0) {
        this.activity.exercisesWithReps.push({
          exerciseId: post.exercisesWithReps[index].id,
          numberOfRepetitions: post.exercisesWithReps[index].numberOfRepetitions,
          numberOfSets: post.exercisesWithReps[index].numberOfSets
        });
      }
    }
    this.activitiesService.apiUsersUserIdActivitiesActivityIdPut(this.route.snapshot.params.activityId, this.id, this.activity)
      .subscribe(() => { this.router.navigateByUrl('activities/list');
    });
  }
}
