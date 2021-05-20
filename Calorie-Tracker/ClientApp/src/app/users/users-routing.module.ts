import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { AuthGuardRole } from '../auth.guardRole';
import { CalorieschartComponent } from './calorieschart/calorieschart.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { EditprofileComponent } from './editprofile/editprofile.component';
import { ListComponent } from './list/list.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'profile/edit', component: EditprofileComponent, canActivate: [AuthGuard] },
  { path: 'profile/chart', component: CalorieschartComponent, canActivate: [AuthGuard] },
  { path: 'users/list', component: ListComponent, canActivate: [AuthGuardRole] },
  { path: 'users/:userId/edit', component: EditComponent, canActivate: [AuthGuardRole] },
  { path: 'users/:userId/details', component: DetailsComponent, canActivate: [AuthGuardRole] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
