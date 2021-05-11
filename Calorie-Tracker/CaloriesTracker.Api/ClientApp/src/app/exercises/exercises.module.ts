import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExercisesRoutingModule } from './exercises-routing.module';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [CreateComponent, EditComponent, ListComponent],
  imports: [
    FormsModule,
    CommonModule,
    ExercisesRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    MaterialModule
  ]
})
export class ExercisesModule { }
