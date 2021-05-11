import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ActivitiesService } from './activities/activities.service';
import { AuthenticationService } from './authentication/authentication.service';
import { EatingsService } from './eatings/eatings.service';
import { ExercisesService } from './exercises/exercises.service';
import { IngredientsService } from './ingredients/ingredients.service';
import { RecipesService } from './recipes/recipes.service';
import { UsersService } from './users/users.service';
import { UserProfilesService } from './users/userProfiles.service';

import { AuthenticationModule } from './authentication/authentication.module';
import { EatingsModule } from './eatings/eatings.module';
import { ExercisesModule } from './exercises/exercises.module';
import { IngredientsModule } from './ingredients/ingredients.module';
import { RecipesModule } from './recipes/recipes.module';
import { ActivitiesModule } from './activities/activities.module';
import { UsersModule } from './users/users.module';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmationDialog } from './confirmation-dialog.component';
import { HomeComponent } from './home/home.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { ChartsModule, WavesModule } from 'angular-bootstrap-md';

import { AuthInterceptor } from './authconfig.interceptor';

@NgModule({
  declarations: [
    AppComponent, ConfirmationDialog, HomeComponent
  ],
  imports: [
    MatDialogModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    AuthenticationModule,
    EatingsModule,
    ExercisesModule,
    IngredientsModule,
    RecipesModule,
    ActivitiesModule,
    UsersModule,
    NgxMaterialTimepickerModule,
    ChartsModule,
    WavesModule.forRoot(),
  ],
  providers: [
    ActivitiesService,
    AuthenticationService,
    EatingsService,
    ExercisesService,
    IngredientsService,
    RecipesService,
    UsersService,
    UserProfilesService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
   ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
