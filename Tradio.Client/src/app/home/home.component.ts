import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  host: {
    class: 'flex-column',
  },
  imports: [CommonModule, RouterLink, TranslateModule],
})
export class HomeComponent {}
