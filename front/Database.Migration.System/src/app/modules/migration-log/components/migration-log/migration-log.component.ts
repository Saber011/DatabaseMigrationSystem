import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MigrationService } from '../../../../api/services/migration.service';
import { UserMigrationData } from '../../../../api/models/user-migration-data';
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-migration-log',
  templateUrl: './migration-log.component.html',
  styleUrls: ['./migration-log.component.css']
})
export class MigrationLogComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  migrationData: UserMigrationData[] = [];
  pagedData: UserMigrationData[] = [];
  isMigrationRunning: boolean = true;

  constructor(private http: HttpClient, private migrationService: MigrationService) {}

  ngOnInit() {
    this.refreshData();
  }

  private refreshData() {
    this.migrationService.apiMigrationGetMigrationJournalDataGet().subscribe(data => {
      this.migrationData = data;
      this.updatePagedData();
    });

  }

  updatePagedData() {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    const endIndex = startIndex + this.paginator.pageSize;
    this.pagedData = this.migrationData.slice(startIndex, endIndex);
  }

  ngAfterViewInit() {
    this.paginator.page.subscribe(() => this.updatePagedData());
  }

  downloadLog(migrationId: string | undefined) {
    if (!migrationId) {
      console.error('ID миграции не определен');
      return;
    }

    // Устанавливаем заголовки запроса, если они требуются для вашего API
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      // Добавьте другие заголовки, если они нужны
    });

    // Определяем URL для запроса
    const url = `${this.migrationService.rootUrl}/api/Migration/ExportLog`;

    // Выполняем запрос с responseType: 'blob'
    this.http.post(url, { id: migrationId }, { headers: headers, responseType: 'blob' }).subscribe(blob => {
      // Создаем URL из полученного Blob
      const downloadUrl = window.URL.createObjectURL(blob);

      // Создаем временный элемент <a> для скачивания файла
      const link = document.createElement('a');
      link.href = downloadUrl;
      link.download = `migration-log-${migrationId}.xlsx`; // Устанавливаем имя файла
      link.click(); // Программно кликаем по ссылке для скачивания

      // Очищаем URL после скачивания
      window.URL.revokeObjectURL(downloadUrl);
    }, error => {
      console.error('Ошибка при скачивании лога миграции:', error);
    });
  }

}
