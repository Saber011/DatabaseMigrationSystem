import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppPreloaderContainerComponent } from './app-preloader-container.component';

describe('AppPreloaderContainerComponent', () => {
  let component: AppPreloaderContainerComponent;
  let fixture: ComponentFixture<AppPreloaderContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppPreloaderContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppPreloaderContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
