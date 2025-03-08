import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-productos',
  standalone: false,
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent implements OnInit{

  productos:any[]=[];

  constructor(private apiService: ApiService) {

  }

  ngOnInit(): void { 
    this.getProductos();
  }

  getProductos(){
    this.apiService.getProductos().subscribe(data => {
      this.productos = data;
      console.log(this.productos);
    })
  }

}
