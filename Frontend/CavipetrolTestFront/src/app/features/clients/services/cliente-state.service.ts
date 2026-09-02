import { Injectable, inject, signal } from '@angular/core';
import { catchError, of } from 'rxjs';
import { ClienteApiService } from './cliente-api.service';
import { Cliente } from '../models/cliente.interface';

@Injectable({
  providedIn: 'root'
})
export class ClienteStateService {
  private apiService = inject(ClienteApiService);

  private _cliente = signal<Cliente | null>(null);
  private _cargando = signal<boolean>(false);
  private _error = signal<string | null>(null);

  readonly cliente = this._cliente.asReadonly();
  readonly cargando = this._cargando.asReadonly();
  readonly error = this._error.asReadonly();

  buscarCliente(id: string): void {
    const idLimpio = id.trim();
    if (!idLimpio) return;
    
    this._cargando.set(true);
    this._error.set(null);
    this._cliente.set(null);

    this.apiService.obtenerPorIdentificacionSP(idLimpio).pipe(
      catchError((err) => {        
        if (err.status === 404) {
          this._error.set('El cliente solicitado no existe.');
        } else {
          this._error.set('Hubo un problema de conexión con el servidor.');
        }
        this._cargando.set(false);
        return of(null);
      })
    ).subscribe((clienteEncontrado: Cliente | null) => {
      if (clienteEncontrado) {
        this._cliente.set(clienteEncontrado);
        this._cargando.set(false);
      }
    });
  }

  limpiarBuscador(): void {
    this._cliente.set(null);
    this._error.set(null);
    this._cargando.set(false);
  }
}