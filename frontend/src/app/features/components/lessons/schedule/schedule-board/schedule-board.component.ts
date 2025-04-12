import { Component, computed, effect, ElementRef, inject, input, OnChanges, OnInit, Renderer2, signal, ViewContainerRef } from '@angular/core';
import { ScheduleCardComponent } from '../schedule-card/schedule-card.component';
import { CardEditingPopupComponent } from "../../card-editing-popup/card-editing-popup.component";
import { Lesson } from '../../../../../shared/models/lesson.model';
import { LessonService } from '../../../../services/lesson.service';
import { AuthService } from '../../../../../core/services/auth/auth.service';
import { HasRoleDirective } from '../../../../../core/directives/role.directive';
import { ModalState } from '../../models/modal-state.enum';
import { BoardService } from '../../../../services/board.service';
import { CardUnsubscribePopupComponent } from "../../card-unsubscribe-popup/card-unsubscribe-popup.component";
import { CardSubscribePopupComponent } from "../../card-subscribe-popup/card-subscribe-popup.component";
import { Roles } from '../../../../../shared/models/roles.enum';
import { UsersService } from '../../../../services/users.service';

@Component({
  selector: 'app-schedule-board',
  standalone: true,
  imports: [CardEditingPopupComponent, HasRoleDirective, CardUnsubscribePopupComponent, CardSubscribePopupComponent],
  templateUrl: './schedule-board.component.html',
  styleUrl: './schedule-board.component.css'
})
export class ScheduleBoardComponent implements OnChanges {
  private boardService = inject(BoardService);
  private authService = inject(AuthService);
  private renderer = inject(Renderer2);
  private el = inject(ElementRef);
  private viewContainerRef = inject(ViewContainerRef);
  private usersService = inject(UsersService);

  minHour = input<number>(8);
  maxHour = input<number>(22);
  minutesStep = input<number>(5);
  userId = input.required<number>();

  isOnOwnPage = input.required<boolean>();
  isOnTeacherPage = signal(false);
  isModalVisible = signal(false);
  selectedLesson = signal<Lesson | null>(null);
  modalState = signal<ModalState>(ModalState.Editing);

  lessons = computed(() => this.boardService.lessons());

  ngOnChanges(): void {
    this.isModalVisible.set(false);
    this.boardService.loadUserLessons(this.userId());
    this.buildBoardGrid();

    this.usersService.getUserRole(this.userId()).subscribe({
      next: (role) => {
        this.isOnTeacherPage.set(role === Roles.Teacher ? true : false);
        this.addLessonBoxes();
      }
    });
  } 

  onModalClose() {
    this.isModalVisible.set(false);
  }

  startLessonEditing(lesson: Lesson) {
    this.selectedLesson.set(lesson);
    this.isModalVisible.set(true);
  }

  onUpdate() {
    this.boardService.loadUserLessons(this.userId());
  }

  addLessonBoxes(){
    const dashboard = this.el.nativeElement.querySelector('#board');

    for (let lesson of this.lessons()) {
      const lessonBox = this.renderer.createElement('div');
      const startHour = lesson.schedule.dayTime.getHours();
      const startMinute = lesson.schedule.dayTime.getMinutes();

      const startRow = (startHour - this.minHour()) * 60 / this.minutesStep() + (startMinute / this.minutesStep()) + 2;
      const endRow = startRow + (60 / this.minutesStep());

      this.renderer.setStyle(lessonBox, 'grid-column', lesson.schedule.dayOfWeek + 1);
      this.renderer.setStyle(lessonBox, 'grid-row', `${startRow} / ${endRow}`);

      this.insertComponentIntoDiv(lessonBox, lesson);
      
      this.renderer.appendChild(dashboard, lessonBox);
    }
  }

  insertComponentIntoDiv(lessonBox: any, lesson: Lesson) {
    const componentRef = this.viewContainerRef.createComponent(ScheduleCardComponent);
    componentRef.setInput('lesson', lesson);

    //TODO with user service getUserRole: if student go on student page
    componentRef.setInput('currentUserRole', this.authService.isTeacher() ? Roles.Teacher : Roles.Student);
    componentRef.setInput('isOnTeacherPage', this.isOnTeacherPage());
    componentRef.setInput('isOnOwnPage', this.isOnOwnPage());
    componentRef.instance.selectedLesson.subscribe(lesson => {
      this.startLessonEditing(lesson);
    });
    lessonBox.appendChild(componentRef.location.nativeElement);
  }

  buildBoardGrid() {
    const dashboard = this.el.nativeElement.querySelector('#board');
    const totalRows = (this.maxHour() - this.minHour()) * (60 / this.minutesStep());
    const daysNames = ['Monday', 'Tuesday', "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

    this.renderer.setStyle(dashboard, 'display', 'grid');
    this.renderer.setStyle(dashboard, 'grid-template-columns', ' 2rem repeat(7, 1fr)');
    this.renderer.setStyle(dashboard, 'grid-template-rows', `repeat(${totalRows}, 1fr)`);

    this.addDaysNames(dashboard, daysNames);
    this.addDaysLines(dashboard, daysNames.length);
    this.addTimeLines(dashboard);    
  }

  addTimeLines(dashboard: any) {
    const totalRows = (this.maxHour() - this.minHour()) * (60 / this.minutesStep());

    for (let i = 0; i < totalRows; i++) {
      const line = this.renderer.createElement('div');

      if (i % (10 / this.minutesStep()) == 0) {
        this.renderer.addClass(line, 'ten-minutes-line');
        this.renderer.setStyle(line, 'grid-column', '2 / -1');
      }
      if (i % (60 / this.minutesStep()) === 0) {
        this.renderer.addClass(line, 'one-hour-line');
        this.renderer.setStyle(line, 'grid-column', '1 / -1');

        this.addTimeLabel(dashboard, i);
      }

      this.renderer.setStyle(line, 'grid-row', i + 1);
      this.renderer.appendChild(dashboard, line);
    }
  }

  addTimeLabel(dashboard: any, i: number) {
    const timeLabel = this.renderer.createElement('div');
    const hour = this.minHour() + (i / (60 / this.minutesStep()));
    const text = this.renderer.createText(`${hour}:00`);

    this.renderer.appendChild(timeLabel, text);
    this.renderer.addClass(timeLabel, 'time-label');
    this.renderer.appendChild(dashboard, timeLabel);

    this.renderer.setStyle(timeLabel, 'grid-row', i + 2);
    this.renderer.setStyle(timeLabel, 'grid-column', '1');
  }

  addDaysNames(dashboard: any, days: string[]){

    for (let i = 0; i < days.length; i++){
      const dayBox = this.renderer.createElement('div');
      const text = this.renderer.createText(days[i]);
      this.renderer.setStyle(dayBox, 'grid-column', i + 2);
      this.renderer.setStyle(dayBox, 'grid-row', '1');
      this.renderer.addClass(dayBox, 'header-text');
      this.renderer.appendChild(dayBox, text);
      this.renderer.appendChild(dashboard, dayBox);
    }
  }

  addDaysLines(dashboard: any, daysCount: number) {
    for (let i = 0; i < daysCount; i++){
      const dayBorder = this.renderer.createElement('div');
      this.renderer.setStyle(dayBorder, 'grid-column', i + 2);
      this.renderer.addClass(dayBorder, 'day-line');
      this.renderer.appendChild(dashboard, dayBorder);
    }
  }
}
