import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-announcement-list',
  templateUrl: './announcement-list.component.html',
  styleUrls: ['./announcement-list.component.css']
})
export class AnnouncementListComponent implements OnInit {

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    console.log(this.route.snapshot.paramMap.get('subcategoryName'));
  }

}
