import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ValidationError } from '../responses/validation-error.model';

@Injectable({ providedIn: 'root' })
export class ApiFormErrorService {
  apply(form: FormGroup, errors: ValidationError[]) {
    if (!Array.isArray(errors)) {
      return;
    }

    errors.forEach((e) => {
      const control = form.get(e.field);
      if (control) {
        let errorMessage = e.message;
        if (e.code === 'STRING_LENGTH') {
          const max = e.additionalData?.['max'];
          const min = e.additionalData?.['min'];
          errorMessage = `Please enter between ${min} and ${max} characters.`;
        }
        control.setErrors({ apiError: errorMessage });
      }
    });
  }
}
