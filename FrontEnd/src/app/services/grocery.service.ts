import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GroceryItem } from '../models/grocery-item.model';

@Injectable({
  providedIn: 'root'
})
export class GroceryService {
  private apiUrl = '/api/groceryitems';

  constructor(private http: HttpClient) { }

  getGroceryItems(search?: string, category?: string): Observable<GroceryItem[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    
    return this.http.get<GroceryItem[]>(this.apiUrl, { params });
  }

  getGroceryItem(id: number): Observable<GroceryItem> {
    return this.http.get<GroceryItem>(`${this.apiUrl}/${id}`);
  }

  getCategories(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/categories`);
  }
}