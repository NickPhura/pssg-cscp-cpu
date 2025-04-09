import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AdministrativeInformationComponent } from './administrative-information.component';

describe('AdministrativeInformationComponent', () => {
  let component: AdministrativeInformationComponent;
  let fixture: ComponentFixture<AdministrativeInformationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministrativeInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministrativeInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
