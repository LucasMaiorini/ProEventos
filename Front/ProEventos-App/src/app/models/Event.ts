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

  // id?: number;
  // location?: string;
  // eventDate?: Date;
  // theme?: string;
  // numberOfPeople?: number;
  // imageURL?: string;
  // phoneNumber?: string;
  // email?: string;
  // batches?: Batch[];
  // socialNetworks?: SocialNetwork[];
  // speecherEvents?: Speecher[];


  // constructor(id: number, location: string, eventDate: Date, theme: string, numberOfPeople: number, imageURL: string,
  //             phoneNumber: string, email: string, batches: Batch[], socialNetworks: SocialNetwork[], speecherEvents: Speecher[]) {
  //   this.id = id;
  //   this.location = location;
  //   this.eventDate = eventDate;
  //   this.theme = theme;
  //   this.numberOfPeople = numberOfPeople;
  //   this.imageURL = imageURL;
  //   this.phoneNumber = phoneNumber;
  //   this.email = email;
  //   this.batches = batches;
  //   this.socialNetworks = socialNetworks;
  //   this.speecherEvents = speecherEvents;
  // }
}
