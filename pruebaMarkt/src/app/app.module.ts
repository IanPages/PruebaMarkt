import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ErrorComponent } from './error/error.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { VentasComponent } from './ventas/ventas.component';
import { ProductosComponent } from './productos/productos.component';
import { CategoriasComponent } from './categorias/categorias.component';
import path from 'path';
import { HomeComponent } from './home/home.component';
import { ApiService } from './services/api.service';
import { AuthService } from './services/auth.service';
import {CookieService} from 'ngx-cookie-service';
import { AdminGuard } from './guards/admin.guard';
import { AuthGuard } from './guards/auth.guard';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { loggedOrAdminGuard } from './guards/logged-or-admin.guard';


//OJO, SI PONEMOS MÄS DE UN GUARD SE HAN DE CUMPLIR LOS DOS PARA QUE DEJE VER LA PÁGINA
const appRoutes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'ventas', component: VentasComponent, canActivate: [AdminGuard] },
  {path: 'productos', component: ProductosComponent , canActivate: [loggedOrAdminGuard]},
  {path: 'categorias', component: CategoriasComponent, canActivate: [loggedOrAdminGuard]},
  {path:'unauthorized',component: UnauthorizedComponent},
  {path: '**', component: ErrorComponent},
];


@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,
    LoginComponent,
    RegisterComponent,
    VentasComponent,
    ProductosComponent,
    CategoriasComponent,
    HomeComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule
  ],
  providers: [
    provideClientHydration(withEventReplay()),
    ApiService,
    AuthService,
    CookieService,
    AdminGuard,
    AuthGuard,
    loggedOrAdminGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
