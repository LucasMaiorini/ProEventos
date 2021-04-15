import { Batch } from './Batch';
import { SocialNetwork } from './SocialNetwork';
import { Speecher } from './Speecher';

export interface Event {
  id: number;
  location: string;
  eventDate?: Date;
  theme: string;
  numberOfPeople: number;
  imageURL: string;
  phoneNumber: string;
  email: string;
  batches: Batch[];
  socialNetworks: SocialNetwork[];
  speecherEvents: Speecher[];
}
