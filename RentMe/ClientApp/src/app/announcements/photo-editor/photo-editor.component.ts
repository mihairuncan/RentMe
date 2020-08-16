import { Component, OnInit, Inject } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { NotifyService } from 'src/app/_services/notify.service';
import Swal from 'sweetalert2';
import { AuthenticationService } from 'src/app/_services/auth.service';

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

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private route: ActivatedRoute,
    private router: Router,
    private announcementService: AnnouncementService,
    private notificationService: NotifyService,
    private authService: AuthenticationService
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.announcementId = this.route.snapshot.paramMap.get('announcementId');
    this.announcementService.getAnnouncement(this.announcementId).subscribe(result => {
      if (result.postedById !== this.authService.decodedToken.nameid) {
        this.router.navigate(['/']);
      }
      // console.log(result);
      this.photos = result.photos;
    }, error => {
      this.notificationService.error(error);
    });
    this.initializeUploader();
    // this.announcementService.getPhotos(this.announcementId).subscribe(result => {
    //   this.photos = result;
    // }, error => {
    //   this.notificationService.error(error);
    // });
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
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.announcementService.setMainPhoto(this.announcementId, photo.id).subscribe(() => {
      this.photos.filter(p => p.isMain === true)[0].isMain = false;
      photo.isMain = true;
    }, error => {
      this.notificationService.error(error);
    });
  }

  uploadPhotos() {
    this.uploader.uploadAll();
    Swal.fire(
      'Saved!',
      'Your announcement is waiting for approval.',
      'success'
    );
  }

  deletePhoto(photoId: string) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this photo!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
        this.announcementService.deletePhoto(this.announcementId, photoId).subscribe(() => {
          this.photos.splice(this.photos.findIndex(p => p.id === photoId), 1);
          Swal.fire(
            'Deleted!',
            'Photo has been deleted.',
            'success'
          );
        },
          error => {
            this.notificationService.error(error);
          });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your photo is safe :)',
          'error'
        );
      }
    });
  }

}
