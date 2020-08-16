import { Component, OnInit, Inject, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotifyService } from 'src/app/_services/notify.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Announcement } from 'src/app/_models/announcement';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { CATEGORIES } from 'src/app/_models/announcement-categories';
import { Subcategory } from 'src/app/_models/subcategory';
import { AuthenticationService } from 'src/app/_services/auth.service';
import Swal from 'sweetalert2';

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
  @Input() announcementId: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private fb: FormBuilder,
    private announcementService: AnnouncementService,
    private notificationService: NotifyService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthenticationService
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.announcement = data['announcement'];
    });
    if (this.announcement && this.announcement.postedById !== this.authService.decodedToken.nameid) {
      this.router.navigate(['/']);
    }
    this.addNewAnnouncementForm();
  }

  loadSubcategories() {
    this.subcategories = this.categories.find(c => c.name === this.newAnnouncementForm.value['category']).subcategories;
  }

  addNewAnnouncementForm() {
    if (this.announcement) {
      this.newAnnouncementForm = this.fb.group({
        id: [this.announcement.id, Validators.required],
        title: [this.announcement.title, Validators.required],
        description: [this.announcement.description, Validators.required],
        rentPrice: [this.announcement.rentPrice, Validators.required],
        rentPeriod: [this.announcement.rentPeriod, Validators.required],
        category: ['', Validators.required],
        subcategoryName: ['', Validators.required]
      });
    } else {
      this.newAnnouncementForm = this.fb.group({
        title: ['', Validators.required],
        description: ['', Validators.required],
        rentPrice: ['', Validators.required],
        rentPeriod: ['day', Validators.required],
        category: ['', Validators.required],
        subcategoryName: ['', Validators.required]
      });
    }
  }

  upsertAnnouncement() {
    if (this.newAnnouncementForm.valid) {
      this.announcement = Object.assign({}, this.newAnnouncementForm.value);
      if (!this.announcement.id) {
        this.announcementService.add(this.announcement).subscribe(res => {
          this.router.navigate(['photos/' + res['announcementId']]);
          this.notificationService.success('Announcement successfully added');
        }, error => {
          this.notificationService.error(error);
        });
      } else {
        this.announcementService.update(this.announcement).subscribe(res => {
          this.router.navigate(['photos/' + res['announcementId']]);
          // this.notificationService.success('Announcement successfully updated');
        }, error => {
          this.notificationService.error(error);
        }, () => {
          Swal.fire(
            'Saved!',
            'Your announcement is waiting for approval.',
            'success'
          );
        });
      }

    }
  }
}





