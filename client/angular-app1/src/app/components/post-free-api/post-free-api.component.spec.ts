import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostFreeApiComponent } from './post-free-api.component';

describe('PostFreeApiComponent', () => {
  let component: PostFreeApiComponent;
  let fixture: ComponentFixture<PostFreeApiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PostFreeApiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostFreeApiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
