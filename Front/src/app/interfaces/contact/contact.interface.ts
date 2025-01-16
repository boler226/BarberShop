export interface Affiliate {
  id: number;
  barbershop: Barbershop;
}

export interface City {
  id: number;
  name: string;
  image: string;
  longitude: number;
  latitude: number;
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
  City: City;
}
