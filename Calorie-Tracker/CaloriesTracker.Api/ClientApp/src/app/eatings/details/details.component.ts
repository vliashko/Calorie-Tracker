import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { IngredientsService } from 'src/app/ingredients/ingredients.service';
import { Eating } from 'src/app/model/eating';
import { Ingredient } from 'src/app/model/ingredient';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { UsersService } from 'src/app/users/users.service';
import { EatingsService } from '../eatings.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  displayedColumns: string[] = [];
  eating!: Eating;
  id!: string;
  editForm!: FormGroup;

  constructor(public eatingsService: EatingsService,
              public authService: AuthenticationService,
              public userPr: UserProfilesService,
              private route: ActivatedRoute,
              private formBuilder: FormBuilder) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'grams'];
    this.createForm();
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.eatingsService.getEating(res.id, this.route.snapshot.params.eatingId).subscribe(eating => {
        this.eating = eating;
        this.editForm.patchValue(eating);
      });
    });
  }
  // tslint:disable-next-line:typedef
  createForm() {
    this.editForm = this.formBuilder.group({
      name: [null],
      calories: [null],
      proteins: [null],
      fats: [null],
      carbohydrates: [null],
      grams: [null]
    });
  }
}
