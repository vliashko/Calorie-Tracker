import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { IngredientsService } from 'src/app/ingredients/ingredients.service';
import { RecipeForCreateDto } from 'src/app/model/recipeForCreateDto';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  formGroup!: any;
  post: any = '';
  id!: string;
  recipe!: RecipeForCreateDto;

  public ingredientsWithGrams: any = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public recipesService: RecipesService,
              public ingredientsService: IngredientsService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.displayedColumns = ['name', 'calories', 'grams'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      });
    this.ingredientsWithGrams = this.ingredientsService.apiIngredientsGet()
    .pipe(map(data => data.map(((x: any) => ({...x, grams: 0})))));
    this.ingredientsWithGrams.subscribe(
      (x: any) => this.addGroupToForm(x, x.length)
    );
  }
  // tslint:disable-next-line:typedef
  addGroupToForm(x: any, k: number) {
    const ingredientsForm = this.formGroup.get('ingredientsWithGrams') as FormArray;
    for (let index = 0; index < k; index++) {
      ingredientsForm.push(this.formBuilder.group({
        calories: [''],
        carbohydrates: [''],
        fats: [''],
        grams: [''],
        id: [''],
        name: [''],
        proteins: ['']
      }));
    }
    ingredientsForm.patchValue(x);
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      instruction: [null, Validators.required],
      ingredientsWithGrams: this.formBuilder.array([])
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  // tslint:disable-next-line:typedef
  onSubmit(post: any) {
    this.post = post;
    this.recipe = {
      name: post.name,
      instruction: post.instruction,
      ingredientsWithGrams: []
    };
    // tslint:disable-next-line:prefer-for-of
    for (let index = 0; index < post.ingredientsWithGrams.length; index++) {
      if (post.ingredientsWithGrams[index].grams > 0) {
        this.recipe.ingredientsWithGrams.push({
          ingredientId: post.ingredientsWithGrams[index].id,
          grams: post.ingredientsWithGrams[index].grams
        });
      }
    }
    this.recipesService.apiUsersUserIdRecipesPost(this.id, this.recipe)
      .subscribe(() => { this.router.navigateByUrl('recipes/list');
  });
  }
}
