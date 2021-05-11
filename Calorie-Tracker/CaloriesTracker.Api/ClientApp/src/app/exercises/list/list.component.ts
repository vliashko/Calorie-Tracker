import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmationDialog } from 'src/app/confirmation-dialog.component';
import { Exercise } from 'src/app/model/exercise';
import { ExercisesService } from '../exercises.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  exercises: Exercise[] = [];
  loading = true;
  event: any;
  displayedColumns: string[] = [];
  searchName = '';
  dataSource: MatTableDataSource<Exercise> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(public exercisesService: ExercisesService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'description', 'caloriesSpent', 'actions'];
    this.getData(1, 5);
  }
  getData(page: number, pageSize: number) {
    this.exercisesService.apiExercisesPageNumberSizePageSizeParamsGet(pageSize, page, this.searchName).subscribe((res: any) => {
      this.loading = false;
      this.exercises = res.objects;
      this.exercises.length = res.pageViewModel.count;
      this.event = {
        previousPageIndex: 0,
        pageIndex: 0,
        pageSize,
        length: res.pageViewModel.count
      };
      this.dataSource = new MatTableDataSource(res.objects);
      this.paginator._intl.itemsPerPageLabel = 'Exercises Per Page:';
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
    this.exercisesService.apiExercisesPageNumberSizePageSizeParamsGet(pageSize, ++page, this.searchName).subscribe(response => {
      this.loading = false;
      this.exercises.length = currentSize;
      this.exercises.push(...response.objects);
      this.exercises.length = response.pageViewModel.count;
      this.dataSource = new MatTableDataSource(this.exercises);
      this.dataSource.paginator = this.paginator;
    });
  }
  // tslint:disable-next-line:typedef
  deleteExercise(id: string) {
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
        this.exercisesService.apiExercisesIdDelete(id).subscribe(res => {
          this.loading = true;
          this.paginator.pageIndex = 0;
          this.getData(1, this.paginator.pageSize);
        });
      }
    });
  }

}
