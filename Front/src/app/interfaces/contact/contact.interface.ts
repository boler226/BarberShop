import {City} from '../city/city.interface';

export interface Affiliate {
  id: number;
  barbershops: Barbershop[];
  city: City;
}

export interface Barbershop {
  id: number;
  name: string;
  phone: string;
  address: Address;
}

export interface Address {
  id: number;
  street: string;
  houseNumber: string;
  longitude: number;
  latitude: number;
  city: City;
}
