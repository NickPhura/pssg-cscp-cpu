import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { Subscription } from "rxjs";
import { nameAssemble } from "../../../core/constants/name-assemble";
import { Person } from "../../../core/models/person.class";
import { iPerson } from "../../../core/models/person.interface";
import { Transmogrifier } from "../../../core/models/transmogrifier.class";
import { StateService } from "../../../core/services/state.service";

@Component({
  selector: "app-person-picker-form",
  templateUrl: "./person-picker-form.component.html",
  styleUrls: ["./person-picker-form.component.scss"],
  standalone: false,
})
export class PersonPickerFormComponent implements OnInit, OnDestroy {
  @Input() title = "Select Person";
  @Input() isDisabled: boolean = false;
  @Input() person: iPerson;
  @Input() idNum: number = 0;
  @Output() personChange = new EventEmitter<iPerson>();
  @Input() showCard = true;
  @Input() control?: FormControl; // Optional FormControl for reactive forms integration
  @Input() errorMessage: string = "Please select a person";
  personId: string = null;
  public nameAssemble = nameAssemble;
  trans: Transmogrifier;
  private stateSubscription: Subscription;

  constructor(private stateService: StateService) {}

  ngOnDestroy() {
    this.stateSubscription.unsubscribe();
  }
  ngOnInit() {
    if (this.person && this.person.personId) {
      this.personId = this.person.personId;
    }

    this.stateSubscription = this.stateService.main.subscribe(
      (m: Transmogrifier) => {
        this.trans = m;

        if (!this.person) {
          this.person = new Person();
          this.personId = this.person.personId;
        }
        this.setPerson(this.personId);
      },
    );
  }
  setPerson(personId: string): void {
    this.person = this.trans.persons.find((p) => p.personId === personId);
    this.personId = personId;

    if (this.control) {
      this.control.setValue(personId);
    }
  }
  onChange() {
    this.setPerson(this.personId);
    this.personChange.emit(this.person);

    if (this.control) {
      this.control.setValue(this.personId);
    }
  }

  get isDirtyOrTouched(): boolean {
    if (!this.control) return false;

    return this.control.dirty || this.control.touched;
  }

  get isRequired(): boolean {
    if (!this.control) return false;

    return (
      this.control?.hasValidator &&
      this.control.hasValidator(Validators.required)
    );
  }

  get hasError(): boolean {
    if (!this.control) return false;

    return this.control.hasError("required") && this.isDirtyOrTouched;
  }
}
