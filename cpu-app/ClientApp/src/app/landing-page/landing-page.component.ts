import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserDataService } from '../core/services/user-data.service';
import { StateService } from '../core/services/state.service';
import { UserSettings } from '../core/models/user-settings.class';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-landing-page',
    templateUrl: './landing-page.component.html',
    styleUrls: ['./landing-page.component.css'],
    standalone: false
})
export class LandingPageComponent implements OnInit {
  window = window;
  loggedIn: boolean = false;
  isNewUserRegistration: boolean = false;
  contactExistsButNotApproved: boolean = false;

  apiUrl = environment.apiRootUrl;

  constructor(private router: Router,
    private userData: UserDataService,
    private stateService: StateService) {

    this.userData.checkIfLoggedIn().subscribe((isLoggedIn) => {
      console.log('YY: isLoggedIn', isLoggedIn);
      if (isLoggedIn) {
        this.userData.getCurrentUser().subscribe((userSettings: UserSettings) => {
          console.log('YY: userSettings', userSettings);
          // console.log("returned user info:");
          // console.log(userSettings);
          if (userSettings && userSettings.userAuthenticated) {
            console.log('YY: userSettings exists');
            // console.log("setting user data as logged in");
            this.stateService.loggedIn.next(true);
            this.stateService.userSettings.next(userSettings);
            this.isNewUserRegistration = userSettings.isNewUserRegistration;
            this.contactExistsButNotApproved = userSettings.contactExistsButNotApproved;

            this.stateService.getUserName();
          }
          else {
            console.log('YY: userSettings does not exist');
            this.stateService.loggedIn.next(false);
          }
        });
      }
    }, (err) => {
      console.log('YY: checkIfLoggedIn error', err);
      console.log(err);
    });

  }

  ngOnInit() {
    this.stateService.loggedIn.subscribe((l: boolean) => {
      this.loggedIn = l;
    });
  }

  login() {
    console.log('WW: Landing Page - login');
    if (window.location.href.includes("localhost")) {
      this.stateService.login();
    }
    else {
      console.log("YY: login: ", this.apiUrl.concat('login'));
      this.window.location.href = this.apiUrl.concat('login');
    }
  }
  logout() {
    console.log("YY: logout");
    this.stateService.logout();
  }
}
