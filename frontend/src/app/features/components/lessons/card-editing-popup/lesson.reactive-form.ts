import { FormControl, FormGroup, Validators } from "@angular/forms";
import { LessonValidatorService } from "./validator.service";
import { inject, Injectable, input, signal } from "@angular/core";

export interface FormModel {
    subject: FormControl<number>,
    schoolYear: FormControl<number>
    maxStudentsCount: FormControl<number>,
    day: FormControl<number>,
    hour: FormControl<number>,
    minutes: FormControl<number>,
    price: FormControl<number>
}

@Injectable({
    providedIn: 'root'
})
export class LessonForm{

    private validatorService = inject(LessonValidatorService);

    private lessonId = signal<number | null>(null);

    generateForm(): FormGroup<FormModel>{
        return  new FormGroup<FormModel>({
            subject: new FormControl<number>(0, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(1)]
            }),
            schoolYear: new FormControl<number>(1, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(1), Validators.max(12), this.validatorService.wholeNumberValidator()]
            }),
            maxStudentsCount: new FormControl<number>(1, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(1), Validators.max(5), this.validatorService.wholeNumberValidator()]
            }),
            day: new FormControl<number>(1, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(1), Validators.max(7), this.validatorService.wholeNumberValidator()]
            }),
            hour: new FormControl<number>(8, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(8), Validators.max(20), this.validatorService.wholeNumberValidator()]
            }),
            minutes: new FormControl<number>(0, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(0), Validators.max(60),
                this.validatorService.stepValidator(5), this.validatorService.wholeNumberValidator()]
            }),
            price: new FormControl<number>(0, {
                nonNullable: true,
                validators: [Validators.required, Validators.min(0), Validators.max(5000),
                this.validatorService.stepValidator(5), this.validatorService.wholeNumberValidator(),
                this.validatorService.divisorValidator()]
            })
        }, {
            validators: [this.validatorService.lessonTimeConflictValidator(this.lessonId())]
        });
    }

    setLessonId(lessonId: number | null) {
        this.lessonId.set(lessonId);
    }
}