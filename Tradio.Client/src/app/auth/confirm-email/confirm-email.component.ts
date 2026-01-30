import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "../../core/services/auth.service";

@Component({
    selector: 'confirm-email',
    templateUrl: './confirm-email.component.html',
    styleUrl: './confirm-email.component.css',
    host: {
      class: 'flex'
    },
    imports: [
        CommonModule,
        ReactiveFormsModule
    ]
})

export class ConfirmEmailComponent {
  confirmEmailForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.confirmEmailForm = this.fb.group({
      code: ['', Validators.required],
    });
  }

  get f() {
    return this.confirmEmailForm.controls;
  }

  onSubmit() {
    if (this.confirmEmailForm.invalid) return;
    const userId = this.activatedRoute.snapshot.queryParams['user-id'];
    const { code } = this.confirmEmailForm.value;
    this.authService.confirmEmail(code, userId).subscribe({
      next: (response) => {
        this.authService.saveJwtToken(response.jwtToken);
        this.router.navigate(['/services']);
      },
      error: err => this.errorMessage = err.error.message || 'Email confirmation error'
    });
  }
}