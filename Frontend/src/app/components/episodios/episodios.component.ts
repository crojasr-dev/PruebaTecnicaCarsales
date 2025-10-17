import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { EpisodioResponseDto, EpisodioDto, InfoDto } from '../../interfaces/episodio.interface';

@Component({
  selector: 'app-episodios',
  imports: [],
  templateUrl: './episodios.component.html'
})
export class EpisodiosComponent implements OnInit {

  episodios: WritableSignal<EpisodioDto[]> = signal([]);
  info: WritableSignal<InfoDto | null> = signal(null);
  paginaActual: WritableSignal<number> = signal(1);
  
  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit() {
    this.cargarEpisodios(1);
  }

  cargarEpisodios(page: number) {
    this.paginaActual.set(page);
    this.apiService.obtenerEpisodios(page).subscribe((response: EpisodioResponseDto) => {
      this.episodios.set(response.results);
      this.info.set(response.info);
    });
  }

  verDetalles(episodioId: number) {
    this.router.navigate(['/detalle', episodioId]);
  }
}
