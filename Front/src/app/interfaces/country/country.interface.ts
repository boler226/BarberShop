import {City} from '../city/city.interface';

export interface Country {
  id: number;
  name: string;
  image: string;
  cities: City[];
}

