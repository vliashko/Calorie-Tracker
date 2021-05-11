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
      this.userPr.apiUserprofilesCountDaysGet(7, res.id).subscribe(data => {
          data.forEach((el: any) => {
            el.day = formatDate(el.day, 'MM/dd/yyyy', 'en-US');
            this.chartLabels.push(el.day);
            this.data.push(el.currentCalories);
          });
          this.chartLabels.reverse();
          this.data.reverse();
          this.chartDatasets = [{ data: this.data, label: 'Calories' }];
      });
    });
  }
  showData(count: number): void {
    this.userPr.apiUserprofilesCountDaysGet(count, this.id).subscribe(data => {
      this.chartDatasets = [];
      this.chartLabels = [];
      this.data = [];
      data.forEach((el: any) => {
        el.day = formatDate(el.day, 'MM/dd/yyyy', 'en-US');
        this.chartLabels.push(el.day);
        this.data.push(el.currentCalories);
      });
      this.chartLabels.reverse();
      this.data.reverse();
      this.chartDatasets = [{ data: this.data, label: 'Calories' }];
    });
  }
}
