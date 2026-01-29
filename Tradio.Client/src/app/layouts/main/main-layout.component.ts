import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink
  ]
})
export class MainLayoutComponent {}
