import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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

  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      calories: [null, [Validators.required, Validators.min(0.01), Validators.max(500)]],
      proteins: [null, [Validators.required, Validators.min(0.01), Validators.max(150)]],
      fats: [null, [Validators.required, Validators.min(0.01), Validators.max(150)]],
      carbohydrates : [null, [Validators.required, Validators.min(0.01), Validators.max(150)]],
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  onSubmit(post: any) {
    this.post = post;
    this.ingredientsService.apiIngredientsPost(post)
      .subscribe(() => { this.router.navigateByUrl('ingredients/list');
  });
  }
}
