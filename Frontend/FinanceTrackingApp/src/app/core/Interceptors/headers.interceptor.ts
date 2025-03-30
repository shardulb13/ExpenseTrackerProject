import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { TokenService } from '../services/token.service';
import { LoaderService } from '../services/loader.service';

@Injectable()
export class HeadersInterceptor implements HttpInterceptor {

  constructor(private tokenservice: TokenService , private loaderService: LoaderService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loaderService.isLoading.next(true);
    const token = this.tokenservice.getToken();
    if(token && token !== '')
    {
      const tokenreq = request.clone({
        headers:request.headers.set('Authorization', 'Bearer ' + token),
      });
      return next.handle(tokenreq).pipe(
        finalize(
          ()=>{
            this.loaderService.isLoading.next(false);
          }
        )
      )
    }
    return next.handle(request).pipe(
      finalize(
        ()=>{
          this.loaderService.isLoading.next(false);
        }
      )
    );
  }
}
