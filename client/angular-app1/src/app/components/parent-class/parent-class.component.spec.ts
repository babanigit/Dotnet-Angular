import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentClassComponent } from './parent-class.component';

describe('ParentClassComponent', () => {
  let component: ParentClassComponent;
  let fixture: ComponentFixture<ParentClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ParentClassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
