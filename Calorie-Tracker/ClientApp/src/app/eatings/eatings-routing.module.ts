import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  { path: 'eatings/list', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'eatings/:eatingId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'eatings/create', component: CreateComponent, canActivate: [AuthGuard] },
  { path: 'eatings/:eatingId/details', component: DetailsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EatingsRoutingModule { }
