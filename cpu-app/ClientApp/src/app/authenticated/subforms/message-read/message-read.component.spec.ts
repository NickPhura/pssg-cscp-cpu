import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { MessageReadComponent } from './message-read.component';

describe('MessageReadComponent', () => {
  let component: MessageReadComponent;
  let fixture: ComponentFixture<MessageReadComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [MessageReadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessageReadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
