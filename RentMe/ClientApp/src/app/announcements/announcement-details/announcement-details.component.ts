import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Announcement } from 'src/app/_models/announcement';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { MessageService } from 'src/app/_services/message.service';
import { AdminService } from 'src/app/_services/admin.service';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit, AfterViewInit {
  announcement: Announcement;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  loggedInUserId: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService,
    private messageService: MessageService,
    private adminService: AdminService,
    private notifyService: NotificationsService
  ) { }

  ngAfterViewInit() {
    const messagesWindow = document.querySelector('.message-history');
    if (messagesWindow) {
      messagesWindow.setAttribute('style', 'height: 275px !important');
      messagesWindow.scroll(0, messagesWindow.scrollHeight);
    }
  }


  ngOnInit() {
    this.route.data.subscribe(data => {
      this.announcement = data['announcement'];
    });

    this.messageService.setRecipientId(this.announcement.postedById);

    this.galleryOptions = [
      {
        imageSize: 'contain',
        thumbnailSize: 'contain',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
      }
    ];
    this.galleryImages = this.getImages();
    if (this.authService.loggedIn()) {
      this.loggedInUserId = this.authService.decodedToken.nameid;
    }
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.announcement.photos) {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description,
      });
    }
    return imageUrls;
  }

  approveAnnouncement(announcementId: string) {
    this.adminService.approveAnnouncement(announcementId).subscribe(() => {
      this.announcement.isApproved = true;
      this.notifyService.success('Announcement approved');
    }, error => {
      this.notifyService.error(error);
    });
  }

  rejectAnnouncement(announcementId: string) {
    this.adminService.rejectAnnouncement(announcementId).subscribe(() => {
      this.notifyService.success('Announcement rejected');
      this.router.navigate(['admin']);
    }, error => {
      this.notifyService.error(error);
    });
  }

}
