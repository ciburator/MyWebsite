import { Injectable } from '@angular/core';
import { UserForAuthenticationDto } from '../dtos/userForAuthentication.dto';
import { AuthResponseDto } from '../dtos/authResponse.dto';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { UserForRegistrationDto } from '../dtos/userForRegistration.dto';
import { RegistrationResponseDto } from '../dtos/registrationResponse.dto';
import { EnvironmentUrlService } from './environmentUrl.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  constructor(
    private http: HttpClient,
    private envUrl: EnvironmentUrlService
  ) {}

  public registerUser(route: string, body: UserForRegistrationDto) {
    return this.http.post<RegistrationResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  };

  public loginUser(route: string, body: UserForAuthenticationDto) {
    return this.http.post<AuthResponseDto>(
      this.createCompleteRoute(route, this.envUrl.urlAddress),
      body
    );
  }

  public logout() {
    localStorage.removeItem('token');
    this.sendAuthStateChangeNotification(false);
  }

  public sendAuthStateChangeNotification(isAuthenticated: boolean) {
    this.authChangeSub.next(isAuthenticated);
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }
}
