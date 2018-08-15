import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    // the service is returning observable we need to subscribe to observable
   this.authService.login(this.model).subscribe(next => {
     console.log('Logged in Successfully');
   }, error => {
     console.log('Failed ot login');
   });
  }

  // NOTE: Need to include formsmodule in modules

  loggedIn() {
const token = localStorage.getItem('token');
    return !!token; // shorthand for if statement if there is something in the token then return true else return false
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

}
