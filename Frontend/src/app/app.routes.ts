import { Routes } from '@angular/router';
import { EpisodiosComponent } from './components/episodios/episodios.component';
import { DetalleComponent } from './components/detalle/detalle.component';

export const routes: Routes = [
    { path: '', redirectTo: '/episodios', pathMatch: 'full' },
    { path: 'episodios', component: EpisodiosComponent },
    { path: 'detalle/:id', component: DetalleComponent }
];
