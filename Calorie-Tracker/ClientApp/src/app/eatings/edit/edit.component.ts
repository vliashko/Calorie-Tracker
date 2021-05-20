import { formatDate } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { IngredientsService } from 'src/app/ingredients/ingredients.service';
import { EatingForUpdateDto } from 'src/app/model/eatingForUpdateDto';
import { Ingredient } from 'src/app/model/ingredient';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { EatingsService } from '../eatings.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  formGroup!: any;
  post: any = '';
  id!: string;
  eating!: EatingForUpdateDto;
  loading = true;
  visible = false;
  event: any;
  searchName = '';

  dataSourceForForm: MatTableDataSource<Ingredient> = new MatTableDataSource();
  dataSource: MatTableDataSource<Ingredient> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  public ingredientsWithGrams: any = [];
  public ingredients: any = [];
  displayedColumnsForForm: string[] = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public eatingsService: EatingsService,
              public ingredientsService: IngredientsService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router,
              private route: ActivatedRoute) {  }

  ngOnInit(): void {
    this.displayedColumnsForForm = this.displayedColumns = ['name', 'calories', 'grams', 'actions'];
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'actions'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.eatingsService.getEating(this.route.snapshot.params.eatingId, this.id).subscribe(response => {
        response.moment = formatDate(response.moment, 'HH:mm', 'en-US');
        this.formGroup.patchValue(response);
        this.addGroupToFormUpdate(response.ingredientsWithGrams, response.ingredientsWithGrams.length);
        this.dataSourceForForm = new MatTableDataSource(this.ingredientsWithGrams);
      });
    });
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number): void {
    this.ingredientsService.apiIngredientsPageNumberSizePageSizeParamsGet(pageSize, page, this.searchName).subscribe((res: any) => {
      this.loading = false;
      this.ingredients = res.objects;
      this.ingredients.length = res.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: res.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(this.ingredients);
      this.paginator._intl.itemsPerPageLabel = 'Ingredients Per Page:';
      this.dataSource.paginator = this.paginator;
    });
  }
  filterData(): void {
    this.getData(1, 5);
  }
  addGroupToFormUpdate(x: any, k: number): void {
    const exercisesForm = this.formGroup.get('ingredientsWithGrams') as FormArray;
    for (let index = 0; index < k; index++) {
      exercisesForm.push(this.formBuilder.group({
        proteins: [''],
        calories: [''],
        name: [''],
        fats: [''],
        carbohydrates: [''],
        grams: ['']
      }));
    }
    const tempArray = [];
    for (let index = 0; index < x.length; index++) {
      let obj = {};
      tempArray.push(obj = {
        proteins: x[index].proteins,
        calories: x[index].calories,
        name: x[index].name,
        fats: x[index].fats,
        carbohydrates: x[index].carbohydrates,
        grams: x[index].grams
      });
    }
      this.ingredientsWithGrams.push(...tempArray);
    if(this.ingredientsWithGrams.length > 0) {
      this.visible = true;
    }
    exercisesForm.patchValue(this.ingredientsWithGrams);
  }
  addGroupToForm(x: any): void {
    const exercisesForm = this.formGroup.get('ingredientsWithGrams') as FormArray;
    exercisesForm.push(this.formBuilder.group({
      proteins: [''],
      calories: [''],
      id: [''],
      name: [''],
      fats: [''],
      carbohydrates: [''],
      grams: ['']
    }));
    this.ingredientsWithGrams.push(x);
    exercisesForm.patchValue(this.ingredientsWithGrams);
  }
  pageChanged(event: any): void {
    this.event = event;
    this.loading = true;
    this.getNextData(event.pageSize * event.pageIndex, event.pageSize, event.pageIndex);
  }
  getNextData(currentSize: number, pageSize: number, page: number): void {
    this.ingredientsService.apiIngredientsPageNumberSizePageSizeParamsGet(pageSize, ++page, this.searchName).subscribe(response => {
      this.loading = false;
      this.ingredients.length = currentSize;
      this.ingredients.push(...response.objects);
      this.ingredients.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.ingredients);
      this.dataSource.paginator = this.paginator;
    });
  }
  addIngredient(name: string, id: string): void {
    this.ingredientsService.ingredientById(id).subscribe(response => {
      response = {
        name: response.name,
        carbohydrates: response.carbohydrates,
        calories: response.calories,
        proteins: response.proteins,
        fats: response.fats,
        grams: 100
      };
      if (this.ingredientsWithGrams.find((x: any) => x.name === name) === undefined) {
        this.addGroupToForm(response);
        this.dataSourceForForm = new MatTableDataSource(this.ingredientsWithGrams);
        this.visible = true;
      }
    });
  }
  deleteExercise(name: string): void {
    const exercisesForm = this.formGroup.get('ingredientsWithGrams') as FormArray;
    exercisesForm.removeAt(this.ingredientsWithGrams.findIndex((x: any) => x.name === name));
    this.ingredientsWithGrams = this.ingredientsWithGrams.filter((x: any) => x.name !== name);
    if (this.ingredientsWithGrams.length === 0) {
      this.visible = false;
    }
    this.dataSourceForForm = new MatTableDataSource(this.ingredientsWithGrams);
  }
  createForm(): void {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      moment: [null, Validators.required],
      ingredientsWithGrams: this.formBuilder.array([])
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
    this.eating = {
      name: post.name,
      moment: date,
      ingredientsWithGrams: []
    };
    for (let index = 0; index < post.ingredientsWithGrams.length; index++) {
      if (post.ingredientsWithGrams[index].grams > 0) {
        this.eating.ingredientsWithGrams.push({
          name: post.ingredientsWithGrams[index].name,
          calories: post.ingredientsWithGrams[index].calories,
          proteins: post.ingredientsWithGrams[index].proteins,
          fats: post.ingredientsWithGrams[index].fats,
          carbohydrates: post.ingredientsWithGrams[index].carbohydrates,
          grams: post.ingredientsWithGrams[index].grams
        });
      }
    }
    this.eatingsService.apiUsersUserIdEatingsEatingIdPut(this.route.snapshot.params.eatingId, this.id, this.eating)
      .subscribe(() => { this.router.navigateByUrl('eatings/list');
    });
  }
}
