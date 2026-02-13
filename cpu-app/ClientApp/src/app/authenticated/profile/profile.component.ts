import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import * as _ from "lodash";
import { Subscription } from "rxjs";
import { ContactInformationFormFactory } from "../../core/forms/contact-information-form.factory";
import { iContactInformation } from "../../core/models/contact-information.interface";
import { convertContactInformationToDynamics } from "../../core/models/converters/contact-information-to-dynamics";
import { Transmogrifier } from "../../core/models/transmogrifier.class";
import { Roles } from "../../core/models/user-settings.interface";
import { NotificationQueueService } from "../../core/services/notification-queue.service";
import { ProfileService } from "../../core/services/profile.service";
import { StateService } from "../../core/services/state.service";
import { PersonPickerComponent } from "../subforms/person-picker/person-picker.component";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.css"],
  standalone: false,
})
export class ProfileComponent implements OnInit, OnDestroy {
  @ViewChild(PersonPickerComponent, { static: false })
  contractorContactComp: PersonPickerComponent;
  @ViewChild(PersonPickerComponent, { static: false })
  boardContactComp: PersonPickerComponent;

  trans: Transmogrifier;
  contactForm: FormGroup;
  saving: boolean = false;
  private stateSubscription: Subscription;
  originalContactInfo: iContactInformation;
  Roles = Roles;
  userRole = Roles.ProgramStaff;

  constructor(
    private router: Router,
    private stateService: StateService,
    private profileService: ProfileService,
    private notificationQueueService: NotificationQueueService,
    private fb: FormBuilder,
  ) {}

  ngOnInit() {
    let userSettings = this.stateService.userSettings.getValue();
    this.userRole = userSettings.userRole;

    this.stateSubscription = this.stateService.main.subscribe(
      (m: Transmogrifier) => {
        this.trans = m;
        this.originalContactInfo = _.cloneDeep(this.trans.contactInformation);

        // Initialize form with contact information
        this.contactForm =
          ContactInformationFormFactory.createContactInformationFormGroup(
            this.fb,
            this.trans.contactInformation,
          );

        // Set up mailing address sync
        this.setupMailingAddressSync();

        // Handle disabled state for ProgramStaff
        if (this.userRole === Roles.ProgramStaff) {
          this.contactForm.disable();
        }
      },
    );
  }

  private setupMailingAddressSync(): void {
    const mailingAddressSameControl = this.contactForm.get(
      "mailingAddressSameAsMainAddress",
    );
    const mainAddressControl = this.contactForm.get("mainAddress");
    const mailingAddressControl = this.contactForm.get("mailingAddress");
    const hasBoardContactControl = this.contactForm.get("hasBoardContact");

    // Sync mailing address when checkbox changes
    mailingAddressSameControl?.valueChanges.subscribe((isSame) => {
      if (isSame) {
        mailingAddressControl?.patchValue(mainAddressControl?.value);
        mailingAddressControl?.disable();
      } else {
        mailingAddressControl?.enable();
        if (this.userRole === Roles.ProgramStaff) {
          mailingAddressControl?.disable();
        }
      }
    });

    // Sync mailing address when main address changes (if checkbox is checked)
    mainAddressControl?.valueChanges.subscribe((mainAddress) => {
      if (mailingAddressSameControl?.value) {
        mailingAddressControl?.patchValue(mainAddress, { emitEvent: false });
      }
    });

    // Sync hasBoardContact back to trans
    hasBoardContactControl?.valueChanges.subscribe((hasBoardContact) => {
      this.trans.contactInformation.hasBoardContact = hasBoardContact;
    });

    // Initialize disabled state if needed
    if (mailingAddressSameControl?.value) {
      mailingAddressControl?.disable();
    }
  }
  ngOnDestroy() {
    // Restore original contact info if form was modified but not saved
    if (this.contactForm && this.contactForm.dirty) {
      const updatedContactInfo =
        ContactInformationFormFactory.formToContactInformation(
          this.contactForm,
          this.trans.contactInformation,
        );
      if (!_.isEqual(this.originalContactInfo, updatedContactInfo)) {
        this.trans.contactInformation = this.originalContactInfo;
      }
    }
    this.stateSubscription.unsubscribe();
  }

  cancel() {
    // Reset form to original values
    ContactInformationFormFactory.patchForm(
      this.contactForm,
      this.originalContactInfo,
    );
    this.contactForm.markAsPristine();
    this.contactForm.markAsUntouched();

    // Reset person pickers - FormControl values trigger setPerson internally
    if (
      this.originalContactInfo.executiveContact &&
      this.originalContactInfo.executiveContact.personId
    ) {
      this.contractorContactComp.setPerson(
        this.originalContactInfo.executiveContact.personId,
      );
    }
    if (
      this.originalContactInfo.boardContact &&
      this.originalContactInfo.boardContact.personId
    ) {
      this.boardContactComp.setPerson(
        this.originalContactInfo.boardContact.personId,
      );
    }
  }
  save(shouldExit: boolean = false): void {
    try {
      // Mark all fields as touched to show validation errors
      this.contactForm.markAllAsTouched();

      // Check form validity (validates all fields including person pickers)
      if (!this.contactForm.valid) {
        this.notificationQueueService.addNotification(
          "All required fields must be completed.",
          "warning",
        );
        return;
      }

      // Merge form values back to trans.contactInformation
      const updatedContactInfo =
        ContactInformationFormFactory.formToContactInformation(
          this.contactForm,
          this.trans.contactInformation,
        );

      this.saving = true;

      // Update trans with form values
      this.trans.contactInformation = updatedContactInfo;

      this.profileService
        .updateOrg(convertContactInformationToDynamics(this.trans))
        .subscribe(
          (res: any) => {
            this.saving = false;
            this.notificationQueueService.addNotification(
              "The contact information for your organization has been updated.",
              "success",
            );
            this.stateService.refresh();
            this.contactForm.markAsPristine();
            this.originalContactInfo = _.cloneDeep(
              this.trans.contactInformation,
            );
            if (shouldExit)
              this.router.navigate([this.stateService.homeRoute.getValue()]);
          },
          (err) => {
            console.log(err);
            this.saving = false;
            this.notificationQueueService.addNotification(
              "The contact information could not be saved. Please try again.",
              "danger",
            );
          },
        );
    } catch (err) {
      console.log(err);
      this.notificationQueueService.addNotification(
        "The contact information for your organization could not be saved. If this problem is persisting please contact your ministry representative.",
        "danger",
      );
      this.saving = false;
    }
  }
  exit() {
    this.stateService.refresh();
    this.router.navigate(["/authenticated/dashboard"]);
  }
}
