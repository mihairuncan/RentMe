import { Component, OnInit, Inject, Input } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { ActivatedRoute } from '@angular/router';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { NotifyService } from 'src/app/_services/notify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  photos: Photo[] = [];
  announcementId: string;
  baseUrl: string;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;

  // currentMain: Photo;


  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private route: ActivatedRoute,
    private announcementService: AnnouncementService,
    private notificationService: NotifyService
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.announcementId = this.route.snapshot.paramMap.get('announcementId');
    this.initializeUploader();
    this.announcementService.getPhotos(this.announcementId).subscribe(result => {
      this.photos = result;
    }, error => {
      this.notificationService.error(error);
    });
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'api/announcements/' + this.announcementId + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
        // if (photo.isMain) {
        //   this.authService.changeMemberPhoto(photo.url);
        //   this.authService.currentUser.photoUrl = photo.url;
        //   localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
        // }
      }
    };
  }

  setMainPhoto(photo: Photo) {
    //   this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
    //     this.currentMain = this.photos.filter(p => p.isMain === true)[0];
    //     this.currentMain.isMain = false;
    //     photo.isMain = true;
    //     this.authService.changeMemberPhoto(photo.url);
    //     this.authService.currentUser.photoUrl = photo.url;
    //     localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
    //   },
    //     error => {
    //       this.alertify.error(error);
    //     }
    //   );
  }

  deletePhoto(id: number) {
    //   this.alertify.confirm('Are you sure you want to delete this photo?', () => {
    //     this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
    //       this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
    //       this.alertify.success('Photo has been deleted');
    //     }, error => {
    //       this.alertify.error('Failed to delete the photo');
    //     });
    //   });
  }

}
