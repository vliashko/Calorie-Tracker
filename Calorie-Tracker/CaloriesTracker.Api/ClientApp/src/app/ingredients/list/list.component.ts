import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
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

  constructor(public ingredientsService: IngredientsService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'actions'];
    this.ingredientsService.apiIngredientsGet().subscribe((data: Ingredient[]) => {
      this.ingredients = data;
    });
  }
  // tslint:disable-next-line:typedef
  deleteIngredient(id: string) {
    const dialogRef = this.dialog.open(ConfirmationDialog, {
      data: {
        message: 'Are you sure want to delete?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No'
        }
      }
    });
    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.ingredientsService.apiIngredientsIdDelete(id).subscribe(res => {
          this.ingredients = this.ingredients.filter(item => item.id !== id);
        });
      }
    });
  }
}
