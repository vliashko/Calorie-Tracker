import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ExercisesService } from '../exercises.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  formGroup!: FormGroup;
  post: any = '';

  constructor(private formBuilder: FormBuilder,
              public exercisesService: ExercisesService,
              private router: Router) {  }

  // tslint:disable-next-line:typedef
  ngOnInit() {
    this.createForm();
  }

  // tslint:disable-next-line:typedef
  createForm() {
    this.formGroup = this.formBuilder.group({
      name: [null, Validators.required],
      description: [null, Validators.required],
      caloriesSpent: [null, [Validators.required, Validators.min(0.01), Validators.max(10)]],
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.formGroup.controls[controlName].hasError(errorName);
  }
  // tslint:disable-next-line:typedef
  onSubmit(post: any) {
    this.post = post;
    this.exercisesService.apiExercisesPost(post)
      .subscribe(() => { this.router.navigateByUrl('exercises/list');
  });
  }
}
