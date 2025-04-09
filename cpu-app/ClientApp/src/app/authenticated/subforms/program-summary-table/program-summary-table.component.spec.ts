import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProgramSummaryTableComponent } from './program-summary-table.component';

describe('ProgramSummaryTableComponent', () => {
  let component: ProgramSummaryTableComponent;
  let fixture: ComponentFixture<ProgramSummaryTableComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramSummaryTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramSummaryTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
