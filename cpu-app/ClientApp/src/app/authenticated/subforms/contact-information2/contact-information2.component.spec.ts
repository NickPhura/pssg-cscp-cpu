import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ContactInformation2Component } from './contact-information2.component';

describe('ContactInformation2Component', () => {
  let component: ContactInformation2Component;
  let fixture: ComponentFixture<ContactInformation2Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactInformation2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactInformation2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
