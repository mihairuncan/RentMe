import { Component, OnInit, Inject, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/_services/notify.service';
import { Router } from '@angular/router';
import { Announcement } from 'src/app/_models/announcement';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { CATEGORIES } from 'src/app/_models/announcement-categories';
import { Subcategory } from 'src/app/_models/subcategory';
import { FileUploader } from 'ng2-file-upload';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-announcement-new',
  templateUrl: './announcement-new.component.html',
  styleUrls: ['./announcement-new.component.css']
})
export class AnnouncementNewComponent implements OnInit {
  baseUrl: string;
  newAnnouncementForm: FormGroup;
  announcement: Announcement;
  categories = CATEGORIES;
  subcategories: Subcategory[];

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private announcementService: AnnouncementService,
    private notificationService: NotifyService,
    private router: Router
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.addNewAnnouncementForm();
  }

  loadSubcategories() {
    this.subcategories = this.categories.find(c => c.name === this.newAnnouncementForm.value['category']).subcategories;
  }

  addNewAnnouncementForm() {
    this.newAnnouncementForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      rentPrice: ['', Validators.required],
      rentPeriod: ['day', Validators.required],
      category: ['', Validators.required],
      subcategoryName: ['', Validators.required]
    });
  }

  addNewAnnouncement() {
    if (this.newAnnouncementForm.valid) {
      this.announcement = Object.assign({}, this.newAnnouncementForm.value);
      this.announcementService.add(this.announcement).subscribe(res => {
        this.router.navigate(['photos/' + res['announcementId']]);
        this.notificationService.success('Announcement successfully added');
      }, error => {
        this.notificationService.error(error);
      }, () => {
        // this.announcementService.login(this.announcement).subscribe(() => {
        //   this.router.navigate(['']);
        // });
      });
    }
  }


}





