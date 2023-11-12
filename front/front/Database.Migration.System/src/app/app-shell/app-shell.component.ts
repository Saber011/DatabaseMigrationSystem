import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subject, takeUntil} from 'rxjs';
import {AuthService} from "../core";
import {UserDto} from "../api/models/user-dto";
@Component({
  selector: 'app-app-shell',
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss'],
})
export class AppShellComponent implements OnInit, OnDestroy {
  userInfo :UserDto | null | undefined;
  isDropdownOpen: boolean = false;

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
  private ngUnsubscribe$ = new Subject();

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.user$.pipe(takeUntil(this.ngUnsubscribe$))
      .subscribe((x) => {
      this.userInfo = x;
    });
  }

  ngOnDestroy() {
    this.ngUnsubscribe$.complete();
  }

  logout() {
     this.authService.logout();
  }
}
