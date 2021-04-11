import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public events: any = [];
  public filteredEvents: any = [];
  showingImage = true;
  private _filtersListBy = '';

  public get filtersListBy(): string {
    return this._filtersListBy;
  }

  public set filtersListBy(value: string) {
    this._filtersListBy = value;
    this.filteredEvents = this.filtersListBy ? this.eventsFiltered(this._filtersListBy) : this.events;
  }

  constructor(public http: HttpClient) { }

  ngOnInit(): void {
    this.getEvents();
  }

  getEvents(): void {
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.events = response;
        this.filteredEvents = response
      },
      error => console.log(error)
    );
  }
  eventsFiltered(filtersListBy: string): any {
    filtersListBy = filtersListBy.toLocaleLowerCase();
    return this.filteredEvents.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filtersListBy) !== -1 ||
        event.location.toLocaleLowerCase().indexOf(filtersListBy) !== -1
    );
  }

  showImage(): void {
    this.showingImage = !this.showingImage;
  }
}
