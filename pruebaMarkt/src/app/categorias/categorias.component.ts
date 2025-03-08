import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';



@Component({
  selector: 'app-categorias',
  standalone: false,
  templateUrl: './categorias.component.html',
  styleUrl: './categorias.component.css'
})
export class CategoriasComponent implements OnInit{

  constructor(private apiService: ApiService){}

  categorias: any[]= [];

  ngOnInit(): void {
    this.getCategorias();
  }

  getCategorias(){
    this.apiService.getCategorias().subscribe(data => {
      this.categorias = data;
      console.log(this.categorias);
    })
  }



}
