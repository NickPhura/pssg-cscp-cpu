import { Component, Inject } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';

@Component({
    selector: 'cap-guidelines.dialog',
    templateUrl: 'cap-guidelines.dialog.html',
})
export class CAPGuidelinesDialog {
    constructor(public dialogRef: MatDialogRef<CAPGuidelinesDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
    }

    onOkayClick() {
        this.dialogRef.close();
    }

    close() {
        this.dialogRef.close();
    }
}
