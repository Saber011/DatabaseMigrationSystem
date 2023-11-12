import {Injectable, OnDestroy, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {BehaviorSubject, Observable, of, Subject, Subscription} from 'rxjs';
import { map, tap, delay, finalize } from 'rxjs/operators';
import {UserDto} from "../../api/models/user-dto";
import {AuthenticateInfo} from "../../api/models/authenticate-info";
import {AccountService} from "../../api/services/account.service";


@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnDestroy{
  private timer: Subscription | undefined;
  private _user = new BehaviorSubject<UserDto | null >(null);
  user$: Observable<UserDto | null> = this._user.asObservable();

  private storageEventListener(event: StorageEvent) {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.stopTokenTimer();
        this._user.next(null);
      }
      if (event.key === 'login-event') {
        this.stopTokenTimer();
        this.accountService.apiAccountRefreshTokenPost()
          .subscribe(data => {
            this._user.next({
            });
            this.startTokenTimer();
            return data;
          });
      }
    }
  }

  constructor(private router: Router, private readonly accountService: AccountService ) {
    window.addEventListener('storage', this.storageEventListener.bind(this));
  }

  ngOnDestroy(): void {
    window.removeEventListener('storage', this.storageEventListener.bind(this));
  }

  login(username: string, password: string) : Observable<boolean> {
    var subject = new Subject<boolean>();
      this.accountService.apiAccountAuthenticatePost({body: {password: password, login: username }})
      .subscribe(data => {
        this._user.next({
          login: data?.login,
          id: data?.id,
        });

        if (data) {
          this.setLocalStorage(data);
          this.startTokenTimer();

        }
        subject.next(data != null);
      });
      return subject.asObservable();
  }

  logout() {
    const refreshToken = localStorage.getItem('refreshToken');
    this.accountService.apiAccountRevokeTokenPost({body: { token: refreshToken}})
      .subscribe(response => {
        this.clearLocalStorage();
        this._user.next(null);
        this.stopTokenTimer();
        this.router.navigate(['login']);
      })
  }

  refreshToken() : Observable<any> {
    const refreshToken = localStorage.getItem('access_token');
    if (!refreshToken) {
      this.clearLocalStorage();
      return of(null);
    }
  return this.accountService.apiAccountRefreshTokenPost()
      .pipe(
        map(data => {
          this._user.next({
            login: data?.login,
            id: data?.id,
          });
          if(data !== null && data) {
            this.setLocalStorage(data);
            this.startTokenTimer();
            return data;
          }
          return data;
        })
      );
  }

  setLocalStorage(x: AuthenticateInfo) {
    localStorage.setItem('access_token', x?.jwtToken ?? "");
    localStorage.setItem('refresh-token', x?.refreshToken ?? "");
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  clearLocalStorage() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.setItem('logout-event', 'logout' + Math.random());
  }

  private getTokenRemainingTime() {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      return 0;
    }
    const jwtToken = JSON.parse(atob(accessToken.split('.')[1]));
    const expires = new Date(jwtToken.exp * 1000);
    console.log(expires);
    console.log("expires");
    return expires.getTime() - Date.now();
  }

  private startTokenTimer() {
    const timeout = this.getTokenRemainingTime();
    this.timer = of(true)
      .pipe(
        delay(timeout),
        tap(() => this.refreshToken().subscribe())
      )
      .subscribe();
  }

  private stopTokenTimer() {
    this.timer?.unsubscribe();
  }

}
