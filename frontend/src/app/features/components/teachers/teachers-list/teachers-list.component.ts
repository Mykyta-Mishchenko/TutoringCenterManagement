import { Component, computed, inject, signal } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { TeachersService } from '../services/teachers.service';
import { CardListComponent } from "../card-list/card-list.component";
import { TableListComponent } from "../table-list/table-list.component";

const SAMPLE_TEACHERS_LIST: Teacher[] = [
  
]

@Component({
  selector: 'app-teachers-list',
  standalone: true,
  imports: [CardListComponent, TableListComponent],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.css'
})
export class TeachersListComponent {
  
  private teacgersService = inject(TeachersService);

  teachers = this.teacgersService.teachersList;

  isStandartView = signal<boolean>(false);

  viewName = computed(() => this.isStandartView() ? "cards" : "table");

  onSwitcherChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isStandartView.update(() => !this.isStandartView());
  }
}
