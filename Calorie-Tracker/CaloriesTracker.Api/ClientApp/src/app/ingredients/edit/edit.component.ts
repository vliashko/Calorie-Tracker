import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ingredient } from 'src/app/model/ingredient';
import { IngredientsService } from '../ingredients.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id!: string;
  ingredient?: Ingredient;
  editForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public ingredientsService: IngredientsService,
              private route: ActivatedRoute,
              private router: Router) {  }

  ngOnInit(): void {
    this.createForm();
    this.setValidate();
    this.id = this.route.snapshot.params.ingredientId;
    this.ingredientsService.ingredientById(this.id).subscribe((data: Ingredient) => {
      this.ingredient = data;
      this.editForm.patchValue(data);
    });
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.editForm = this.formBuilder.group({
      name: [null, Validators.required],
      calories: [null, Validators.required],
      proteins: [null, Validators.required],
      fats: [null, Validators.required],
      carbohydrates : [null, Validators.required],
    });
  }
  // tslint:disable-next-line:typedef
  setValidate() {
    this.editForm.get('name')?.setValidators(Validators.required);
    this.editForm.get('calories')?.setValidators(Validators.required);
    this.editForm.get('proteins')?.setValidators(Validators.required);
    this.editForm.get('fats')?.setValidators(Validators.required);
    this.editForm.get('carbohydrates')?.setValidators(Validators.required);
  }

  // tslint:disable-next-line:typedef
  onSubmit(formData: any) {
    this.ingredientsService.apiIngredientsIdPut(this.id, formData).subscribe(res => {
      this.router.navigateByUrl('ingredients/list');
    });
  }
}
