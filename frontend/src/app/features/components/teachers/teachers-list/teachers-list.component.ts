import { Component, computed, inject, OnInit, signal} from '@angular/core';
import { CardListComponent } from "../card-list/card-list.component";
import { TableListComponent } from "../table-list/table-list.component";
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";
import { UsersService } from '../../../services/users.service';
import { UsersFilter } from '../../../../shared/models/dto/users-filter.dto';
import { SearchFormComponent } from "../search-form/search-form.component";
import { UserInfo } from '../../../../shared/models/dto/user-info.dto';

@Component({
  selector: 'app-teachers-list',
  standalone: true,
  imports: [CardListComponent, TableListComponent, PaginationComponent, SearchFormComponent],
  templateUrl: './teachers-list.component.html',
  styleUrl: './teachers-list.component.css'
})
export class TeachersListComponent implements OnInit {

  private usersService = inject(UsersService);

  teachers = signal<UserInfo[] | null>(null);
  isStandartView = signal<boolean>(true);
  viewName = computed(() => this.isStandartView() ? "cards" : "table");
  filter = signal<UsersFilter>(this.usersService.BasicTeachersFilter);

  total = signal<number>(1);
  page = signal<number>(this.filter().page);
  limit = signal<number>(10);
  perPage = signal<number>(this.filter().perPage);
  loading = false;

  ngOnInit(): void {
    this.getTeachers(this.filter());
  }

  getTeachers(filter: UsersFilter) {
    this.usersService.getUsersByFilter(filter).subscribe({
      next: (usersList) => {
        this.teachers.set(usersList.usersList);
        this.total.set(usersList.totalPageNumber);
      }
    });
  }

  onSwitcherChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isStandartView.update(() => !this.isStandartView());
  }

  goToPrevious() {
    this.page.set(this.page() - 1);
    this.filter.update((current) => ({...current, page: current.page - 1}));
    this.getTeachers(this.filter());
  }

  goToNext() {
    this.page.set(this.page() + 1);
    this.filter.update((current) => ({...current, page: current.page + 1}));
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
