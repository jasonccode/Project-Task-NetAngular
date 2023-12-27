import { Component, OnInit } from '@angular/core';
import { Tarea } from './Interface/Tarea';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TareaService } from './Services/tarea.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  listaTareas: Tarea[] = [];
  formularioTarea: FormGroup;

  constructor(private _tareaServicio: TareaService, private fb: FormBuilder) {
    this.formularioTarea = this.fb.group({
      nombre: ['', Validators.required],
    });
  }

  obtenerTareas() {
    this._tareaServicio.getList().subscribe({
      next: (data) => {
        this.listaTareas = data;
      },
      error: (e) => {},
    });
  }

  ngOnInit(): void {
    this.obtenerTareas();
  }

  agregarTarea() {
    const request: Tarea = {
      idtarea: 0,
      nombre: this.formularioTarea.value.nombre,
    };

    this._tareaServicio.add(request).subscribe({
      next: (data) => {
        this.listaTareas.push(data);
        this.formularioTarea.patchValue({
          nombre: '',
        });
      },
      error: (e) => {},
    });
  }

  eliminarTarea(tarea: Tarea) {
    console.log('ID de la tarea a eliminar:', tarea.idtarea);

    this._tareaServicio.delete(tarea.idtarea).subscribe({
      next: (data) => {
        const nuevaLista = this.listaTareas.filter(item => item.idtarea != tarea.idtarea);
        this.listaTareas = nuevaLista;
      },
      error: (e) => {
        console.error('Error al eliminar tarea', e);
      },
    });
  }

}
