import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.interface';

@Injectable({
  providedIn: 'root'
})
export class ClienteApiService {
  private http = inject(HttpClient);  
  private apiUrl = 'https://localhost:5001';

  obtenerPorIdentificacion(identificacion: string): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.apiUrl}/api/Cliente/GetByIdentification/${identificacion}`);
  }

  obtenerPorIdentificacionSP(identificacion: string): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.apiUrl}/api/Cliente/GetByIdentificationSP/${identificacion}`);
  }
}