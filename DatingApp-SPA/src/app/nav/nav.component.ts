import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '../../../node_modules/@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService,  private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    // the service is returning observable we need to subscribe to observable
   this.authService.login(this.model).subscribe(next => {
    this.alertify.success('Logged in successfully');
   }, error => {
   this.alertify.error(error);
   }, () => {
     this.router.navigate(['/members']);
   });
  }

  // NOTE: Need to include formsmodule in modules

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
