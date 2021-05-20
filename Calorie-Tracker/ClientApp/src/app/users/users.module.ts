import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';

import { ProfileComponent } from './profile/profile.component';
import { EditprofileComponent } from './editprofile/editprofile.component';
import { CalorieschartComponent } from './calorieschart/calorieschart.component';
import { DetailsComponent } from './details/details.component';

@NgModule({
  declarations: [ListComponent, EditComponent, ProfileComponent,
    EditprofileComponent, CalorieschartComponent, DetailsComponent],
  imports: [
    FormsModule,
    CommonModule,
    UsersRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    MatDialogModule,
    ChartsModule,
    WavesModule.forRoot(),
  ]
})
export class UsersModule { }
