import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '../users.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  formGroup!: FormGroup;

  constructor(public usersService: UsersService,
              public route: ActivatedRoute,
              private formBuilder: FormBuilder) {  }

  ngOnInit(): void {
    this.createForm();
    this.usersService.userById(this.route.snapshot.params.userId).subscribe(res => {
      res.userProfile.dateOfBirth = formatDate(res.userProfile.dateOfBirth, 'MM/dd/yyyy', 'en-US');
      res.userProfile.gender === 0 ? res.userProfile.gender = 'Male' : res.userProfile.gender = 'Female';
      this.formGroup.patchValue(res.userProfile);
    });
  }
  createForm() {
    this.formGroup = this.formBuilder.group({
      weight: [null],
      height: [null],
      gender: [null],
      dateOfBirth: [null],
    });
  }
}
