import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EatingsRoutingModule } from './eatings-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { DetailsComponent } from './details/details.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';


@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent, DetailsComponent],
  imports: [
    FormsModule,
    CommonModule,
    EatingsRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    MatDialogModule,
    NgxMaterialTimepickerModule
  ]
})
export class EatingsModule { }
