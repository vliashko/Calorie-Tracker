import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
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
  displayedColumns: string[] = [];
  dataSource: MatTableDataSource<User> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  // tslint:disable-next-line:typedef
  applyFilter(filterValue: any) {
    filterValue = filterValue.target.value;
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    console.log(filterValue);
    this.dataSource.filter = filterValue;
  }

  constructor(public usersService: UsersService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['userName', 'email', 'actions'];
    this.usersService.apiUsersGet().subscribe(res => {
      this.dataSource = new MatTableDataSource(res);
      this.paginator._intl.itemsPerPageLabel = 'Users Per Page';
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.dataSource.data);
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
        this.usersService.apiUsersIdDelete(id).subscribe(res => {
          this.users = this.users.filter(item => item.id !== id);
        });
      }
    });
  }
}
