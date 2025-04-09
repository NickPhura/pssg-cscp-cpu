import { Component, Inject } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';

@Component({
    selector: 'program-egilibility.dialog',
    templateUrl: 'program-egilibility.dialog.html',
})
export class ProgramEgilibilityDialog {
    constructor(public dialogRef: MatDialogRef<ProgramEgilibilityDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any) {
    }

    onOkayClick() {
        this.dialogRef.close();
    }

    close() {
        this.dialogRef.close();
    }
}
