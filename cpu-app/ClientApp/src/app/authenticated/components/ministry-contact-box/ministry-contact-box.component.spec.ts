import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MinistryContactBoxComponent } from './ministry-contact-box.component';

describe('MinistryContactBoxComponent', () => {
  let component: MinistryContactBoxComponent;
  let fixture: ComponentFixture<MinistryContactBoxComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MinistryContactBoxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MinistryContactBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
