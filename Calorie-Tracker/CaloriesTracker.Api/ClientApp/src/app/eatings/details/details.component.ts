import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { Eating } from 'src/app/model/eating';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
import { EatingsService } from '../eatings.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  displayedColumns: string[] = [];
  eating: Eating = {
    id: '',
    name: '',
    moment: new Date(),
    userProfileId: '',
    totalCalories: 0,
    ingredientsWithGrams: []
  };
  id!: string;
  editForm!: FormGroup;

  constructor(public eatingsService: EatingsService,
              public authService: AuthenticationService,
              public userPr: UserProfilesService,
              private route: ActivatedRoute) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'calories', 'proteins', 'fats', 'carbohydrates', 'grams'];
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.eatingsService.getEating(this.route.snapshot.params.eatingId, res.id).subscribe(eating => {
        this.eating = eating;
      });
    });
  }
}
