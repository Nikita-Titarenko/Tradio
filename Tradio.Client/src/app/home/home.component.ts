import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.css'],
  host: {
    class: 'flex-column',
  },
  imports: [CommonModule, RouterLink],
})
export class HomeComponent {}
