import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = '/api/account';

  constructor(private http: HttpClient) {}

  register(data: { username: string; email: string; password: string }) {
    return this.http.post(`${this.baseUrl}/register`, data);
  }

  login(data: { username: string; password: string }) {
    return this.http.post(`${this.baseUrl}/login`, data);
  }
}
