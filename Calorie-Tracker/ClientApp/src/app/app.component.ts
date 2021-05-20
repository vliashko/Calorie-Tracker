import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './authentication/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(public authService: AuthenticationService,
              private router: Router) {}

  logout() {
    this.authService.doLogout();
    this.router.navigateByUrl('/');
  }
}

