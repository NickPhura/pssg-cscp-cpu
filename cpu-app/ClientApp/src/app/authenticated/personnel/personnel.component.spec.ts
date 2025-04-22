import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PersonnelComponent } from './personnel.component';

describe('PersonnelComponent', () => {
  let component: PersonnelComponent;
  let fixture: ComponentFixture<PersonnelComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonnelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonnelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
