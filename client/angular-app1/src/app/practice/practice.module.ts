import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PracticeComponent } from './practice.component';
<<<<<<< HEAD



@NgModule({
  declarations: [
    PracticeComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
=======
import { FormsModule } from '@angular/forms';
import { ParentClassComponent } from '../components/parent-class/parent-class.component';
import { ChildClassComponent } from '../components/child-class/child-class.component';
import { PostFreeApiComponent } from '../components/post-free-api/post-free-api.component';

@NgModule({
  declarations: [
    PracticeComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ParentClassComponent,
    PostFreeApiComponent

  ],
  exports:[
>>>>>>> 541749af3e9466c4efd5510e4775e7684be2876e
    PracticeComponent
  ]
})
export class PracticeModule { }
