import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'pruebaMarkt';

  constructor(private authService:AuthService){
    
  }

  ngOnInit(): void {
  }
  
  loggedin(){
    return this.authService.isAuthenticated();
  }

  logout(){
    this.authService.logout();
  }
}
