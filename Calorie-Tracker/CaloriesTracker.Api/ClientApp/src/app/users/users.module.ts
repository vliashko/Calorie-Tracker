import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';
import { ProfileComponent } from './profile/profile.component';
import { CreateprofileComponent } from './createprofile/createprofile.component';
import { EditprofileComponent } from './editprofile/editprofile.component';

@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent, ProfileComponent, CreateprofileComponent, EditprofileComponent],
  imports: [
    CommonModule,
    UsersRoutingModule,
    ReactiveFormsModule,
    MaterialModule,
    MatDialogModule
  ]
})
export class UsersModule { }
