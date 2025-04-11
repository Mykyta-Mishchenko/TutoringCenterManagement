import { NgIf } from '@angular/common';
import { FormModel, LessonForm } from './lesson.reactive-form';
import { Component, computed, inject, input, OnChanges, OnInit, output, signal, SimpleChanges} from '@angular/core';
import { Lesson } from '../../../../shared/models/lesson.model';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Subject } from '../../../../shared/models/subject.model';
import { LessonService } from '../../../services/lesson.service';
import { LessonDTO } from '../../../../shared/models/dto/lesson-create.dto';
import { ModalState } from '../models/modal-state.enum';

@Component({
  selector: 'app-card-editing-popup',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],
  templateUrl: './card-editing-popup.component.html',
  styleUrl: './card-editing-popup.component.css'
})
export class CardEditingPopupComponent implements OnInit, OnChanges{

  private lessonService = inject(LessonService);
  private lessonForm = inject(LessonForm);
  lesson = input.required<Lesson | null>();
  show = input.required<boolean>();
  state = input.required<ModalState>();
  close = output();
  update = output();

  form = signal<FormGroup<FormModel>>(this.lessonForm.generateForm());

  isEditing = computed(() => this.state() === ModalState.Editing);

  subjects!: Subject[]; 
  days = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];


  ngOnInit(): void {
    this.subjects = this.lessonService.getSubjects();
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
  }

  closeModal() {
    this.close.emit();
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
      const lesson: LessonDTO = {
        lessonId: this.lesson()?.lessonId ?? null,
        subjectId: this.form().controls.subject.value,
        schoolYear: this.form().controls.schoolYear.value,
        maxStudentsCount: this.form().controls.maxStudentsCount.value,
        day: this.form().controls.day.value,
        hour: this.form().controls.hour.value,
        minutes: this.form().controls.minutes.value,
        price: this.form().controls.price.value
      }
      
      this.closeModal();
      if (this.state() === ModalState.Editing) {
        this.lessonService.updateLesson(lesson);
      }
      else {
        this.lessonService.addLesson(lesson);
      }

      this.update.emit();
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
      subject: this.lesson()?.type.subject.subjectId,
      schoolYear: this.lesson()?.type.schoolYear,
      maxStudentsCount: this.lesson()?.type.maxStudentsCount,
      day: this.lesson()?.schedule.dayOfWeek,
      hour: this.lesson()?.schedule.dayTime.getHours(),
      minutes: this.lesson()?.schedule.dayTime.getMinutes(),
      price: this.lesson()?.type.price
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
    return this.form().controls.maxStudentsCount.touched &&
      this.form().controls.maxStudentsCount.valid
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
    return this.form().controls.price.touched &&
      this.form().controls.price.valid
  }

  markAllTouched() {
    this.form().controls.subject.markAsTouched(); 
    this.form().controls.schoolYear.markAsTouched(); 
    this.form().controls.maxStudentsCount.markAsTouched(); 
    this.form().controls.day.markAsTouched(); 
    this.form().controls.hour.markAsTouched(); 
    this.form().controls.minutes.markAsTouched(); 
    this.form().controls.price.markAsTouched(); 
  }
}
