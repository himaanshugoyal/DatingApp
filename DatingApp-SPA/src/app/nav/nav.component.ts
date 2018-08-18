import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService,  private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    // the service is returning observable we need to subscribe to observable
   this.authService.login(this.model).subscribe(next => {
    this.alertify.success('Logged in successfully');
   }, error => {
   this.alertify.error(error);
   });
  }

  // NOTE: Need to include formsmodule in modules

  loggedIn() {
const token = localStorage.getItem('token');
    return !!token; // shorthand for if statement if there is something in the token then return true else return false
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

}
