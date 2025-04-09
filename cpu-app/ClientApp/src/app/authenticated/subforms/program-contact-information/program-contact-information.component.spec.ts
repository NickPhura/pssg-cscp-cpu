import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProgramContactInformationComponent } from './program-contact-information.component';

describe('ProgramContactInformationComponent', () => {
  let component: ProgramContactInformationComponent;
  let fixture: ComponentFixture<ProgramContactInformationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgramContactInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgramContactInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
