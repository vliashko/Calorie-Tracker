import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { Recipe } from 'src/app/model/recipe';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  displayedColumns: string[] = [];
  recipe: Recipe = {
    id: '',
    name: '',
    instruction: '',
    totalCalories: 0,
    userProfileId: '',
    ingredientsWithGrams: []
  };
  id!: string;
  editForm!: FormGroup;

  constructor(public recipesService: RecipesService,
              public authService: AuthenticationService,
              public userPr: UserProfilesService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'grams'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.recipesService.getRecipe(this.route.snapshot.params.recipeId, res.id).subscribe(recipe => {
        this.recipe = recipe;
        this.editForm.patchValue(recipe);
      });
    });
  }
  createForm() {
    this.editForm = this.formBuilder.group({
      name: [null],
      calories: [null],
      proteins: [null],
      fats: [null],
      carbohydrates: [null],
      grams: [null]
    });
  }

}
