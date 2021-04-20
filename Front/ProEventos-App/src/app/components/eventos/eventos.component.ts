import { Component, OnInit, TemplateRef } from '@angular/core';
import { Event } from '@app/models/Event';
import { EventService } from '@app/services/event.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  modalRef!: BsModalRef;
  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  private filter = '';
  eventId = 0;
  public showingImage = true;
  eventTheme = '';

  public get filtersListBy(): string {
    return this.filter;
  }

  public set filtersListBy(value: string) {
    this.filter = value;
    this.filteredEvents = this.filtersListBy ? this.eventsFilter(this.filtersListBy) : this.events;
  }

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router,
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEvents();
  }

  public getEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (events: Event[]) => {
        this.events = events;
        this.filteredEvents = this.events;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Não foi possível carregar os eventos', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public eventsFilter(filtersListBy: string): Event[] {
    filtersListBy = filtersListBy.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.theme.toLocaleLowerCase().indexOf(filtersListBy) !== -1 ||
        event.location.toLocaleLowerCase().indexOf(filtersListBy) !== -1
    );
  }

  public showImage(): void {
    this.showingImage = !this.showingImage;
  }

  detailEvent(id: number): void {
    this.router.navigate([`detalhe/${id}`]);
  }

  // MODAL
  openModal(event: any, template: TemplateRef<any>, id: number, theme: string): void {
    event.stopPropagation();
    this.eventId = id;
    this.eventTheme = theme;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.eventService.deleteEvent(this.eventId).subscribe({
      next: (result: string) => {
        this.toastr.success('O evento foi excluído com sucesso!', 'Excluído');
        this.getEvents();
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Não foi possível excluir o evento!', 'Operação falhou');
        console.error(error);
      },
      complete: () => { }
    }).add(() => { this.spinner.hide(); });
  }

  decline(): void {
    this.modalRef.hide();
  }

}
