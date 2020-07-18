import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { routes } from './routes';
import { AuthenticationService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { NotifyService } from './_services/notify.service';


@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      LoginComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(routes),
      SimpleNotificationsModule.forRoot(
         {
            position: ['top', 'center'],
            maxStack: 1
         }
      ),
      ReactiveFormsModule,
      BrowserAnimationsModule,
      BsDatepickerModule.forRoot(),
   ],
   providers: [
      AuthenticationService,
      ErrorInterceptorProvider,
      NotifyService
   ],
   bootstrap: [AppComponent]
})
export class AppModule { }
