import { Component, OnInit } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 5000, noPause: false, showIndicators: true } }
  ]
})
export class HomeComponent implements OnInit {
  category = 'clothes';
  constructor() { }

  ngOnInit() {
  }

  changeCategory(category: string) {
    this.category = category;
    console.log(this.category);
  }

}
