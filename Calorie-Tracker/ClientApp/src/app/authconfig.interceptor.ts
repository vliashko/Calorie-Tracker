import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';
import { AuthenticationService } from './authentication/authentication.service';

@Injectable()

export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthenticationService) { }

    // tslint:disable-next-line:typedef
    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const authToken = this.authService.getToken();
        if (authToken) {
            const cloned = req.clone({
              headers: req.headers.set('Authorization', `Bearer ${authToken}`),
            });
            return next.handle(cloned);
          } else {
            return next.handle(req);
          }
    }
}

