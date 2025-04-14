import { NgIf } from '@angular/common';
import { FormModel, LessonForm } from './lesson.reactive-form';
import { Component, computed, inject, input, OnChanges, OnInit, output, signal, SimpleChanges} from '@angular/core';
import { Lesson } from '../../../../shared/models/lesson.model';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Subject } from '../../../../shared/models/subject.model';
import { LessonService } from '../../../services/lesson.service';
import { ModalState } from '../models/modal-state.enum';
import { LessonEditDTO } from '../../../../shared/models/dto/lesson-edit.dto';
import { LessonCreateDTO } from '../../../../shared/models/dto/lesson-create.dto';
import { BoardService } from '../../../services/board.service';

@Component({
  selector: 'app-card-editing-popup',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './card-editing-popup.component.html',
  styleUrl: './card-editing-popup.component.css'
})
export class CardEditingPopupComponent implements OnInit, OnChanges{

  private boardService = inject(BoardService)
  private lessonService = inject(LessonService);
  private lessonForm = inject(LessonForm);
  lesson = input.required<Lesson | null>();
  show = input.required<boolean>();
  state = input.required<ModalState>();
  userId = input.required<number>();
  close = output();

  form = signal<FormGroup<FormModel>>(this.lessonForm.generateForm());

  isEditing = computed(() => this.state() === ModalState.Editing);

  isIncorrectResponse = signal<boolean>(false);

  subjects = signal<Subject[] | null>(null); 
  days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];


  ngOnInit(): void {
    this.lessonService.getSubjects().subscribe({
      next: (subjects) => {
        this.subjects.set(subjects);
      }
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.lesson() && this.form) {
      this.form().reset();
    }
    if (this.state() === ModalState.Editing && this.lesson()) {
      this.lessonForm.setLessonId(this.lesson()?.lessonId ?? null);
      this.form.set(this.lessonForm.generateForm());
      this.loadLessonInfo();
    }
    this.form().controls.day.markAsTouched();
    this.form().controls.hour.markAsTouched();
    this.form().controls.minutes.markAsTouched();
  }

  closeModal() {
    this.close.emit();
  }

  onDelete() {
    if (this.lesson()?.lessonId) {
      this.lessonService.deleteLesson(this.lesson()!.lessonId).subscribe({
        next: (response) => {
          this.boardService.loadUserLessons(this.userId()).subscribe();
          this.closeModal();
        }
      });
    }
  }

  onSubmit() {
    if (
      this.subjectIsValid &&
      this.schoolYearIsValid &&
      this.maxStudentsCountIsValid &&
      this.dayIsValid &&
      this.hourIsValid &&
      this.minutesIsValid &&
      this.priceIsValid &&
      this.timeIsValid
    ) {
      if (this.state() === ModalState.Editing) {
        this.lessonService.updateLesson(this.getEditLessonDTO()).subscribe({
          next: (response) => {
            this.boardService.loadUserLessons(this.userId()).subscribe();
            this.closeModal();
            this.isIncorrectResponse.set(false);
          },
          error: (error) => {
            this.isIncorrectResponse.set(true);
          }
        });
      }
      else {
        this.lessonService.addLesson(this.getCreateLessonDTO()).subscribe({
          next: (response) => {  
            this.boardService.loadUserLessons(this.userId()).subscribe();
            this.closeModal();
            this.isIncorrectResponse.set(false);
          },
          error: (error) => {
            this.isIncorrectResponse.set(true);
          }
        });
      }
    }
    else {
      this.markAllTouched();
    }
  }

  getRange(start: number, end: number, step:number = 1): number[] {
    return Array(end/step - start + 1).fill(0).map((_, idx) => start + idx*step);
  }

  loadLessonInfo() {
    this.form().patchValue({
      subject: this.lesson()?.lessonType.subject.subjectId,
      schoolYear: this.lesson()?.lessonType.schoolYear,
      day: this.lesson()?.schedule.dayOfWeek,
      hour: this.lesson()?.schedule.dayTime.getHours(),
      minutes: this.lesson()?.schedule.dayTime.getMinutes(),
      groupedPriceSettings: {
        maxStudentsCount: this.lesson()?.lessonType.maxStudentsCount,
        price: this.lesson()?.lessonType.price
      }
    })
    this.markAllTouched();
  }

  get subjectIsValid() {
    return this.form().controls.subject.touched &&
      this.form().controls.subject.valid
  }
  get schoolYearIsValid() {
    return this.form().controls.schoolYear.touched &&
      this.form().controls.schoolYear.valid
  }
  get maxStudentsCountIsValid() {
    return this.form().controls.groupedPriceSettings.controls.maxStudentsCount.touched &&
      this.form().controls.groupedPriceSettings.controls.maxStudentsCount.valid
  }
  get dayIsValid() {
    return this.timeIsValid && this.form().controls.day.touched &&
      this.form().controls.day.valid
  }
  get hourIsValid() {
    return this.timeIsValid && this.form().controls.hour.touched &&
      this.form().controls.hour.valid
  }
  get minutesIsValid() {
    return this.timeIsValid && this.form().controls.minutes.touched &&
      this.form().controls.minutes.valid
  }
  get timeIsValid() {
    return !this.form().hasError('lessonConflict')
      && this.form().controls.day.touched
      && this.form().controls.hour.touched
      && this.form().controls.minutes.touched;
  }
  get priceIsValid() {
    return this.form().controls.groupedPriceSettings.controls.price.touched &&
      this.form().controls.groupedPriceSettings.valid
  }

  markAllTouched() {
    this.form().controls.subject.markAsTouched(); 
    this.form().controls.schoolYear.markAsTouched(); 
    this.form().controls.groupedPriceSettings.controls.maxStudentsCount.markAsTouched(); 
    this.form().controls.day.markAsTouched(); 
    this.form().controls.hour.markAsTouched(); 
    this.form().controls.minutes.markAsTouched(); 
    this.form().controls.groupedPriceSettings.controls.price.markAsTouched(); 
  }

  getCreateLessonDTO(): LessonCreateDTO {
    return {
      userId: this.userId(),
      subjectId: this.form().controls.subject.value,
      schoolYear: this.form().controls.schoolYear.value,
      maxStudentsCount: this.form().controls.groupedPriceSettings.controls.maxStudentsCount.value,
      day: this.form().controls.day.value,
      hour: this.form().controls.hour.value,
      minutes: this.form().controls.minutes.value,
      price: this.form().controls.groupedPriceSettings.controls.price.value
    } 
  }

  getEditLessonDTO(): LessonEditDTO {
    return {
      lessonId: this.lesson()!.lessonId,
      userId: this.userId(),
      subjectId: this.form().controls.subject.value,
      schoolYear: this.form().controls.schoolYear.value,
      maxStudentsCount: this.form().controls.groupedPriceSettings.controls.maxStudentsCount.value,
      day: this.form().controls.day.value,
      hour: this.form().controls.hour.value,
      minutes: this.form().controls.minutes.value,
      price: this.form().controls.groupedPriceSettings.controls.price.value
    }
  }
}
