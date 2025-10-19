import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User, LoginRequest, RegisterRequest } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    const storedUser = localStorage.getItem('currentUser');
    if (storedUser) {
      this.currentUserSubject.next(JSON.parse(storedUser));
    }
  }

  register(request: RegisterRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, request)
      .pipe(tap(user => this.setCurrentUser(user)));
  }

  login(request: LoginRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/login`, request)
      .pipe(tap(user => this.setCurrentUser(user)));
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  private setCurrentUser(user: User): void {
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  get isLoggedIn(): boolean {
    return !!this.currentUserValue;
  }

  getToken(): string | null {
    return this.currentUserValue?.token || null;
  }
}