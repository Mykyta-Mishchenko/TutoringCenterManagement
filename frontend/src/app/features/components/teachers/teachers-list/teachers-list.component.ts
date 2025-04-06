import { Component, computed, inject, OnInit, Signal, signal } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { TeachersService } from '../services/teachers.service';
import { CardListComponent } from "../card-list/card-list.component";
import { TableListComponent } from "../table-list/table-list.component";
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";

const SAMPLE_TEACHERS_LIST: Teacher[] = [
  
]

@Component({
  selector: 'app-teachers-list',
  standalone: true,
  imports: [CardListComponent, TableListComponent, PaginationComponent],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.css'
})
export class TeachersListComponent implements OnInit {
  
  private teachersService = inject(TeachersService);

  teachers!: Signal<Teacher[]>;
  isStandartView = signal<boolean>(true);
  viewName = computed(() => this.isStandartView() ? "cards" : "table");

  subjects = this.teachersService.subjects;

  total = 10;
  page = 1;
  limit = 1;
  loading = false;

  ngOnInit(): void {
    this.getTeachers();
  }

  getRange(start: number, end: number): number[] {
    return Array(end - start + 1).fill(0).map((_, idx) => start + idx);
  }

  getTeachers() {
    this.teachers = this.teachersService.teachersList;
  }

  onSwitcherChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isStandartView.update(() => !this.isStandartView());
  }

  goToPrevious() {
    this.page--;
    this.getTeachers();
  }

  goToNext() {
    this.getTeachers();
  }

  goToPage(n: number) {
    this.page = n;
    this.getTeachers();
  }
}
