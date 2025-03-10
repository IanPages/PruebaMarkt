import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, GuardResult, MaybeAsync, RouterStateSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class loggedOrAdminGuard implements CanActivate {

  constructor(private router:Router, private authService:AuthService){}

  canActivate() {
    if (this.authService.isAuthenticated() || this.authService.hasRole('Admin')){
      return true;
    }else{
      this.router.navigate(['/unauthorized']);
      return false;
    }
  }
}