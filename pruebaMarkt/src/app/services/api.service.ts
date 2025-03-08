import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }


  getCategorias():Observable<any>{
    //para saber cual puerto verificar launchSettings.json del .net
    return this.http.get('https://localhost:7074/api/categorias');
  }

  getProductos():Observable<any>{
    return this.http.get('https://localhost:7074/api/productos');
  }
  
  getVentas():Observable<any>{
    return this.http.get('https://localhost:7074/api/ventas');
  }

}
