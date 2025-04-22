import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ContractTombstoneComponent } from './contract-tombstone.component';

describe('ContractTombstoneComponent', () => {
  let component: ContractTombstoneComponent;
  let fixture: ComponentFixture<ContractTombstoneComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractTombstoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractTombstoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
