import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Event } from '@app/models/Event';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  locale = 'pt-br';
  form!: FormGroup;
  event!: Event;
  mode = 'post';

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private eventService: EventService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
    this.localeService.use(this.locale);
  }

  ngOnInit(): void {
    this.validation();
    this.loadEvent();

  }

  public validation(): void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      location: ['', Validators.required],
      eventDate: ['', Validators.required],
      numberOfPeople: ['', [Validators.required, Validators.max(120000)]],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  // Returns the Class to set invalid status to Bootstrap if is there any validation error.
  public cssValidator(field: string): any {
    return { 'is-invalid': this.form.get(field)?.errors && this.form.get(field)?.touched };
  }

  get bsConfig(): any {
    return {
      containerClass: 'theme-default',
      isAnimated: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm',
      showWeekNumbers: false,
    };
  }

  loadEvent(): void {
    const eventIdParam = this.activatedRouter.snapshot.paramMap.get('id');
    if (eventIdParam !== null) {
      this.spinner.show();
      this.mode = 'put';
      this.eventService.getEventsById(+eventIdParam).subscribe({
        next: (eventReturned: Event) => {
          this.event = { ...eventReturned };
          this.form.patchValue(this.event);
        },
        error: (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Não foi possível carregar o Evento', 'Falha');
        },
        complete: () => {
          this.spinner.hide();
        },
      });
    }
  }

  public saveChanges(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.event = (this.mode === 'post') ? { ...this.form.value } : { id: this.event.id, ...this.form.value };

      if (this.mode === 'post' || this.mode === 'put') {
        this.eventService[this.mode](this.event).subscribe({
          next: () => { this.toastr.success('Evento atualizado com sucesso!', 'Atualizado'); },
          error: (error: any) => {
            console.log(error);
            this.spinner.hide();
            this.toastr.error('Não foi possível cadastras o novo evento.', 'Falha');
          },
          complete: () => {
            this.spinner.hide();
          }
        });
      }
    }
  }

}
