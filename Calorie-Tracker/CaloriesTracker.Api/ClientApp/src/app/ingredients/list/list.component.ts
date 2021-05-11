import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
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
  loading = true;
  event: any;
  searchName = '';
  displayedColumns: string[] = [];
  dataSource: MatTableDataSource<Ingredient> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(public ingredientsService: IngredientsService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'actions'];
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number) {
    this.ingredientsService.apiIngredientsPageNumberSizePageSizeParamsGet(pageSize, page, this.searchName).subscribe((res: any) => {
      this.loading = false;
      this.ingredients = res.objects;
      this.ingredients.length = res.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: res.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(res.objects);
      this.paginator._intl.itemsPerPageLabel = 'Ingredients Per Page:';
      this.dataSource.paginator = this.paginator;
    });
  }
  pageChanged(event: any) {
    this.event = event;
    this.loading = true;
    this.getNextData(event.pageSize * event.pageIndex, event.pageSize, event.pageIndex);
  }
  filterData() {
    this.getData(1, 5);
  }
  getNextData(currentSize: number, pageSize: number, page: number) {
    this.ingredientsService.apiIngredientsPageNumberSizePageSizeParamsGet(pageSize, ++page, this.searchName).subscribe(response => {
      this.loading = false;
      this.ingredients.length = currentSize;
      this.ingredients.push(...response.objects);
      this.ingredients.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.ingredients);
      this.dataSource.paginator = this.paginator;
    });
  }
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
          this.loading = true;
          this.paginator.pageIndex = 0;
          this.getData(1, this.paginator.pageSize);
        });
      }
    });
  }
}
