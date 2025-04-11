import { Component, computed, inject, OnInit, signal} from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { CardListComponent } from "../card-list/card-list.component";
import { TableListComponent } from "../table-list/table-list.component";
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";
import { UsersService } from '../../../services/users.service';
import { Roles } from '../../../../shared/models/roles.enum';
import { UsersFilter } from '../../../../shared/models/dto/users-filter.dto';
import { SearchFormComponent } from "../search-form/search-form.component";

@Component({
  selector: 'app-teachers-list',
  standalone: true,
  imports: [CardListComponent, TableListComponent, PaginationComponent, SearchFormComponent],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.css'
})
export class TeachersListComponent implements OnInit {

  private usersService = inject(UsersService);

  teachers!: Teacher[];
  isStandartView = signal<boolean>(true);
  viewName = computed(() => this.isStandartView() ? "cards" : "table");
  filter = signal<UsersFilter>(this.usersService.BasicTeachersFilter);

  total = signal<number>(10);
  page = signal<number>(1);
  limit = signal<number>(1);
  loading = false;

  ngOnInit(): void {
    this.getTeachers(this.filter());
  }

  getTeachers(filter: UsersFilter) {
    this.teachers = this.usersService.getUsersByFilter(filter);
  }

  onSwitcherChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isStandartView.update(() => !this.isStandartView());
  }

  goToPrevious() {
    this.page.set(this.page() - 1);
    this.getTeachers(this.filter());
  }

  goToNext() {
    this.page.set(this.page() + 1);
    this.getTeachers(this.filter());
  }

  goToPage(n: number) {
    this.page.set(n);
    this.filter.update(current => ({
      ...current,
      page: n
    }));
    this.getTeachers(this.filter());
  }

  onFilter(filter: UsersFilter) {
    this.filter.set(filter);
    this.getTeachers(filter)
  }
}
