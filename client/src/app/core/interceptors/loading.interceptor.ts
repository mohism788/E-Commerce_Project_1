import { C } from '@angular/cdk/keycodes';
import { HttpInterceptorFn } from '@angular/common/http';
import { delay, finalize } from 'rxjs';
import { BusyService } from '../Services/busy.service';
import { inject } from '@angular/core';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);

  busyService.busy();

  return next(req).pipe(
    delay(500),
    finalize(() => busyService.idle())
  )
};