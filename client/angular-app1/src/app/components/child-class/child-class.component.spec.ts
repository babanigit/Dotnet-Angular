import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildClassComponent } from './child-class.component';

describe('ChildClassComponent', () => {
  let component: ChildClassComponent;
  let fixture: ComponentFixture<ChildClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChildClassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChildClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
