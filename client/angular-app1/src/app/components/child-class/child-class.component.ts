import { CommonModule } from '@angular/common';
import { Component, Input, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-child-class',
  templateUrl: './child-class.component.html',
  styleUrl: './child-class.component.css',
  standalone: true,
  imports: [CommonModule],
})
export class ChildClassComponent {
  @Input() name: string | undefined = ''; // This will be filled by parent

  ngOnChanges(changes: SimpleChanges) {
    console.log('Changes in child components:', changes);
  }
}
