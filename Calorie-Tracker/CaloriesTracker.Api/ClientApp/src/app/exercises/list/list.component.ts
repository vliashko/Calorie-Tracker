import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
  displayedColumns: string[] = [];

  constructor(public exerciseService: ExercisesService,
              private dialog: MatDialog) {  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'description', 'caloriesSpent', 'actions'];
    this.exerciseService.apiExercisesGet().subscribe((data: Exercise[]) => {
      this.exercises = data;
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
        this.exerciseService.apiExercisesIdDelete(id).subscribe(res => {
          this.exercises = this.exercises.filter(item => item.id !== id);
        });
      }
    });
  }

}
