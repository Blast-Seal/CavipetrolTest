import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DetalleClienteComponent } from "./features/clients/pages/detalle-cliente/detalle-cliente.component";

@Component({
  standalone: true,
  imports: [RouterOutlet, DetalleClienteComponent],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('Cavipetrol Test Front');
}
