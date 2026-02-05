import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.css'],
  host: {
    class: 'flex-column',
  },
  imports: [CommonModule],
})
export class HomeComponent {}
