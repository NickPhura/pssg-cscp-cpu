import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OrganizationProfileBoxComponent } from './organization-profile-box.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('OrganizationProfileBoxComponent', () => {
  let component: OrganizationProfileBoxComponent;
  let fixture: ComponentFixture<OrganizationProfileBoxComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [OrganizationProfileBoxComponent],
      imports: [RouterTestingModule]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationProfileBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
