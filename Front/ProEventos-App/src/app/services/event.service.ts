import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Event } from '../models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseURL = 'https://localhost:5001/api/eventos';
  constructor(private http: HttpClient) { }

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseURL);
  }
  getEventsByTheme(theme: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseURL}/${theme}/tema`);
  }
  getEventsById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseURL}/${id}`);
  }
}
