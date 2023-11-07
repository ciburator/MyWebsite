import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserForAuthenticationDto } from '../../shared/dtos/userForAuthentication.dto';
import { AuthResponseDto } from '../../shared/dtos/authResponse.dto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  private returnUrl: string = '';

  public loginForm: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  public errorMessage: string = '';
  public showError: boolean = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public validateControl(controlName: string) {
    return (
      this.loginForm.get(controlName).invalid &&
      this.loginForm.get(controlName).touched
    );
  }

  public hasError(controlName: string, errorName: string) {
    return this.loginForm.get(controlName).hasError(errorName);
  }

  public loginUser(loginFormValue: any) {
    this.showError = false;
    const login = { ...loginFormValue };
    const userForAuth: UserForAuthenticationDto = {
      email: login.username,
      password: login.password,
    };
    this.authService.loginUser('api/accounts/login', userForAuth).subscribe({
      next: (res: AuthResponseDto) => {
        localStorage.setItem('token', res.token);
        this.router.navigate([this.returnUrl]);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
      },
    });
  }
}
