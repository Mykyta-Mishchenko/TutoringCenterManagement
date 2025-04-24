import { DatePipe, NgIf } from '@angular/common';
import { Component, computed, inject, input, OnChanges, OnInit, output, signal } from '@angular/core';
import { ReportDTO } from '../../../../shared/models/dto/reports-dto/report.dto';
import { ReportsService } from '../../../services/reports.service';
import { MarkType } from '../../../../shared/models/mark-type.model';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';
import { ReportCreatingDTO } from '../../../../shared/models/dto/reports-dto/report-creating.dto';
import { ReportEditingDTO } from '../../../../shared/models/dto/reports-dto/report-editing.dto';
import { ReportScheduleDTO } from '../../../../shared/models/dto/reports-dto/lesson-schedule.dto';
import { startWith } from 'rxjs';

@Component({
  selector: 'app-report-maker',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, DatePipe],
  templateUrl: './report-maker.component.html',
  styleUrl: './report-maker.component.css'
})
export class ReportMakerComponent implements OnChanges, OnInit{

  private authService = inject(AuthService);
  private reportsService = inject(ReportsService);

  reportId = input.required<number | null>();
  show = input.required<boolean>();
  close = output();

  teacherId = this.authService.User()!.userId;
  isBadRequest = signal<boolean>(false);
  report = signal<ReportDTO | null>(null);
  markTypes = signal<MarkType[]>([]);
  students = signal<SearchUserDTO[]>([]);

  studentSchedule = signal<ReportScheduleDTO[]>([]);
  isAnyLessons = signal<boolean>(false);
  selectedStudentId = signal<number | null>(null);

  previousSelectedStudentId = -1;

  minDateStr = signal<string>('')
  maxDateStr = signal<string>('')
  
  form!: FormGroup;

  isAdding = computed(() => this.reportId() === null);

  ngOnInit(): void {
    this.reportsService.getMarkTypes().subscribe({
      next: (markTypes) => {
        this.markTypes.set(markTypes);    
      }
    })
    this.reportsService.getTeacherStudents(this.teacherId).subscribe({
      next: (students) => {
        this.students.set(students);
        this.setForm();
      }
    })
  }

  ngOnChanges(): void {
    if (!this.isAdding()) {
      this.reportsService.getReport(this.reportId()!).subscribe({
        next: (report) => {
          this.report.set(report);
          this.setForm();
          this.studentSchedule().push({ lessonId: report.reportId, date: report.date });
          this.setEditingFormFieldsDisabled();
        },
        error: () => {
          this.close.emit();
        }
      })
    }
    else if(this.form){
      this.report.set(null);
      this.setForm();
      this.updateStudentSchedule();
    }
  }

  closeModal() {
    this.close.emit();
  }

  onStudentChange(studentId: number) {
    if (studentId && studentId != this.previousSelectedStudentId) {
      this.selectedStudentId.set(studentId);
      this.updateStudentSchedule();

      this.previousSelectedStudentId = studentId;
    }
  }

  updateStudentSchedule() {
    if (this.selectedStudentId()) {
      this.reportsService.getStudentSchedule(this.teacherId, this.selectedStudentId()!).subscribe({
        next: (schedule) => {
          this.studentSchedule.set(schedule);
          if (this.studentSchedule().length > 0) {
            this.setCreatingFormFieldsEnabled();
            this.isAnyLessons.set(true);
          }
          else {
            this.setCreatingFormFieldsDisabled();
            this.isAnyLessons.set(false);
          }
        }
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      if (this.isAdding()) {
        const lessonDate = this.form.get('lessonDate')!.value;
        const lessonId = this.studentSchedule().find(l => l.date == lessonDate)!.lessonId;

        const report: ReportCreatingDTO = {
          teacherId: this.authService.User()!.userId,
          studentId: this.form.get('studentId')!.value,
          teacherLessonId: lessonId,
          date: lessonDate,
          description: this.form.get('description')!.value,
          marks: this.form.get('marks')!.value.map((m: any) => ({
            markTypeId: m.markType,
            markValue: m.markValue
          }))
        };

        this.reportsService.addReport(report).subscribe({
          next: () => {
            this.closeModal();     
          }
        });
      }
      else {
        const report: ReportEditingDTO = {
          reportId: this.reportId()!,
          description: this.form.get('description')!.value,
        };
        this.reportsService.editReport(report).subscribe({
          next: () => {
            this.closeModal(); 
          }
        });
      }
    }
  }

  getMarkValue(report: ReportDTO | null, typeId: number): number | null {
    if (report == null) return null;
    const found = report.marks.find(m => m.markTypeId === typeId);
    return found ? found.markValue : null;
  }

  getMarkType(typeId: number): string | null {
    const found = this.markTypes().find(m => m.typeId === typeId);
    return found ? found.name.replace(' score','') : null;
  }

  get marks(): FormArray {
    return this.form.get('marks') as FormArray;
  }

  getRange(start: number, end: number, step:number = 1): number[] {
    return Array(end/step - start + 1).fill(0).map((_, idx) => start + idx*step);
  }
  
  setForm() {
    const today = new Date();
    const minDate = new Date(today.getFullYear(), today.getMonth(), 1); 
    const maxDate = today;

    this.minDateStr.set(minDate.toISOString().split('T')[0]);
    this.maxDateStr.set(maxDate.toISOString().split('T')[0]);

    const reportDate = this.report()?.date ? new Date(this.report()!.date) : today;

    this.form = new FormGroup({
      studentId: new FormControl<number | null>({ value: this.report()?.studentId ?? this.students()[0]?.userId , disabled: !this.isAdding()},
        { validators: [Validators.required] }),
      lessonDate: new FormControl<Date | null>({ value: this.report()?.date ?? null, disabled: !this.isAdding() },
        { validators: [Validators.required]}),
      marks: new FormArray(this.markTypes().map((mark) =>
        new FormGroup({
          markType: new FormControl<number>(mark.typeId,
            { validators: [Validators.required] }),
          markValue: new FormControl<number>(
            { value: this.report()?.marks.find(m => m.markTypeId == mark.typeId)?.markValue ?? 0, disabled: !this.isAdding() },
            { validators: [Validators.required, Validators.min(0), Validators.max(10)] })
        })
      )),
      description: new FormControl<string>(this.report()?.description ?? '', { validators: [Validators.required, Validators.maxLength(500)] })
    });

    this.form.get('studentId')?.valueChanges
    .pipe(startWith(this.form.get('studentId')?.value))
    .subscribe((studentId: number) => {
      this.onStudentChange(studentId);
    });
  }

  setEditingFormFieldsDisabled(){
    this.form.get('date')?.disable();
    this.form.get('studentId')?.disable();
    this.form.get('marks')?.disable();
  }

  setCreatingFormFieldsDisabled() {
    this.form.get('lessonDate')?.disable();
    this.form.get('marks')?.disable();
    this.form.get('description')?.disable();
  }

  setCreatingFormFieldsEnabled() {
    this.form.get('lessonDate')?.enable();
    this.form.get('marks')?.enable();
    this.form.get('description')?.enable();
  }
  
  get studentIdControl() {
    return this.form.get('studentId');
  }

  get lessonDateControl() {
    return this.form.get('lessonDate');
  }
  
  get descriptionControl() {
    return this.form.get('description');
  }
  
  get marksArray(): FormArray {
    return this.form.get('marks') as FormArray;
  }
}
