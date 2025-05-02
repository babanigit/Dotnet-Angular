import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ChildClassComponent } from '../child-class/child-class.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-parent-class',
  templateUrl: './parent-class.component.html',
  styleUrl: './parent-class.component.css',
  standalone: true,
  imports: [CommonModule, FormsModule, ChildClassComponent],
})
export class ParentClassComponent {
  username: string = 'aniket';
}
