import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IngredientsRoutingModule } from './ingredients-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { CreateComponent } from './create/create.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';


@NgModule({
  declarations: [ListComponent, EditComponent, CreateComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    IngredientsRoutingModule,
    MaterialModule
  ]
})
export class IngredientsModule { }
