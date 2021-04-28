import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  { path: 'exercises/list', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'exercises/:exerciseId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'exercises/create', component: CreateComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExercisesRoutingModule { }
