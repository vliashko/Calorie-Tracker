import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { CalorieschartComponent } from './calorieschart/calorieschart.component';
import { CreateComponent } from './create/create.component';
import { CreateprofileComponent } from './createprofile/createprofile.component';
import { EditComponent } from './edit/edit.component';
import { EditprofileComponent } from './editprofile/editprofile.component';
import { ListComponent } from './list/list.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'profile/create', component: CreateprofileComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/edit', component: EditprofileComponent, canActivate: [AuthGuard] },
  { path: 'profile/chart', component: CalorieschartComponent, canActivate: [AuthGuard] },
  { path: 'users', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'users/:userId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'users/create', component: CreateComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
