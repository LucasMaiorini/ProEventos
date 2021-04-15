import { Component, OnInit, TemplateRef } from '@angular/core';
import { Event } from '../../models/Event';
import { EventService } from '../../services/event.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  public showingImage = true;
  private filter = '';
  modalRef!: BsModalRef;

  public get filtersListBy(): string {
    return this.filter;
  }

  public set filtersListBy(value: string) {
    this.filter = value;
    this.filteredEvents = this.filtersListBy ? this.eventsFiltered(this.filter) : this.events;
  }

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEvents();
  }

  public getEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (events: Event[]) => {
        console.log(events);
        this.events = events;
        this.filteredEvents = events;
      },
      error: (error: any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Não foi possível carregar os eventos', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public eventsFiltered(filtersListBy: string): Event[] {
    filtersListBy = filtersListBy.toLocaleLowerCase();
    return this.filteredEvents.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filtersListBy) !== -1 ||
        event.location.toLocaleLowerCase().indexOf(filtersListBy) !== -1
    );
  }

  public showImage(): void {
    this.showingImage = !this.showingImage;
  }

  // MODAL
  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('O evento foi excluído com sucesso!', 'Excluído');
  }

  decline(): void {
    this.modalRef.hide();
  }

}
