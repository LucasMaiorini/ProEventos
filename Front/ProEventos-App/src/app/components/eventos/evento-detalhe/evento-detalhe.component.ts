import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form!: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
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

}
