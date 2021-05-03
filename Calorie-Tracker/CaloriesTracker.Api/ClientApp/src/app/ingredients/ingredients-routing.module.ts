import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { CreateComponent } from './create/create.component';
import { AuthGuardRole } from '../auth.guardRole';

const routes: Routes = [
  { path: 'ingredients/list', component: ListComponent, canActivate: [AuthGuardRole] },
  { path: 'ingredients/:ingredientId/edit', component: EditComponent, canActivate: [AuthGuardRole] },
  { path: 'ingredients/create', component: CreateComponent, canActivate: [AuthGuardRole] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IngredientsRoutingModule { }
