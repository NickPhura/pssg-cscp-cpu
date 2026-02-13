import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { EMAIL, PHONE_NUMBER } from "../../../core/constants/regex.constants";
import { FormHelper } from "../../../core/form-helper";
import { iContactInformation } from "../../../core/models/contact-information.interface";

@Component({
  selector: "app-primary-contact-form",
  templateUrl: "./primary-contact-form.component.html",
  styleUrls: ["./primary-contact-form.component.css"],
  standalone: false,
})
export class PrimaryContactFormComponent implements OnInit {
  @Input() contactInformation: iContactInformation;
  @Input() isDisabled: boolean = false;
  @Input() formGroup?: FormGroup;
  @Output() contactInformationChange = new EventEmitter<iContactInformation>();

  public formHelper = new FormHelper();
  emailRegex: RegExp = EMAIL;
  phoneRegex: RegExp = PHONE_NUMBER;

  ngOnInit() {
    this.updateDisabledState();
  }

  private updateDisabledState(): void {
    if (!this.formGroup) return;

    if (this.isDisabled) {
      this.formGroup.disable();
    } else {
      this.formGroup.enable();
    }
  }

  get emailControl() {
    return this.formGroup?.get("emailAddress") || null;
  }

  get phoneControl() {
    return this.formGroup?.get("phoneNumber") || null;
  }

  get faxControl() {
    return this.formGroup?.get("faxNumber") || null;
  }
}
