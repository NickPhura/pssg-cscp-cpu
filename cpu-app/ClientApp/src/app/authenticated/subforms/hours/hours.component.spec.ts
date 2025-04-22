import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HoursComponent } from './hours.component';

describe('HoursComponent', () => {
  let component: HoursComponent;
  let fixture: ComponentFixture<HoursComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HoursComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
