import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SignContractComponent } from './sign-contract.component';


describe('SignContractComponent', () => {
  let component: SignContractComponent;
  let fixture: ComponentFixture<SignContractComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SignContractComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignContractComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
