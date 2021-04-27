import { Component, OnInit, ViewChild } from '@angular/core';
import { Ingredient } from 'src/app/model/ingredient';
import { IngredientsService } from '../ingredients.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  ingredients: Ingredient[] = [];
  displayedColumns: string[] = [];

  constructor(public ingredientsService: IngredientsService) { }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'actions'];
    this.ingredientsService.apiIngredientsGet().subscribe((data: Ingredient[]) => {
      this.ingredients = data;
    });
  }
  // tslint:disable-next-line:typedef
  deleteIngredient(id: string) {
    this.ingredientsService.apiIngredientsIdDelete(id).subscribe(res => {
      this.ingredients = this.ingredients.filter(item => item.id !== id);
    });
  }
}
