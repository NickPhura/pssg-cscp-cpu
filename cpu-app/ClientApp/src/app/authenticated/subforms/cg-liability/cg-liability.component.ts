import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { AppendixADialog } from '../../dialogs/appendix-a/appendix-a.dialog';

@Component({
  selector: 'app-cg-liability',
  templateUrl: './cg-liability.component.html',
  styleUrls: ['./cg-liability.component.css']
})
export class CgLiabilityComponent implements OnInit {
  @Input() cgLiability: string;
  @Input() isDisabled: boolean = false;
  @Output() cgLiabilityChange = new EventEmitter<string>();
  constructor(
    public dialog: MatDialog,
  ) { }

  ngOnInit() {
  }

  onChanges() {
    this.cgLiabilityChange.emit(this.cgLiability);
  }

  showAppendixADialog() {
    this.dialog.open(AppendixADialog, {
      autoFocus: false,
      data: {}
    });
  }
}
