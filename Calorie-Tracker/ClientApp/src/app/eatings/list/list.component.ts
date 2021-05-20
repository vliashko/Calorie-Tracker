import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import jwtDecode from 'jwt-decode';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { Eating } from 'src/app/model/eating';
import { UserProfilesService } from 'src/app/users/userProfiles.service';
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
        .filter((act: any) => formatDate(act.moment, 'yyyy-MM-dd', 'en-US') === formatDate(new Date(), 'yyyy-MM-dd', 'en-US'));
        this.eatings
          .forEach((act: any) => act.moment = formatDate(act.moment, 'HH:mm', 'en-US'));
      });
    });
  }
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
        this.eatingsService.apiUsersUserIdEatingsEatingIdDelete(id, this.id).subscribe(res => {
          this.eatings = this.eatings.filter(item => item.id !== id);
        });
      }
    });
  }
}
