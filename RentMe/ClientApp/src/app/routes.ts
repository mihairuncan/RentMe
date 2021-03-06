import { Routes } from '@angular/router';

import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { UsersResolver } from './_resolvers/users.resolver';
import { AnnouncementListComponent } from './announcements/announcement-list/announcement-list.component';
import { AnnouncementNewComponent } from './announcements/announcement-new/announcement-new.component';
import { PhotoEditorComponent } from './announcements/photo-editor/photo-editor.component';
import { UnapprovedAnnouncementsResolver } from './_resolvers/unapproved-announcements.resolver';
import { AnnouncementsResolver } from './_resolvers/announcements.resolver';
import { AnnouncementDetailsComponent } from './announcements/announcement-details/announcement-details.component';
import { AnnouncementResolver } from './_resolvers/announcement-details.resolver';
import { MyAnnouncementListComponent } from './announcements/my-announcement-list/my-announcement-list.component';
import { MyAnnouncementsResolver } from './_resolvers/my-announcements.resolver';
import { MessagesListComponent } from './messages/messages-list/messages-list.component';
import { ForgotPasswordComponent } from './authentication/forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './authentication/change-password/change-password.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgot-password', component: ForgotPasswordComponent },
    {
        path: 'announcements/:subcategoryName', component: AnnouncementListComponent,
        resolve: { announcements: AnnouncementsResolver }
    },
    {
        path: 'announcement/:announcementId', component: AnnouncementDetailsComponent,
        resolve: { announcement: AnnouncementResolver }
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {
                path: 'admin', component: AdminPanelComponent,
                resolve: { users: UsersResolver, announcements: UnapprovedAnnouncementsResolver },
                data: { roles: ['Admin', 'Moderator'] }
            },
            {
                path: 'new-announcement', component: AnnouncementNewComponent
            },
            {
                path: 'photos/:announcementId', component: PhotoEditorComponent
            },
            {
                path: 'myAnnouncements', component: MyAnnouncementListComponent,
                resolve: { announcements: MyAnnouncementsResolver }
            },
            {
                path: 'edit-announcement/:announcementId', component: AnnouncementNewComponent,
                resolve: { announcement: AnnouncementResolver }
            },
            {
                path: 'myMessages', component: MessagesListComponent
            },
            {
                path: 'change-password', component: ChangePasswordComponent
            }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
