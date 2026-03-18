import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  standalone: true,
  imports: [RouterOutlet],
})
export class AppComponent implements OnInit {
  constructor(private translate: TranslateService) {
    this.translate.addLangs(['en', 'uk', 'ar']);
    this.translate.setDefaultLang('en');
  }

  ngOnInit(): void {
    const savedLang = localStorage.getItem('lang') || 'en';
    const langToUse = savedLang === 'ua' ? 'uk' : savedLang;

    this.translate.use(langToUse);
    this.updateDirection(langToUse);

    this.translate.onLangChange.subscribe((event) => {
      this.updateDirection(event.lang);
      localStorage.setItem('lang', event.lang);
    });
  }

  private updateDirection(lang: string): void {
    const dir = lang === 'ar' ? 'rtl' : 'ltr';

    document.documentElement.dir = dir;
    document.documentElement.lang = lang;
  }
}
