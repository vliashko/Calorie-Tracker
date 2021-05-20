import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { Recipe } from 'src/app/model/recipe';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  recipes: Recipe[] = [];
  loading = true;
  event: any;
  displayedColumns: string[] = [];
  dataSource: MatTableDataSource<Recipe> = new MatTableDataSource();
  id!: string;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(public recipesService: RecipesService,
              public userPr: UserProfilesService,
              public authService: AuthenticationService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'instruction', 'totalCalories', 'actions'];
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number) {
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.recipesService.apiUsersUserIdRecipesPageNumberSizePageSizeGet(res.id, pageSize, page).subscribe((response: any) => {
      this.loading = false;
      this.recipes = response.objects;
      this.recipes.length = response.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: response.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(this.recipes);
      this.paginator._intl.itemsPerPageLabel = 'Recipes Per Page:';
      this.dataSource.paginator = this.paginator;
    });
    });
  }
  pageChanged(event: any) {
    this.event = event;
    this.loading = true;
    this.getNextData(event.pageSize * event.pageIndex, event.pageSize, event.pageIndex);
  }
  getNextData(currentSize: number, pageSize: number, page: number) {
    this.recipesService.apiUsersUserIdRecipesPageNumberSizePageSizeGet(this.id, pageSize, ++page).subscribe(response => {
      this.loading = false;
      this.recipes.length = currentSize;
      this.recipes.push(...response.objects);
      this.recipes.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.recipes);
      this.dataSource.paginator = this.paginator;
    });
  }
  deleteRecipe(id: string) {
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
        this.recipesService.apiUsersUserIdRecipesRecipeIdDelete(this.id, id).subscribe(res => {
          this.loading = true;
          this.paginator.pageIndex = 0;
          this.getData(1, this.paginator.pageSize);
        });
      }
    });
  }
}
