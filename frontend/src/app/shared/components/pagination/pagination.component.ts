import { NgClass, NgFor } from '@angular/common';
import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [NgClass, NgFor],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
export class PaginationComponent {
  page = input.required<number>();
  count = input.required<number>();
  perPage = input.required<number>();
  pagesToShow = input<number>();
  loading = input<boolean>();

  goPrev = output<boolean>();
  goNext = output<boolean>();
  goPage = output<number>();

  onPrev() {
    this.goPrev.emit(true);
  }

  onNext() {
    this.goNext.emit(true);
  }

  onPage(n: number) {
    this.goPage.emit(n);
  }

  totalPages(): number{
    return Math.ceil(this.count() / this.perPage()) || 0;
  }

  isLastPage(): boolean{
    return this.perPage() * this.page() >= this.count();
  }

  getPages(): number[] {
    const totalPages = Math.ceil(this.count() / this.perPage());
    const thisPage = this.page() || 1;
    const pagesToShow = this.pagesToShow() || 9;
    const pages: number[] = [];
    pages.push(thisPage);

    for (let i = 0; i < pagesToShow - 1; i++){
      if (pages.length < pagesToShow) {
        if (Math.min.apply(null, pages) > 1) {
          pages.push(Math.min.apply(null, pages) - 1);
        }
      }

      if (pages.length < pagesToShow) {
        if (Math.max.apply(null, pages) < totalPages) {
          pages.push(Math.max.apply(null, pages) + 1);
        }
      }
    }
    pages.sort((a, b) => a - b);
    return pages;
  }

  getMax() {
    let max = this.perPage() * this.page();
    if (max > this.count()) {
      max = this.count();
    }
    return max;
  }
}
