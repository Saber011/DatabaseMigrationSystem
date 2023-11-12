import {Component, OnInit, OnDestroy, ChangeDetectionStrategy} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core';
import { Subscription } from 'rxjs';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {
  busy = false;
  form: FormGroup;
  public loginInvalid = false;
  errMessage: string = '';
  private subscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
  ) {
    this.form = this.fb.group({
      username: ['', Validators.email],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.subscription = this.authService.user$.subscribe((x) => {
      if (this.route.snapshot.url[0].path === 'login') {
        const accessToken = localStorage.getItem('access_token');
        const refreshToken = localStorage.getItem('refresh_token');
        if (x && accessToken && refreshToken) {
         const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '';
         this.router.navigate([returnUrl]);
        }
      }
    });
  }

  login() {
    const username = this.form.get('username')?.value;
    const password = this.form.get('password')?.value;

    const returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'home';
    this.authService.login(username, password).subscribe(data => {
      if (data) {
        this.router.navigate([returnUrl]);
      } else {
        this.errMessage = 'Пользователь не найден или неверный пароль';
      }
    });
  }

  ngOnDestroy(): void {
  }

}
