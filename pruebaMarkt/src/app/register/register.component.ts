import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {


  constructor(private authService:AuthService){}

  register(form:NgForm){
    const email=form.value.email;
    const password=form.value.password;

    this.authService.register(email,password);
  }
}
