import { Event } from './Event';
import { Speecher } from './Speecher';

export interface SocialNetwork {
  id: number;
  name: string;
  url: string;
  eventId: number;
  speecherId: number;
}
