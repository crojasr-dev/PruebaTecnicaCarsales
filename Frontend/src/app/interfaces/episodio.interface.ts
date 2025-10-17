export interface EpisodioResponseDto {
  info: InfoDto;
  results: EpisodioDto[];
}

export interface InfoDto {
  count: number;
  pages: number;
  next: string | null;
  prev: string | null;
}

export interface EpisodioDto {
  id: number;
  name: string;
  air_date: string;
  episode: string;
  characters: string[];
  url: string;
  created: string;
}

export interface PersonajeDto {
  id: number;
  name: string;
  species: string;
  image: string;
}