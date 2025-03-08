import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, public cookieService: CookieService, private router:Router) { }

  login(Email:string, Password:string){
    //aclarar el rsponseType porque si no Angular por defecto espera JSON
    return this.http.post('https://localhost:7074/api/Auth/login', { Email, Password }, { responseType: 'text' }).subscribe({
      //next refiere a que vendra si sale satisfactoriamente
      next: (response: any) => {
        this.cookieService.set('token', response);
        this.router.navigate(['/']);
      },
      error: error => {
        console.error('Error en el login', error);
      }
    });
  }

  register(Email:string, Password:string){
    this.http.post('https://localhost:7074/api/Auth/register', { Email, Password }).subscribe({
    next: () => {
      alert('Registro exitoso, ahora inicia sesión.');
      this.router.navigate(['/login']);
    },
    error: error => {
      console.error('Error en el registro', error);
      alert('Hubo un error en el registro.');
    }
  });
  }

  logout(){
    this.cookieService.delete('token');
    this.router.navigate(['/']);
  }

  getToken(){
    return this.cookieService.get('token');
  }

  isAuthenticated():boolean{
    return !!this.getToken();
    //!! se encarga de verificar si devuelve algo que refiera a true siendo un valor pedido etc, si no tira false
  }
  getRoles(): string[] {
    const token = this.getToken();
    if (!token) return [];
    try{
      //decode del jwt para ver los roles
      const decoded: any = jwtDecode(token);
      //en el array.is array lo que hacemos es que si solo devuelve un role, pues que lo convierta en array con el valor único
      return decoded["Role"] ? (Array.isArray(decoded["Role"]) ? decoded["Role"] : [decoded["Role"]]) : [];
    }catch(error){
      return [];
    }
  }

  hasRole(role:string):boolean{
    //verificamos que tenga el role
    return this.getRoles().includes(role);
  }
}
