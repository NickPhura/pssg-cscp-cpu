import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProgramApplicationComponent } from './program-application.component';

describe('ProgramApplicationComponent', () => {
  let component: ProgramApplicationComponent;
  let fixture: ComponentFixture<ProgramApplicationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramApplicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramApplicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
