import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-subcategory-card',
  templateUrl: './subcategory-card.component.html',
  styleUrls: ['./subcategory-card.component.css']
})
export class SubcategoryCardComponent implements OnInit {
  @Input() linkText: string;
  @Input() subcategoryName: string;
  @Input() imageUrl: string;

  constructor() { }

  ngOnInit() {
  }

}
