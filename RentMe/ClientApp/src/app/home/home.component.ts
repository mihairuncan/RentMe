import { Component, OnInit } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import { CATEGORIES } from '../_models/announcement-categories';
import { Subcategory } from '../_models/subcategory';
import { AuthenticationService } from '../_services/auth.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 5000, noPause: false, showIndicators: true } }
  ]
})
export class HomeComponent implements OnInit {
  categories = CATEGORIES;
  selectedCategoryName: string;
  subcategories: Subcategory[];
  loggedIn: boolean;

  constructor(
    private authService: AuthenticationService,
  ) { }

  ngOnInit() {
    this.selectedCategoryName = this.categories[0].name;
    this.loadSubcategories(this.selectedCategoryName);


   this.authService.userIsLoggedIn.subscribe(loggedIn => this.loggedIn = loggedIn);
  }

  loadSubcategories(categoryName: string) {
    this.subcategories = this.categories.find(c => c.name === categoryName).subcategories;
  }

  changeCategory(categoryName: string) {
    this.selectedCategoryName = categoryName;
    this.loadSubcategories(categoryName);
  }
}
