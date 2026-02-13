import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { NgxMaskDirective } from "ngx-mask";
import { FooterComponent } from "./footer/footer.component";
import { HeaderComponent } from "./header/header.component";
import { IconStepperComponent } from "./icon-stepper/icon-stepper.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { NotificationBannerComponent } from "./notification-banner/notification-banner.component";
import { FormFieldComponent } from "./form-field/form-field.component";

@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    IconStepperComponent,
    NotFoundComponent,
    NotificationBannerComponent,
    FormFieldComponent,
  ],
  imports: [CommonModule, ReactiveFormsModule, NgxMaskDirective],
  exports: [
    FooterComponent,
    HeaderComponent,
    IconStepperComponent,
    NotFoundComponent,
    NotificationBannerComponent,
    FormFieldComponent,
  ],
})
export class SharedModule {}
