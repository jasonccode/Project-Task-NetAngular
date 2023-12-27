import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tarea } from '../Interface/Tarea';

@Injectable({
  providedIn: 'root',
})
export class TareaService {

  private edpoint: string = 'https://localhost:7085/api/Tarea/';

  constructor(private http: HttpClient) {}

  getList(): Observable<Tarea[]> {
    return this.http.get<Tarea[]>(`${this.edpoint}Lista`);
  }

  add(request: Tarea): Observable<Tarea> {
    return this.http.post<Tarea>(`${this.edpoint}Agregar`, request);
  }

  delete(idTarea: number): Observable<void> {
    return this.http.delete<void>(`${this.edpoint}Eliminar/${idTarea}`);
  }
}
