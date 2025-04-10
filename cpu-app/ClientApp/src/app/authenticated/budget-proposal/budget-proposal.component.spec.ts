import { provideHttpClientTesting } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, waitForAsync } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { BehaviorSubject } from "rxjs";
import { StateService } from "../../core/services/state.service";
import { BudgetProposalComponent } from "./budget-proposal.component";
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";

describe("BudgetProposalComponent", () => {
  let component: BudgetProposalComponent;
  let fixture: ComponentFixture<BudgetProposalComponent>;

  beforeEach(waitForAsync(() => {
    const stateServiceStub = {
      main: new BehaviorSubject({
        persons: [
          // You can add test persons here, for example:
          { personId: 1, firstName: "John", middleName: "A.", lastName: "Doe" },
          { personId: 2, firstName: "Jane", middleName: "", lastName: "Smith" },
        ],
      }),
    };

    TestBed.configureTestingModule({
    declarations: [BudgetProposalComponent],
    imports: [RouterTestingModule],
    providers: [
        { provide: StateService, useValue: stateServiceStub },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting(),
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetProposalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
