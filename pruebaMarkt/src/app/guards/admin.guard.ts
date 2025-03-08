import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  
  constructor(private authService:AuthService, private router : Router){}

  canActivate() {
    if(!this.authService.hasRole("Admin")){
      this.router.navigate(['/unauthorized']);
      return false;
    }else if(!this.authService.isAuthenticated()){
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}