import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { ExercisesService } from '../exercises.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id!: string;
  exercise?: Exercise;
  editForm!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              public exerciseService: ExercisesService,
              private route: ActivatedRoute,
              private router: Router) {  }

  ngOnInit(): void {
    this.createForm();
    this.id = this.route.snapshot.params.exerciseId;
    this.exerciseService.exerciseById(this.id).subscribe((data: Exercise) => {
      this.exercise = data;
      this.editForm.patchValue(data);
    });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.editForm.controls[controlName].hasError(errorName);
  }
  createForm() {
    this.editForm = this.formBuilder.group({
      name: [null, Validators.required],
      description: [null, Validators.required],
      caloriesSpent: [null, [Validators.required, Validators.min(0.01), Validators.max(10)]],
    });
  }

  onSubmit(formData: any) {
    this.exerciseService.apiExercisesIdPut(this.id, formData).subscribe(res => {
      this.router.navigateByUrl('exercises/list');
    });
  }
}
