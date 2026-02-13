import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
} from "@angular/core";
import { FormGroup } from "@angular/forms";
import { COUNTRIES_ADDRESS_2 } from "../../../core/constants/country-list";
import { POSTAL_CODE } from "../../../core/constants/regex.constants";
import { FormHelper } from "../../../core/form-helper";
import { iAddress } from "../../../core/models/address.interface";

@Component({
  selector: "app-address-form",
  templateUrl: "./address-form.component.html",
  styleUrls: ["./address-form.component.css"],
  standalone: false,
})
export class AddressFormComponent implements OnInit {
  @Input() address: iAddress;
  @Input() isDisabled: boolean = false;
  @Input() addressRequired: boolean = false;
  @Input() formGroup: FormGroup;
  @Output() addressChange = new EventEmitter<iAddress>();

  public formHelper = new FormHelper();

  countries: any;
  country: any;
  postalRegex: RegExp;

  constructor() {
    this.postalRegex = POSTAL_CODE;
    this.countries = COUNTRIES_ADDRESS_2;
  }

  ngOnInit() {
    this.updateCountry();
    this.updateDisabledState();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["isDisabled"]) {
      this.updateDisabledState();
    }
    if (changes["formGroup"] && this.formGroup) {
      this.updateCountry();
    }
  }

  private updateCountry(): void {
    // consider input address first, then form control, then default to Canada
    const countryValue =
      (this.address && this.address.country) ||
      this.formGroup?.get("country")?.value;

    this.country =
      countryValue && COUNTRIES_ADDRESS_2[countryValue]
        ? COUNTRIES_ADDRESS_2[countryValue]
        : COUNTRIES_ADDRESS_2["Canada"];
  }

  onChange() {
    this.addressChange.emit(this.address);
  }

  private updateDisabledState(): void {
    if (!this.formGroup) return;

    if (this.isDisabled) {
      this.formGroup.disable();
    } else {
      this.formGroup.enable();
    }
  }

  get line1Control() {
    return this.formGroup?.get("line1") || null;
  }

  get line2Control() {
    return this.formGroup?.get("line2") || null;
  }

  get cityControl() {
    return this.formGroup?.get("city") || null;
  }

  get provinceControl() {
    return this.formGroup?.get("province") || null;
  }

  get postalCodeControl() {
    return this.formGroup?.get("postalCode") || null;
  }

  get countryControl() {
    return this.formGroup?.get("country") || null;
  }
}
