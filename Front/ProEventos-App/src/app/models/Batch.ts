import { Event } from './Event';

export interface Batch {
  id: number;
  name: string;
  price: number;
  beginDate: Date;
  endDate: Date;
  quantity: number;
  eventId: number;
  event: Event;

  // constructor(id?: number) {
  //   if (id === undefined || id < 1) { this.id = 0; }
  // }
}
