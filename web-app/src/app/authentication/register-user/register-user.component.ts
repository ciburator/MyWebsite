import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForRegistrationDto } from 'src/app/shared/dtos/userForRegistration.dto';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';
import { PasswordConfirmationValidatorService } from 'src/app/shared/services/passwordConfirmationValidator.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss'],
})
export class RegisterUserComponent implements OnInit {
  public registerForm: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirm: new FormControl(''),
  });
  public errorMessage: string = '';
  public showError: boolean | undefined;

  constructor(
    private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.registerForm
      .get('confirm')
      .setValidators([
        Validators.required,
        this.passConfValidator.validateConfirmPassword(
          this.registerForm.get('password')
        ),
      ]);
  }

  public validateControl(controlName: string): boolean {
    return (
      this.registerForm.get(controlName).invalid &&
      this.registerForm.get(controlName).touched
    );
  }

  public hasError(controlName: string, errorName: string): boolean {
    return this.registerForm.get(controlName).hasError(errorName);
  }

  public registerUser(registerFormValue: any): void {
    this.showError = false;
    const formValues = { ...registerFormValue };

    const user: UserForRegistrationDto = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm,
    };

    this.authService.registerUser('api/accounts/registration', user).subscribe({
      next: (_) => this.router.navigate(['/authentication/login']),
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
      },
    });
  }
}
