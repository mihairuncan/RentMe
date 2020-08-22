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
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxGalleryModule } from 'ngx-gallery';
import { TimeAgoPipe } from 'time-ago-pipe';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
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
import { AnnouncementManagementComponent } from './admin/announcement-management/announcement-management.component';
import { UnapprovedAnnouncementsResolver } from './_resolvers/unapproved-announcements.resolver';
import { AngularMaterialModule } from './_shared/angular-material.module';
import { AnnouncementsResolver } from './_resolvers/announcements.resolver';
import { AnnouncementDetailsComponent } from './announcements/announcement-details/announcement-details.component';
import { AnnouncementResolver } from './_resolvers/announcement-details.resolver';
import { MyAnnouncementListComponent } from './announcements/my-announcement-list/my-announcement-list.component';
import { MyAnnouncementsResolver } from './_resolvers/my-announcements.resolver';
import { MessageService } from './_services/message.service';
import { UserMessagesComponent } from './messages/user-messages/user-messages.component';
import { MessagesListComponent } from './messages/messages-list/messages-list.component';
import { ForgotPasswordComponent } from './authentication/forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './authentication/change-password/change-password.component';

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
      PhotoEditorComponent,
      AnnouncementManagementComponent,
      AnnouncementDetailsComponent,
      TimeAgoPipe,
      MyAnnouncementListComponent,
      UserMessagesComponent,
      MessagesListComponent,
      ForgotPasswordComponent,
      ChangePasswordComponent
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
      SimpleNotificationsModule.forRoot(
         {
            position: ['top', 'center'],
            maxStack: 1
         },
      ),
      ReactiveFormsModule,
      BrowserAnimationsModule,
      BsDatepickerModule.forRoot(),
      TabsModule.forRoot(),
      ModalModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            allowedDomains: ['localhost:5001'],
            disallowedRoutes: ['localhost:5001/api/auth']
         },
      }),
      NgbPaginationModule,
      NgbAlertModule,
      CarouselModule.forRoot(),
      FileUploadModule,
      SweetAlert2Module.forRoot(),
      BsDropdownModule.forRoot(),
      AngularMaterialModule,
      NgxGalleryModule
   ],
   exports: [
      AngularMaterialModule
   ],
   providers: [
      AuthenticationService,
      ErrorInterceptorProvider,
      NotifyService,
      AdminService,
      DatePipe,
      AuthGuard,
      UsersResolver,
      UnapprovedAnnouncementsResolver,
      AnnouncementsResolver,
      AnnouncementService,
      AnnouncementResolver,
      MyAnnouncementsResolver,
      MessageService
   ],
   entryComponents: [
      RolesModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
