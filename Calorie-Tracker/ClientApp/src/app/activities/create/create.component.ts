import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ExercisesService } from 'src/app/exercises/exercises.service';
import { ActivityForCreateDto } from 'src/app/model/activityForCreateDto';
import { Exercise } from 'src/app/model/exercise';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
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
              private router: Router) {  }

  ngOnInit(): void {
    this.displayedColumnsForForm = ['name', 'caloriesSpent', 'numberOfRepetitions', 'numberOfSets', 'actions'];
    this.displayedColumns = ['name', 'caloriesSpent', 'actions'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
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
  addGroupToForm(x: any, k: number): void {
    const exercisesForm = this.formGroup.get('exercisesWithReps') as FormArray;
    exercisesForm.push(this.formBuilder.group({
      description: [''],
      caloriesSpent: [''],
      id: [''],
      name: [''],
      numberOfRepetitions: [''],
      numberOfSets: ['']
    }));
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
  deleteExercise(name: string): void {
    const exercisesForm = this.formGroup.get('exercisesWithReps') as FormArray;
    exercisesForm.removeAt(this.exercisesWithReps.findIndex((x: any) => x.name === name));
    this.exercisesWithReps = this.exercisesWithReps.filter((x: any) => x.name !== name);
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
          name: post.exercisesWithReps[index].name,
          description: post.exercisesWithReps[index].description,
          caloriesSpent: post.exercisesWithReps[index].caloriesSpent,
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
