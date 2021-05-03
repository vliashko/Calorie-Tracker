import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth.guard';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  { path: 'recipes/list', component: ListComponent, canActivate: [AuthGuard] },
  { path: 'recipes/:recipeId/edit', component: EditComponent, canActivate: [AuthGuard] },
  { path: 'recipes/create', component: CreateComponent, canActivate: [AuthGuard] },
  { path: 'recipes/:recipeId/details', component: DetailsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipesRoutingModule { }
