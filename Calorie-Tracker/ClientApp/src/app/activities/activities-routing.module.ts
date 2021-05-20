import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  { path: 'activities/list', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'activities/:activityId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'activities/create', component: CreateComponent, canActivate: [AuthGuard] },
  { path: 'activities/:activityId/details', component: DetailsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ActivitiesRoutingModule { }
