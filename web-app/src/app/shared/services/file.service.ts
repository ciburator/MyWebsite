import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DownloadFile } from '../models/downloadItem.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private _host: string = environment.hostApi + "file/";

  constructor(
    private http: HttpClient) {}

  getItems(): Observable<DownloadFile[]> {
    return this.http.get<DownloadFile[]>(this._host + 'getAll')
  }

  getItem(fileUrl: string) {
    return this.http.get(`${this._host}get?fileUrl=${fileUrl}`, {
      reportProgress: true,
      observe: 'events',
      responseType: 'blob'})
  }

  setItem(){}

  removeItem(){}
}
