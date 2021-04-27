import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ingredient, IngredientForCreateDto } from 'src/app/model/models';
import { IngredientsService } from '../ingredients.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent implements OnInit {

  formGroup!: FormGroup;
  titleAlert = 'This field is required';
  post: any = '';

  constructor(private formBuilder: FormBuilder,
              public ingredientsService: IngredientsService,
              private router: Router) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createForm();
    this.setValidate();
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      calories: [null, Validators.required],
      proteins: [null, Validators.required],
      fats: [null, Validators.required],
      carbohydrates : [null, Validators.required],
    });
  }
  // tslint:disable-next-line:typedef
  setValidate() {
    this.formGroup.get('name')?.setValidators(Validators.required);
    this.formGroup.get('calories')?.setValidators(Validators.required);
    this.formGroup.get('proteins')?.setValidators(Validators.required);
    this.formGroup.get('fats')?.setValidators(Validators.required);
    this.formGroup.get('carbohydrates')?.setValidators(Validators.required);
  }

  // tslint:disable-next-line:typedef
  onSubmit(post: any) {
    this.post = post;
    this.ingredientsService.apiIngredientsPost(post)
      .subscribe(res => { this.router.navigateByUrl('ingredients/list');
  });
  }
}
