import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EatingsRoutingModule } from './eatings-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';


@NgModule({
  declarations: [ListComponent, CreateComponent, EditComponent],
  imports: [
    CommonModule,
    EatingsRoutingModule
  ]
})
export class EatingsModule { }
