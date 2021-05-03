import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { AuthGuardRole } from '../auth.guardRole';

const routes: Routes = [
  { path: 'exercises/list', component: ListComponent, canActivate: [AuthGuardRole] },
  { path: 'exercises/:exerciseId/edit', component: EditComponent, canActivate: [AuthGuardRole] },
  { path: 'exercises/create', component: CreateComponent, canActivate: [AuthGuardRole] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExercisesRoutingModule { }
