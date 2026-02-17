import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { EMAIL, PHONE_NUMBER, POSTAL_CODE } from "../constants/regex.constants";
import { iAddress } from "../models/address.interface";
import { iContactInformation } from "../models/contact-information.interface";

export class ContactInformationFormFactory {
  /**
   * Creates a FormGroup for an Address
   */
  static createAddressFormGroup(
    fb: FormBuilder,
    address?: iAddress,
    isRequired: boolean = true,
  ): FormGroup {
    const validators = isRequired ? [Validators.required] : [];

    return fb.group({
      line1: [address?.line1 || "", validators],
      line2: [address?.line2 || ""],
      city: [address?.city || "", validators],
      province: [address?.province || "", validators],
      postalCode: [
        address?.postalCode || "",
        isRequired
          ? [Validators.required, Validators.pattern(POSTAL_CODE)]
          : [Validators.pattern(POSTAL_CODE)],
      ],
      country: [address?.country || "Canada"],
    });
  }

  /**
   * Creates a FormGroup for Primary Contact Information (email, phone, fax)
   */
  static createPrimaryContactFormGroup(
    fb: FormBuilder,
    contactInfo?: iContactInformation,
  ): FormGroup {
    return fb.group({
      emailAddress: [
        contactInfo?.emailAddress || "",
        [Validators.required, Validators.pattern(EMAIL)],
      ],
      phoneNumber: [
        contactInfo?.phoneNumber || "",
        [Validators.required, Validators.pattern(PHONE_NUMBER)],
      ],
      faxNumber: [
        contactInfo?.faxNumber || "",
        [Validators.pattern(PHONE_NUMBER)],
      ],
    });
  }

  /**
   * Creates the complete ContactInformation FormGroup
   */
  static createContactInformationFormGroup(
    fb: FormBuilder,
    contactInfo?: iContactInformation,
  ): FormGroup {
    return fb.group({
      // Primary contact info (email, phone, fax)
      primaryContact: this.createPrimaryContactFormGroup(fb, contactInfo),

      // Main address
      mainAddress: this.createAddressFormGroup(
        fb,
        contactInfo?.mainAddress,
        true,
      ),

      // Mailing address
      mailingAddressSameAsMainAddress: [
        contactInfo?.mailingAddressSameAsMainAddress || false,
      ],
      mailingAddress: this.createAddressFormGroup(
        fb,
        contactInfo?.mailingAddress,
        true,
      ),

      // Organization contacts
      hasBoardContact: [contactInfo?.hasBoardContact || false],

      // Person contacts - storing personId for validation
      executiveContactId: [
        contactInfo?.executiveContact?.personId || null,
        [Validators.required],
      ],
      boardContactId: [contactInfo?.boardContact?.personId || null],
    });
  }

  /**
   * Extracts the form values and converts back to iContactInformation
   */
  static formToContactInformation(
    formGroup: FormGroup,
    originalContactInfo: iContactInformation,
  ): iContactInformation {
    // Use getRawValue() to include disabled controls (e.g., mailing address when checkbox is checked)
    const formValue = formGroup.getRawValue();

    return {
      ...originalContactInfo,
      emailAddress: formValue.primaryContact?.emailAddress,
      phoneNumber: formValue.primaryContact?.phoneNumber,
      faxNumber: formValue.primaryContact?.faxNumber,
      mainAddress: formValue.mainAddress,
      mailingAddressSameAsMainAddress:
        formValue.mailingAddressSameAsMainAddress,
      mailingAddress: formValue.mailingAddress,
      hasBoardContact: formValue.hasBoardContact,
      // Person contacts preserved from original (full object managed by person-picker)
      // The form only validates personId, but we keep the full person object
      executiveContact: originalContactInfo.executiveContact,
      boardContact: originalContactInfo.boardContact,
    };
  }

  /**
   * Patches the form with new contact information
   */
  static patchForm(
    formGroup: FormGroup,
    contactInfo: iContactInformation,
  ): void {
    formGroup.patchValue({
      primaryContact: {
        emailAddress: contactInfo.emailAddress,
        phoneNumber: contactInfo.phoneNumber,
        faxNumber: contactInfo.faxNumber,
      },
      mainAddress: contactInfo.mainAddress,
      mailingAddressSameAsMainAddress:
        contactInfo.mailingAddressSameAsMainAddress,
      mailingAddress: contactInfo.mailingAddress,
      hasBoardContact: contactInfo.hasBoardContact,
      executiveContactId: contactInfo.executiveContact?.personId || null,
      boardContactId: contactInfo.boardContact?.personId || null,
    });
  }
}
