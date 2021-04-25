import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Batch } from '@app/models/Batch';
import { Event } from '@app/models/Event';
import { BatchService } from '@app/services/batch.service';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  eventId: number;
  form: FormGroup;
  mode = 'post';
  locale = 'pt-br';
  event: Event;
  currentBatch = { id: 0, name: '', index: 0 };

  get batches(): FormArray {
    return this.form.get('batches') as FormArray;
  }

  get getControls(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private eventService: EventService,
    private batchService: BatchService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private modalService: BsModalService,
  ) {
    this.localeService.use(this.locale);
  }

  ngOnInit(): void {
    this.validation();
    this.loadEvent();

  }

  //
  //FORMS
  //

  public validation(): void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      location: ['', Validators.required],
      eventDate: ['', Validators.required],
      numberOfPeople: ['', [Validators.required, Validators.max(120000)]],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
      batches: this.fb.array([]),
    });
  }

  loadEvent(): void {
    this.eventId = +this.activatedRouter.snapshot.paramMap.get('id');

    if (this.eventId !== null && this.eventId !== 0) {
      this.spinner.show();
      this.mode = 'put';

      this.eventService.getEventsById(this.eventId).subscribe({
        next: (eventReturned: Event) => {
          this.event = { ...eventReturned };
          this.form.patchValue(this.event);
          this.loadBatches();
        },
        error: (error: any) => {
          console.error(error);
          this.toastr.error('Não foi possível carregar o evento.', 'Erro');
        },
        complete: () => { },
      }).add(() => this.spinner.hide());
    }
  }

  public loadBatches(): void {
    this.batchService.getBatchesFromEvent(this.eventId).subscribe({
      next: (batchesReturned: Batch[]) => {
        batchesReturned.forEach(batch => {
          this.batches.push(this.createBatch(batch));
        });
      },
      error: (error: any) => { this.toastr.error('Não foi possível carregar os lotes.', 'Erro'); },
      complete: () => { }
    }).add(() => this.spinner.hide());
  }

  createBatch(batch: Batch): FormGroup {
    return this.fb.group({
      id: [batch.id],
      name: [batch.name, Validators.required],
      price: [batch.price, Validators.required],
      quantity: [batch.quantity, Validators.required],
      beginDate: [batch.beginDate, Validators.required],
      endDate: [batch.endDate, Validators.required],
    });
  }

  addBatch(): void {
    this.batches.push(this.createBatch({ id: 0 } as Batch));
  }

  //
  // SAVES
  //

  public saveEvents(): void {
    if (this.form.valid) {
      this.spinner.show();
      this.event = (this.mode === 'post') ? { ...this.form.value } : { id: this.event.id, ...this.form.value };
      if (this.mode === 'post' || this.mode === 'put') {
        this.eventService[this.mode](this.event).subscribe({
          next: (eventReturned: Event) => {
            this.toastr.success('Evento atualizado com sucesso!', 'Atualizado');
            this.router.navigate([`detalhe/${eventReturned.id}`]);
          },
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

  public saveBatches(): void {
    if (this.form.controls.batches.valid) {
      this.spinner.show();
      this.batchService.saveBatches(this.eventId, this.form.value.batches).subscribe({
        next: () => {
          this.toastr.success('Lote cadastrado com sucesso!', 'Cadastrado');
          this.router.navigate([`detalhe/${this.eventId}`]);
        },
        error: (error: any) => {
          this.toastr.error('Erro ao tentar salvar os lotes');
          console.log(error);
        },
        complete: () => { }
      }).add(() => { this.spinner.hide(); });
    }
  }

  //
  //DELETE
  //
  public removeBatch(template: TemplateRef<any>, index: number): void {
    this.currentBatch.id = this.batches.get(index + '.id').value;
    this.currentBatch.name = this.batches.get(index + '.name').value;
    this.currentBatch.index = index;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmBatchDelete(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.batchService.deleteBatch(this.eventId, this.currentBatch.id).subscribe({
      next: () => {
        this.toastr.success('O lote foi excluído com sucesso.', 'Deletado');
        this.batches.removeAt(this.currentBatch.index);
      },
      error: (error: any) => {
        this.toastr.error(`Não foi possível excluir o lote ${this.currentBatch.name}.`, 'Falha');
        console.log(error);
      },
      complete: () => { }
    }).add(() => { this.spinner.hide(); });
  }

  public declineBatchDelete(): void {
    this.modalRef.hide();
  }

  //
  //DATES
  //

  get bsConfig(): any {
    return {
      containerClass: 'theme-default',
      adaptivePosition: true,
      isAnimated: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm',
      showWeekNumbers: false,
    };
  }

  public changeDateValue(value: Date, index: number, field: string): void {
    this.batches.value[index][field] = value;
  }

  //
  // OTHERS FUNCTIONS
  //

  // Returns the Class to set invalid status to Bootstrap if is there any validation error.
  public cssValidator(field: FormControl | AbstractControl): any {
    return { 'is-invalid': field.errors && field.touched };
  }

  editMode(): boolean {
    return this.mode === 'put';
  }

  public batchTitle(name: string): string {
    return name === null || name === '' ? 'Novo Lote' : name;
  }

  public resetForm(): void {
    this.form.reset();
  }

}
