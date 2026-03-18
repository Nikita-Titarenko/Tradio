import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { DatabaseService } from '../core/services/database.service';

@Component({
  selector: 'database',
  templateUrl: './database.component.html',
  styleUrls: ['./database.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
})
export class DatabaseComponent implements OnInit {
  selectedFile: File | null = null;
  isDragging = false;
  sucessMessage: string = '';
  errorMessage: string = '';

  constructor(
    public translate: TranslateService,
    public databaseService: DatabaseService,
  ) {}

  ngOnInit(): void {}

  exportInserts() {
    this.databaseService
      .getBackup()
      .subscribe((response: HttpResponse<Blob>) => {
        const blob = response.body;
        if (!blob) return;

        const contentDisposition = response.headers.get('content-disposition');
        const fileName = contentDisposition
          ? contentDisposition.split('filename=')[1]?.trim().replace(/"/g, '')
          : `tradio_dump_${new Date().getTime()}.sql`;

        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = fileName;

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      });
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.handleFile(files[0]);
    }
  }

  onFileSelected(event: any) {
    const files = event.target.files;
    if (files && files.length > 0) {
      this.handleFile(files[0]);
    }
  }

  private handleFile(file: File) {
    if (file.name.endsWith('.sql')) {
      this.selectedFile = file;
    } else {
      this.errorMessage = 'Будь ласка, виберіть файл з розширенням .sql';
    }
  }

  importBackup() {
    if (!this.selectedFile) return;

    const reader = new FileReader();
    reader.onload = (e) => {
      const sqlContent = e.target?.result as string;
      this.databaseService.loadBackup(sqlContent).subscribe({
        next: () => {
          this.sucessMessage = 'Імпорт успішний';
          this.selectedFile = null;
        },
        error: (err) => {
          this.errorMessage = 'Помилка імпорту';
        },
      });
    };
    reader.readAsText(this.selectedFile);
  }
}
