// ng g guard auth --spec=false | To generate this guard ile
import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
// CanActivate will tell that whether we are going to allow or Can ACtivate
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router,
  private alertify: AlertifyService) {}
  // Syntax with | (pipes) this is union type and we can return any 1 of these values
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

  this.alertify.error('You shall not pass!!!');
  this.router.navigate(['/home']);
  return false;
}
}
