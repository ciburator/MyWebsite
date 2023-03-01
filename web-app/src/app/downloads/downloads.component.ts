import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { DownloadFile } from '../shared/models/downloadItem.model';
import { FileService } from '../shared/services/file.service';

@Component({
  selector: 'app-downloads',
  templateUrl: './downloads.component.html',
  styleUrls: ['./downloads.component.scss']
})
export class DownloadsComponent implements OnInit {

  files$: Observable<DownloadFile[]> = this.fileService.getItems();

  constructor(private fileService: FileService,
    private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
  }

  getSafeUrl(url: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  download(item: DownloadFile) {
    this.fileService.getItem(item.url).subscribe((event) => {
      if (event.type === HttpEventType.UploadProgress && event.total){
        item.progress = Math.round((100 * event.loaded) / event.total);
        console.log(item);
      }
      else if (event.type === HttpEventType.Response) {
          this.downloadFile(event, item.name);
      }
    })
  }

  private downloadFile = (data: HttpResponse<Blob>, name: string) => {
    if(data.body) {
      const downloadedFile = new Blob([data.body], { type: data.body.type });
      const a = document.createElement('a');
      a.setAttribute('style', 'display:none;');
      document.body.appendChild(a);
      a.download = name;
      a.href = URL.createObjectURL(downloadedFile);
      a.target = '_blank';
      a.click();
      document.body.removeChild(a);
    }
  }
}
