import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { UsersResolver } from './_resolvers/users.resolver';
import { AnnouncementListComponent } from './announcements/announcement-list/announcement-list.component';
import { AnnouncementNewComponent } from './announcements/announcement-new/announcement-new.component';
import { PhotoEditorComponent } from './announcements/photo-editor/photo-editor.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'announcements/:subcategoryName', component: AnnouncementListComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {
                path: 'admin', component: AdminPanelComponent,
                resolve: { users: UsersResolver },
                data: { roles: ['Admin'] }
            },
            {
                path: 'new-announcement', component: AnnouncementNewComponent
            },
            {
                path: 'photos/:announcementId', component: PhotoEditorComponent
            }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
