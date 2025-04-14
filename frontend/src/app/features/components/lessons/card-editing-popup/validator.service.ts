import { inject, Injectable } from "@angular/core";
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { BoardService } from "../../../services/board.service";

@Injectable({
    providedIn:'root'
})
export class LessonValidatorService {

    private boardService = inject(BoardService);

    stepValidator(step: number): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
          const value = control.value;
          return value % step === 0
            ? null
            : { step: { requiredStep: step, actual: value } };
        };
    }
      
    wholeNumberValidator(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
          const value = control.value;
          
          if (value == null || value === '') return null;
      
          const parsedValue = parseFloat(value);
          
          return Number.isInteger(parsedValue) && !isNaN(parsedValue)
            ? null
            : { notWholeNumber: { actual: value } };
        };
    }
      
    divisorValidator(): ValidatorFn {
      return (group: AbstractControl): ValidationErrors | null => {
        const maxStudents = group.get('maxStudentsCount')?.value;
        const price = group.get('price')?.value;
    
        if (maxStudents == null || price == null || maxStudents === 0) return null;
    
        return price % maxStudents === 0
          ? null
          : { notDivisible: { price, maxStudents } };
      };
    }
      
    lessonTimeConflictValidator(editingLessonId: number | null): ValidatorFn {
        return (group: AbstractControl): ValidationErrors | null => {
          const day = Number(group.get('day')?.value);
          const hour = Number(group.get('hour')?.value);
          const minutes = Number(group.get('minutes')?.value);
      
          if (day == null || hour == null || minutes == null) return null;
      
          const newStart = hour * 60 + minutes;
          const newEnd = newStart + 60;

          const hasConflict = this.boardService.lessons().some(lesson => {

            if (lesson.lessonId === editingLessonId) return false;
            const lessonStart = lesson.schedule.dayTime.getHours() * 60 + 
                               lesson.schedule.dayTime.getMinutes();
            const lessonEnd = lessonStart + 60;
      
            return lesson.schedule.dayOfWeek === day && 
                   ((newStart >= lessonStart && newStart < lessonEnd) ||
                    (newEnd > lessonStart && newEnd <= lessonEnd));
          });
      
          return hasConflict ? { lessonConflict: true } : null;
        };
      }
}