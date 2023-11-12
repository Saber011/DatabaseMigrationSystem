import {Component, OnInit} from '@angular/core';
import {SettingsService} from "../../../../api/services/settings.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
@Component({
  selector: 'app-articles-view',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  form: FormGroup;


  constructor(private readonly settingsService: SettingsService, private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {
    this.form = this.formBuilder.group({
      sourceConnectionString: ['', Validators.required],
      sourceDatabaseType: ['', Validators.required],
      destinationConnectionString: ['', Validators.required],
      destinationDatabaseType: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.settingsService.apiSettingsGetSettingsGet()
      .subscribe(value => {
        setTimeout(() => {
          this.form.patchValue({
            sourceConnectionString: value.sourceConnectionString,
            sourceDatabaseType: Number(value.sourceDatabaseType).toString(),
            destinationConnectionString: value.destinationConnectionString,
            destinationDatabaseType: Number(value.destinationDatabaseType).toString()
          });
        });
      });


  }


  saveSettings() {
    this.settingsService.apiSettingsSetSettingsPost$Response({ body: {
      sourceConnectionString : this.form.get('sourceConnectionString')?.value,
        destinationConnectionString: this.form.get('destinationConnectionString')?.value,
        destinationDatabaseType: this.form.get('destinationDatabaseType')?.value,
        sourceDatabaseType: this.form.get('sourceDatabaseType')?.value,
    }
    }).subscribe(data => {
      this.router.navigate(['/home']);
    });
  }
}
