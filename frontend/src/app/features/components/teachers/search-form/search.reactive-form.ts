import { FormControl, FormGroup } from "@angular/forms"

interface FormModel {
    name: FormControl<string | null>
    subjectId: FormControl<number | null>,
    schoolYear: FormControl<number | null>,
}

export const form : FormGroup<FormModel> = new FormGroup<FormModel>({
    name: new FormControl<string | null>(null),
    schoolYear: new FormControl<number | null>(null),
    subjectId: new FormControl<number | null>(null),
});