import { FormControl, FormGroup } from "@angular/forms"

interface FormModel {
    name: FormControl<string | null>
    subjectId: FormControl<number>,
    schoolYear: FormControl<number>,
}

export const form : FormGroup<FormModel> = new FormGroup<FormModel>({
    name: new FormControl<string | null>(null),
    schoolYear: new FormControl<number>(0, { nonNullable: true }),
    subjectId: new FormControl<number>(0, { nonNullable: true }),
});