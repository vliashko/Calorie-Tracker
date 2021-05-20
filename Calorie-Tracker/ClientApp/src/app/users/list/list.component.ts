import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { User } from 'src/app/model/user';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  users: User[] = [];
  userName: string = '';
  email: string = '';
  loading = true;
  event: any;
  displayedColumns: string[] = [];
  dataSource: MatTableDataSource<User> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(public usersService: UsersService,
              public authService: AuthenticationService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['userName', 'email', 'password', 'actions'];
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number) {
    this.usersService.apiUsersPageNumberSizePageSizeParamsGet(pageSize, page, this.userName, this.email).subscribe((res: any) => {
      this.loading = false;
      this.users = res.objects;
      this.users.length = res.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: res.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(res.objects);
      this.paginator._intl.itemsPerPageLabel = 'Users Per Page:';
      this.dataSource.paginator = this.paginator;
    });
  }
  filterData() {
    this.getData(1, 5);
  }
  pageChanged(event: any) {
    this.event = event;
    this.loading = true;
    this.getNextData(event.pageSize * event.pageIndex, event.pageSize, event.pageIndex);
  }
  getNextData(currentSize: number, pageSize: number, page: number) {
    this.usersService.apiUsersPageNumberSizePageSizeParamsGet(pageSize, ++page, this.userName, this.email).subscribe(response => {
      this.loading = false;
      this.users.length = currentSize;
      this.users.push(...response.objects);
      this.users.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.users);
      this.dataSource.paginator = this.paginator;
    });
  }
  deleteUser(id: string) {
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
        this.usersService.apiUsersIdDelete(id).subscribe(res => {
          this.loading = true;
          this.paginator.pageIndex = 0;
          this.getData(1, this.paginator.pageSize);
        });
      }
    });
  }
  resetPassword(id: string) {
    const dialogRef = this.dialog.open(ConfirmationDialog, {
      data: {
        message: 'Are you sure want to reset password?',
        buttonText: {
          ok: 'Yes',
          cancel: 'No'
        }
      }
    });
    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.authService.apiAuthenticationGeneratenewpasswordIdGet(id).subscribe(res => {
          const result = this.dialog.open(ConfirmationDialog, {
            data: {
              message: 'New Password: ' + res,
              buttonText: {
                cancel: 'I send it to user'
              }
            }
          });
        });
      }
    });
  }
}
