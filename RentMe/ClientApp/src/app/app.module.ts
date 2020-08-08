import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { DatePipe } from '@angular/common';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FileUploadModule } from 'ng2-file-upload';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { routes } from './routes';
import { AuthenticationService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { NotifyService } from './_services/notify.service';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { AdminService } from './_services/admin.service';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './_guards/auth.guard';
import { UsersResolver } from './_resolvers/users.resolver';
import { FooterComponent } from './footer/footer.component';
import { SubcategoryCardComponent } from './home/subcategory-card/subcategory-card.component';
import { AnnouncementCardComponent } from './announcements/announcement-card/announcement-card.component';
import { AnnouncementListComponent } from './announcements/announcement-list/announcement-list.component';
import { AnnouncementNewComponent } from './announcements/announcement-new/announcement-new.component';
import { AnnouncementService } from './_services/announcement.service';
import { PhotoEditorComponent } from './announcements/photo-editor/photo-editor.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      LoginComponent,
      RegisterComponent,
      UserManagementComponent,
      RolesModalComponent,
      HasRoleDirective,
      AdminPanelComponent,
      HomeComponent,
      FooterComponent,
      SubcategoryCardComponent,
      AnnouncementCardComponent,
      AnnouncementListComponent,
      AnnouncementNewComponent,
      PhotoEditorComponent
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
      TabsModule.forRoot(),
      ModalModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            allowedDomains: ['localhost:5000'],
            disallowedRoutes: ['localhost:5000/api/auth']
         },
      }),
      NgbPaginationModule,
      NgbAlertModule,
      CarouselModule.forRoot(),
      FileUploadModule
   ],
   providers: [
      AuthenticationService,
      ErrorInterceptorProvider,
      NotifyService,
      AdminService,
      DatePipe,
      AuthGuard,
      UsersResolver,
      AnnouncementService
   ],
   entryComponents: [
      RolesModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
