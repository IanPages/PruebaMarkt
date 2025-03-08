import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-ventas',
  standalone: false,
  templateUrl: './ventas.component.html',
  styleUrl: './ventas.component.css'
})
export class VentasComponent implements OnInit {

  ventas:any[] = [];


  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.getVentas();
  } 

  getVentas(){
    this.apiService.getVentas().subscribe( data =>{
      this.ventas = data;
      console.log(this.ventas)
    });
  }


}
