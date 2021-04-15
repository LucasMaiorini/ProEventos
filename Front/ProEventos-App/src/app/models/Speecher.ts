import { Event } from './Event';
import { SocialNetwork } from './SocialNetwork';

export interface Speecher {
  id: number;
  name: string;
  resume: string;
  imageUrl: string;
  phoneNumber: string;
  email: string;
  socialNetworks: SocialNetwork[];
  speecherEvents: Event[];
}
