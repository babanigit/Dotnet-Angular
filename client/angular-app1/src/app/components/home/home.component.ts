import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone:true,
  imports:[
    CommonModule
  ]
})
export class HomeComponent {


  constructor(
    private auth: AuthService,
    private product: ProductService
  ) {}

  register() {
    this.auth.register({
      username: 'testuser',
      email: 'test@example.com',
      password: 'Test@123'
    }).subscribe({
      next: res => console.log('✅ Register Success', res),
      error: err => console.error('❌ Register Error', err)
    });
  }

  login() {
    this.auth.login({
      username: 'testuser',
      password: 'Test@123'
    }).subscribe({
      next: res => console.log('✅ Login Success', res),
      error: err => console.error('❌ Login Error', err)
    });
  }

  getProducts() {
    this.product.getProducts().subscribe({
      next: res => console.log('✅ Products:', res),
      error: err => console.error('❌ Product Error:', err)
    });
  }

}
