import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProgramContactComponent } from './program-contact.component';

describe('ProgramContactComponent', () => {
  let component: ProgramContactComponent;
  let fixture: ComponentFixture<ProgramContactComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramContactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
