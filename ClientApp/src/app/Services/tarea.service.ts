import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tarea } from '../Interface/Tarea';

@Injectable({
  providedIn: 'root',
})
export class TareaService {

  private edpoint: string = 'https://localhost:44401/';
  private apiUrl: string = this.edpoint + 'Tarea/';

  constructor(private http: HttpClient) {}

  getList(): Observable<Tarea[]> {
    return this.http.get<Tarea[]>(`${this.apiUrl}Lista`);
  }

  add(request: Tarea): Observable<Tarea> {
    return this.http.post<Tarea>(`${this.apiUrl}Agregar`, request);
  }

  delete(idTarea: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}Eliminar/${idTarea}`);
  }
}
