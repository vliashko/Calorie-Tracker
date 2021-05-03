import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { map } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { IngredientsService } from 'src/app/ingredients/ingredients.service';
import { IngredientEatingForUpdateDto } from 'src/app/model/ingredientEatingForUpdateDto';
import { RecipeForUpdateDto } from 'src/app/model/recipeForUpdateDto';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  formGroup!: any;
  post: any = '';
  id!: string;
  recipeUpdate: RecipeForUpdateDto = {
    name: '',
    instruction: '',
    ingredientsWithGrams: []
  };
  ingredients!: IngredientEatingForUpdateDto;

  public ingredientsWithGrams: any = [];
  displayedColumns: string[] = [];

  constructor(private formBuilder: FormBuilder,
              public recipesService: RecipesService,
              public ingredientsService: IngredientsService,
              public authService: AuthenticationService,
              public userPf: UserProfilesService,
              private router: Router,
              private route: ActivatedRoute) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.displayedColumns = ['name', 'calories', 'grams'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPf.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.recipesService.getRecipe(this.id, this.route.snapshot.params.recipeId)
        .subscribe(recipe => {
          this.ingredientsWithGrams = this.ingredientsService.apiIngredientsGet()
          .pipe(map(data => data.map(((x: any) => ({...x, grams: 0})))));
          this.ingredientsWithGrams.subscribe(
            (x: any) => {
              this.addGroupToForm(x, x.length, recipe);
            }
          );
        });
    });
  }
  // tslint:disable-next-line:typedef
  addGroupToForm(x: any, k: number, recipe: any) {
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
    for (let index = 0; index < k; index++) {
      // tslint:disable-next-line:prefer-for-of
      for (let j = 0; j < recipe.ingredientsWithGrams.length; j++) {
        if (x[index].id === recipe.ingredientsWithGrams[j].ingredient.id) {
          x[index].grams = recipe.ingredientsWithGrams[j].grams;
        }
      }
    }
    this.formGroup.patchValue(recipe);
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
    this.recipeUpdate = {
      name: post.name,
      instruction: post.instruction,
      ingredientsWithGrams: []
    };
    // tslint:disable-next-line:prefer-for-of
    for (let index = 0; index < post.ingredientsWithGrams.length; index++) {
      if (post.ingredientsWithGrams[index].grams > 0) {
        this.recipeUpdate.ingredientsWithGrams.push({
          ingredientId: post.ingredientsWithGrams[index].id,
          ingredient: {
            id: post.ingredientsWithGrams[index].id,
            name: post.ingredientsWithGrams[index].name,
            calories: post.ingredientsWithGrams[index].calories,
            proteins: post.ingredientsWithGrams[index].proteins,
            fats: post.ingredientsWithGrams[index].fats,
            carbohydrates: post.ingredientsWithGrams[index].carbohydrates
          },
          grams: post.ingredientsWithGrams[index].grams
        });
      }
    }
    this.recipesService.apiUsersUserIdRecipesRecipeIdPut(this.id, this.route.snapshot.params.recipeId, this.recipeUpdate)
      .subscribe(() => { this.router.navigateByUrl('recipes/list');
  });
  }
}
