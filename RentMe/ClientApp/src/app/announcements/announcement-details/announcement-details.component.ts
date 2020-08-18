import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Announcement } from 'src/app/_models/announcement';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-announcement-details',
  templateUrl: './announcement-details.component.html',
  styleUrls: ['./announcement-details.component.css']
})
export class AnnouncementDetailsComponent implements OnInit {
  announcement: Announcement;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  loggedInUserId: string;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private messageService: MessageService
  ) { }

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

}
