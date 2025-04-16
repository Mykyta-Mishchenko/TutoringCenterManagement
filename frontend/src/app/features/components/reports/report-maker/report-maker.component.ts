import { NgIf } from '@angular/common';
import { Component, computed, inject, input, OnChanges, OnInit, output, signal } from '@angular/core';
import { ReportDTO } from '../../../../shared/models/dto/reports-dto/report.dto';
import { ReportsService } from '../../../services/reports.service';
import { MarkType } from '../../../../shared/models/mark-type.model';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';
import { dateRangeValidator } from './form.validators';
import { ReportCreatingDTO } from '../../../../shared/models/dto/reports-dto/report-creating.dto';
import { ReportEditingDTO } from '../../../../shared/models/dto/reports-dto/report-editing.dto';

@Component({
  selector: 'app-report-maker',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule],
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

  minDateStr = signal<string>('')
  maxDateStr = signal<string>('')
  
  form!: FormGroup;

  isAdding = computed(() => this.reportId() === null);

  ngOnInit(): void {
    this.markTypes.set(this.reportsService.getMarkTypes());
    this.students.set(this.reportsService.getTeacherStudents(this.teacherId));

    this.setForm();
  }

  ngOnChanges(): void {
    if (!this.isAdding()) {
      this.report.set(this.reportsService.getReport(this.reportId()!));
      this.setForm();
      this.setFormFieldsDisabled();
    }
    else if(this.form){
      this.report.set(null);
      this.setForm();
    }
  }

  closeModal() {
    this.close.emit();
  }

  onSubmit() {
    if (this.form.valid) {
      if (this.isAdding()) {
        const report: ReportCreatingDTO = {
          teacherId: this.authService.User()!.userId,
          studentId: this.form.get('studentId')!.value,
          date: new Date(this.form.get('date')!.value),
          description: this.form.get('description')!.value,
          marks: this.form.get('marks')!.value.map((m: any) => ({
            markTypeId: m.markType,
            markValue: m.markValue
          }))
        };
        this.reportsService.addUserReport(report);
      }
      else {
        const report: ReportEditingDTO = {
          reportId: this.reportId()!,
          description: this.form.get('description')!.value,
        };
        this.reportsService.editReport(report);
      }
      this.closeModal();
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

    const formattedDate = reportDate.toISOString().substring(0, 10);

    this.form = new FormGroup({
      date: new FormControl<string>(formattedDate,
        { validators: [Validators.required, dateRangeValidator(minDate, maxDate)] }),
      studentId: new FormControl<number>(this.report()?.studentId ?? this.students()[0].userId,
        { validators: [Validators.required] }),
      marks: new FormArray(this.markTypes().map((mark) =>
        new FormGroup({
          markType: new FormControl<number>(mark.typeId,
            { validators: [Validators.required] }),
          markValue: new FormControl<number>(
            this.report()?.marks.find(m => m.markTypeId == mark.typeId)?.markValue ?? 0,
            { validators: [Validators.required, Validators.min(0), Validators.max(10)] })
        })
      )),
      description: new FormControl<string>(this.report()?.description ?? '', { validators: [Validators.required, Validators.maxLength(500)] })
    });
  }

  setFormFieldsDisabled(){
    this.form.get('date')?.disable();
    this.form.get('studentId')?.disable();
    this.form.get('marks')?.disable();
  }

  get dateControl() {
    return this.form.get('date');
  }
  
  get studentIdControl() {
    return this.form.get('studentId');
  }
  
  get descriptionControl() {
    return this.form.get('description');
  }
  
  get marksArray(): FormArray {
    return this.form.get('marks') as FormArray;
  }


}
