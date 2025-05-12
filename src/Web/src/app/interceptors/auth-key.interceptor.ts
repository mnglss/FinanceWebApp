import { HttpInterceptorFn } from '@angular/common/http';

export const authKeyInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};
