import { Component, Input } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";

@Component({
  selector: "app-form-field",
  templateUrl: "./form-field.component.html",
  styleUrls: ["./form-field.component.css"],
  standalone: false,
})
export class FormFieldComponent {
  @Input() control?: FormControl;
  @Input() label: string;
  @Input() id: string;
  @Input() type: string = "text";
  @Input() placeholder: string = "";
  @Input() mask: string = "";
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() errorMessage: string = "Try Again";
  @Input() trimOnBlur: boolean = true;

  constructor() {}

  get isDirtyOrTouched(): boolean {
    if (!this.control) return false;
    return this.control.dirty || this.control.touched;
  }

  get isValid(): boolean {
    if (!this.control) return false;
    return this.control.valid && this.isDirtyOrTouched && !!this.control.value;
  }

  get hasError(): boolean {
    if (!this.control) return false;
    return this.control.hasError("required") && this.isDirtyOrTouched;
  }

  get isRequired(): boolean {
    return (
      this.required ||
      (this.control?.hasValidator &&
        this.control.hasValidator(Validators.required))
    );
  }

  get isDisabled(): boolean {
    return this.disabled || this.control?.disabled;
  }

  onBlur(): void {
    if (
      this.trimOnBlur &&
      this.control?.value &&
      typeof this.control.value === "string"
    ) {
      const trimmedValue = this.control.value.trim();
      if (trimmedValue !== this.control.value) {
        this.control.setValue(trimmedValue);
      }
    }
  }
}
