import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EatingsRoutingModule } from './eatings-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { DetailsComponent } from './details/details.component';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent, DetailsComponent],
  imports: [
    CommonModule,
    EatingsRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    MatDialogModule,
  ]
})
export class EatingsModule { }
