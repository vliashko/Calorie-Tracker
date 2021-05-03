import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { Recipe } from 'src/app/model/recipe';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { RecipesService } from '../recipes.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  recipes: Recipe[] = [];
  displayedColumns: string[] = [];
  id!: string;

  constructor(public recipesService: RecipesService,
              public authService: AuthenticationService,
              private dialog: MatDialog,
              public userPr: UserProfilesService) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'instruction', 'totalCalories', 'actions'];
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.recipesService.apiUsersUserIdRecipesGet(res.id).subscribe(recipes => {
        this.recipes = recipes;
      });
    });
  }
  // tslint:disable-next-line:typedef
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
          this.recipes = this.recipes.filter(item => item.id !== id);
        });
      }
    });
  }
}
