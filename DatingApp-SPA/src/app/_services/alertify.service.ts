import { Injectable } from '@angular/core';
declare let alertify: any; // To avoid ts lint warning


// Like any other service, we need to add this to our app moodule
@Injectable({
  providedIn: 'root'
})

// We are creating wrappers around alertify service
export class AlertifyService {
  constructor() {}
  // okCallback: function of type any
  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, function(e) {
      if (e) {
        okCallback();
      } else {
      }
    });
  }
  success(message: string) {
    alertify.success(message);
  }
  error(message: string) {
    alertify.error(message);
  }
  message(message: string) {
    alertify.message(message);
  }
  warning(message: string) {
    alertify.warning(message);
  }
}
