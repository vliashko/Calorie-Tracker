import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { CreateComponent } from './create/create.component';
import { AuthGuard } from '../auth.guard';

const routes: Routes = [
  { path: 'ingredients/list', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'ingredients/:ingredientId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'ingredients/create', component: CreateComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IngredientsRoutingModule { }
