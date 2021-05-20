import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { ActivitiesService } from 'src/app/activities/activities.service';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { EatingsService } from 'src/app/eatings/eatings.service';
import { UserProfilesService } from 'src/app/users/userProfiles.service';

@Component({
  selector: 'app-calorieschart',
  templateUrl: './calorieschart.component.html',
  styleUrls: ['./calorieschart.component.css']
})
export class CalorieschartComponent implements OnInit {

  data: Array<any> = [];
  currentCalories!: number[];
  id: any;

  public chartType = 'line';

  public chartDatasets: Array<any> = [{ data: [], label: '' }];

  public chartLabels: Array<any> = [];

  public chartColors: Array<any> = [
    {
      backgroundColor: 'rgba(105, 0, 132, .2)',
      borderColor: 'rgba(200, 99, 132, .7)',
      borderWidth: 4,
    }
  ];

  public chartOptions: any = {
    responsive: true
  };
  public chartClicked(e: any): void { }
  public chartHovered(e: any): void { }

  constructor(public eatingsService: EatingsService,
              public activitesService: ActivitiesService,
              public authService: AuthenticationService,
              public userPr: UserProfilesService) { }

  ngOnInit(): void {
    const token = this.authService.getToken();
    const decoded: any = jwtDecode(token);
    this.userPr.apiUserprofilesGet(decoded.userId).subscribe(res => {
      this.id = res.id;
      this.activitesService.apiActivitiesCountDaysGet(7, res.id).subscribe(data1 => {
        this.eatingsService.apiEatingsCountDaysGet(7, res.id).subscribe(data2 => {
          for (let index = 0; index < data1.length; index++) {
            this.chartLabels.push(formatDate(data2[index].day, 'MM/dd/yyyy', 'en-US'));
            this.data.push(data2[index].currentCalories - data1[index].currentCalories);
            this.chartDatasets = [{ data: this.data, label: 'Calories' }];
          }
        });
      });
    });
  }
  showData(count: number): void {
    this.activitesService.apiActivitiesCountDaysGet(count, this.id).subscribe(data1 => {
      this.eatingsService.apiEatingsCountDaysGet(count, this.id).subscribe(data2 => {
        this.chartDatasets = [];
        this.chartLabels = [];
        this.data = [];
        for (let index = 0; index < data1.length; index++) {
          this.chartLabels.push(formatDate(data2[index].day, 'MM/dd/yyyy', 'en-US'));
          this.data.push(data2[index].currentCalories - data1[index].currentCalories);
          this.chartDatasets = [{ data: this.data, label: 'Calories' }];
        }
      });
    });
  }
}
