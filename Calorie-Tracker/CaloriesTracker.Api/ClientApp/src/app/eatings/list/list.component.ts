import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { Eating } from 'src/app/model/eating';
import { UserProfilesService } from 'src/app/userProfiles.service';
import { EatingsService } from '../eatings.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  eatings: Eating[] = [];
  displayedColumns: string[] = [];
  id!: string;

  constructor(public eatingsService: EatingsService,
              public authService: AuthenticationService,
              private dialog: MatDialog,
              public userPr: UserProfilesService) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'moment', 'totalCalories', 'actions'];
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.id = decoded.userId;
    this.userPr.apiUserprofilesGet(this.id).subscribe(res => {
      this.id = res.id;
      this.eatingsService.apiUsersUserIdEatingsGet(res.id).subscribe(eatings => {
        this.eatings = eatings
        .filter((act: any) => act.moment.split('T')[0] === formatDate(new Date(), 'yyyy-MM-dd', 'en-US'));
        this.eatings
          .forEach((act: any) => act.moment = act.moment.split('T')[1].split('+')[0].slice(0, -3));
      });
    });
  }
  // tslint:disable-next-line:typedef
  deleteEating(id: string) {
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
        this.eatingsService.apiUsersUserIdEatingsEatingIdDelete(this.id, id).subscribe(res => {
          this.eatings = this.eatings.filter(item => item.id !== id);
        });
      }
    });
  }
}
