import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserDataService } from '../core/services/user-data.service';
import { StateService } from '../core/services/state.service';
import { NotificationQueueService } from '../core/services/notification-queue.service';
import { UserSettings } from '../core/models/user-settings.class';

@Component({
    selector: 'app-landing-page',
    templateUrl: './login.component.html',
    standalone: false
})
export class LoginPageComponent implements OnInit {
  loading: boolean = true;
  willLogOut: boolean = false;
  constructor(private router: Router,
    private userData: UserDataService,
    private notificationQueueService: NotificationQueueService,
    private stateService: StateService) {

    console.log("XX: login component");

    this.userData.getCurrentUser().subscribe((userInfo: UserSettings) => {
      console.log("XX: user info", JSON.stringify(userInfo));
      if (userInfo && userInfo.userId && userInfo.accountId) {
        this.stateService.loggedIn.next(true);
        this.stateService.userSettings.next(userInfo);
        // console.log(userInfo);
        if (userInfo.isNewUserRegistration) {
          console.log("XX: isNewUserRegistration");
          this.router.navigate(['/authenticated/new_user']);
        }
        else if (userInfo.isNewUserAndNewOrganizationRegistration) {
          console.log("XX: isNewUserAndNewOrganizationRegistration");
          this.router.navigate(['/authenticated/new_user_new_organization']);
        }
        else if (userInfo.contactExistsButNotApproved) {
          console.log("XX: contactExistsButNotApproved");
          this.notificationQueueService.addNotification(`User is not approved for portal access. Please contact an administrator.`, 'danger');
          this.willLogOut = true;
          setTimeout(() => {
            this.stateService.logout();
          }, 6000);
        }
        else if (userInfo.noRolesAssigned) {
          console.log("XX: noRolesAssigned");
          this.notificationQueueService.addNotification(`User has no portal roles. Please contact an administrator.`, 'danger');
          this.willLogOut = true;
          setTimeout(() => {
            this.stateService.logout();
          }, 6000);
        }
        else {
          console.log("XX: stateService.login");
          this.stateService.login();
        }
      }
      else {
        console.log("XX: no user info");
        this.notificationQueueService.addNotification(`No associated CRM account. Please contact an administrator.`, 'warning');
        this.willLogOut = true;
        setTimeout(() => {
          this.stateService.logout();
        }, 6000);
      }
    }, (err) => {
      console.log("XX: error getting user info");
      this.notificationQueueService.addNotification(`Error retrieving user information.`, 'danger');
      console.log(err);
    });
  }

  ngOnInit() {
  }
}
