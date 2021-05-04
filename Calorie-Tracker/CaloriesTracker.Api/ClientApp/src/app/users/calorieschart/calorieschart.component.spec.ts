import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalorieschartComponent } from './calorieschart.component';

describe('CalorieschartComponent', () => {
  let component: CalorieschartComponent;
  let fixture: ComponentFixture<CalorieschartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CalorieschartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CalorieschartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
