import { CanActivateFn } from '@angular/router';

export const roleCheckGuard: CanActivateFn = (route, state) => {
  return true;
};
