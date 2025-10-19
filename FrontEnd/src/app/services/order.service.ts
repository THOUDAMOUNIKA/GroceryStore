import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, CreateOrderRequest } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = '/api/orders';

  constructor(private http: HttpClient) { }

  createOrder(request: CreateOrderRequest): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, request);
  }

  getUserOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  getDeliverySlots(): Observable<Date[]> {
    return this.http.get<Date[]>(`${this.apiUrl}/delivery-slots`);
  }
}