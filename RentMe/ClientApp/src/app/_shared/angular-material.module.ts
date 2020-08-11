import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatTooltipModule,
    MatIconModule,
    MatCardModule,
    MatButtonModule
  ],
  exports: [
    MatTooltipModule,
    MatIconModule,
    MatCardModule,
    MatButtonModule
  ]
})
export class AngularMaterialModule { }
