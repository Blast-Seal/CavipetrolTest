import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteStateService } from '../../services/cliente-state.service';

@Component({
  selector: 'app-detalle-cliente',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './detalle-cliente.component.html',
  styleUrls: []
})
export class DetalleClienteComponent {  
  protected state = inject(ClienteStateService);

  idBusqueda = signal<string>('');

  ejecutarBusqueda(): void {
    this.state.buscarCliente(this.idBusqueda());
  }
}