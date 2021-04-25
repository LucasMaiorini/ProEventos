import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Batch } from '@app/models/Batch';
import { Observable } from 'rxjs/internal/Observable';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  baseURL = 'https://localhost:5001/api/lotes';

  constructor(private http: HttpClient) { }

  public getBatchesFromEvent(eventId: number): Observable<Batch[]> {
    return this.http.get<Batch[]>(`${this.baseURL}/${eventId}`).pipe(take(1));
  }
  public saveBatches(eventId: number, batches: Batch[]): Observable<Batch[]> {
    console.log(batches);
    return this.http.put<Batch[]>(`${this.baseURL}/${eventId}`, batches).pipe(take(1));
  }
  public deleteBatch(eventId: number, batchId: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${eventId}/${batchId}`).pipe(take(1));
  }
}
