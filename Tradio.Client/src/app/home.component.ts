import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: 'home.component.html',
  imports: [
    CommonModule,
    RouterLink,
    RouterOutlet
  ]
})

export class HomeComponent { }
