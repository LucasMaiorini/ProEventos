import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { take } from 'rxjs/operators';
import { Event } from '../models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseURL = 'https://localhost:5001/api/eventos';

  constructor(private http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseURL).pipe(take(1));
  }
  public getEventsByTheme(theme: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseURL}/${theme}/tema`).pipe(take(1));
  }
  public getEventsById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseURL}/${id}`).pipe(take(1));
  }
  public post(event: Event): Observable<Event> {
    return this.http.post<Event>(this.baseURL, event).pipe(take(1));
  }
  public put(event: Event): Observable<Event> {
    return this.http.put<Event>(`${this.baseURL}/${event.id}`, event).pipe(take(1));
  }
  public deleteEvent(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
