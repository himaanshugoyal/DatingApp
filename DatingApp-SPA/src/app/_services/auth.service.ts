// When a service is created we need to create this in our app module in th providers
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';

// This allows us to inject things in our service
// NOTE: Components are automatically injectable by default
@Injectable({
  // Provided In: This tells us which module is providing this service, in this case,  root module is providing the service.
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private http: HttpClient) {}

  login(model: any) {
    // We might need to specify the type of content, neither authorization header as of now.
    // We need to add code to do some thing when it comes back from the server
    // Inorder to do something we have an observable, rxJS operators
    // .pipe allows us to chain rxJs operators
    // take the response and do something
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
