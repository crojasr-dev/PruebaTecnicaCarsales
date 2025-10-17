import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { EpisodioResponseDto, EpisodioDto, PersonajeDto } from '../interfaces/episodio.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obtenerEpisodios(page: number = 1): Observable<EpisodioResponseDto> {
    return this.http.get<EpisodioResponseDto>(`${this.baseUrl}/ObtenerEpisodios?page=${page}`);
  }

  obtenerEpisodio(id: number): Observable<EpisodioDto> {
    return this.http.get<EpisodioDto>(`${this.baseUrl}/ObtenerEpisodio?id=${id}`);
  }

  obtenerPersonajes(ids: string): Observable<PersonajeDto[]> {
    return this.http.get<PersonajeDto[]>(`${this.baseUrl}/ObtenerPersonajes?ids=${ids}`);
  }

  
}