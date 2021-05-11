import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material-module';
import { MatDialogModule } from '@angular/material/dialog';


@NgModule({
  declarations: [RegisterComponent, LoginComponent],
  imports: [
    FormsModule,
    CommonModule,
    AuthenticationRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    MaterialModule
  ]
})
export class AuthenticationModule { }
