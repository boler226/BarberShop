export interface Country {
  id: number;
  name: string;
  image: string;
  cities: City[];
}

export interface City {
  id: number;
  name: string;
}
