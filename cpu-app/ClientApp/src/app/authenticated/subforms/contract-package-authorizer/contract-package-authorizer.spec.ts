import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ContractPackageAuthorizerComponent } from './contract-package-authorizer';



describe('ContractPackageAuthorizerComponent', () => {
  let component: ContractPackageAuthorizerComponent;
  let fixture: ComponentFixture<ContractPackageAuthorizerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractPackageAuthorizerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractPackageAuthorizerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
