import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivitiesRoutingModule } from './activities-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { DetailsComponent } from './details/details.component';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent, DetailsComponent],
  imports: [
    CommonModule,
    ActivitiesRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    MaterialModule,
    NgxMaterialTimepickerModule
  ]
})
export class ActivitiesModule { }
