import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PracticeComponent } from './practice.component';
import { FormsModule } from '@angular/forms';
import { ParentClassComponent } from '../components/parent-class/parent-class.component';
import { ChildClassComponent } from '../components/child-class/child-class.component';

@NgModule({
  declarations: [
    PracticeComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ParentClassComponent,

  ],
  exports:[
    PracticeComponent
  ]
})
export class PracticeModule { }
