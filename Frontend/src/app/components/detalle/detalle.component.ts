import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { EpisodioDto, PersonajeDto } from '../../interfaces/episodio.interface';

@Component({
  selector: 'app-detalle',
  imports: [RouterLink, DatePipe],
  templateUrl: './detalle.component.html'
})
export class DetalleComponent implements OnInit {
  episodioId: WritableSignal<number> = signal(0);
  episodio: WritableSignal<EpisodioDto | null> = signal(null);
  personajes: WritableSignal<PersonajeDto[]> = signal([]);

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.episodioId.set(+params['id']);
      this.cargarEpisodio();
    });
  }

  cargarEpisodio() {
    this.apiService.obtenerEpisodio(this.episodioId()).subscribe((data: EpisodioDto) => {
      this.episodio.set(data);
      this.cargarPersonajes();
    });
  }

  cargarPersonajes() {
    const episodio = this.episodio();
    if (episodio && episodio.characters && episodio.characters.length > 0) {
      const ids = episodio.characters.map(url => {
        const parts = url.split('/');
        return parts[parts.length - 1];
      }).join(',');
      
      this.apiService.obtenerPersonajes(ids).subscribe((data: PersonajeDto[]) => {
        this.personajes.set(data);
      });
    }
  }
}
