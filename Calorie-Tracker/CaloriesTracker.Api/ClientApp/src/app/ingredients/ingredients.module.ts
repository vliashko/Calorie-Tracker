import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IngredientsRoutingModule } from './ingredients-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { CreateComponent } from './create/create.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [ListComponent, EditComponent, CreateComponent],
  imports: [
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    IngredientsRoutingModule,
    MaterialModule,
    MatDialogModule
  ]
})
export class IngredientsModule { }
