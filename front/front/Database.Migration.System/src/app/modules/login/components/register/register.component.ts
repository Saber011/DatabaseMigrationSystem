import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {AccountService} from "../../../../api/services/account.service";
import {throwError} from "rxjs";
import {catchError} from "rxjs/operators";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  public loginInvalid = false;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private readonly accountService: AccountService,
              private fb: FormBuilder){
    this.form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });
  }

  ngOnInit(): void {
  }

  registration() {
    const username = this.form.get('username')?.value;
    const password = this.form.get('password')?.value;
    this.accountService.apiAccountRegisterUserPost$Response({ body: { password: password, login: username}})
      .pipe(
        catchError(error => {
          if (error.status === 400) {
            const errorMessage = JSON.parse(error.error)[0].message;
            alert(errorMessage);
          }
          return throwError(error);
        })
      )
      .subscribe(response => {
        if (response.ok) {
          this.router.navigate(['login']);
        }
      });
  }
}
