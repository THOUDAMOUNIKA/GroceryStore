import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from '../models/order.model';
import { GroceryItem } from '../models/grocery-item.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems = new BehaviorSubject<CartItem[]>([]);
  public cartItems$ = this.cartItems.asObservable();

  constructor() {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      this.cartItems.next(JSON.parse(savedCart));
    }
  }

  addToCart(item: GroceryItem, quantity: number = 1): void {
    const currentItems = this.cartItems.value;
    const existingItem = currentItems.find(ci => ci.groceryItem.id === item.id);

    if (existingItem) {
      existingItem.quantity += quantity;
    } else {
      currentItems.push({ groceryItem: item, quantity });
    }

    this.updateCart(currentItems);
  }

  removeFromCart(itemId: number): void {
    const currentItems = this.cartItems.value.filter(ci => ci.groceryItem.id !== itemId);
    this.updateCart(currentItems);
  }

  updateQuantity(itemId: number, quantity: number): void {
    const currentItems = this.cartItems.value;
    const item = currentItems.find(ci => ci.groceryItem.id === itemId);
    
    if (item) {
      if (quantity <= 0) {
        this.removeFromCart(itemId);
      } else {
        item.quantity = quantity;
        this.updateCart(currentItems);
      }
    }
  }

  clearCart(): void {
    this.updateCart([]);
  }

  private updateCart(items: CartItem[]): void {
    this.cartItems.next(items);
    localStorage.setItem('cart', JSON.stringify(items));
  }

  get cartItemCount(): number {
    return this.cartItems.value.reduce((total, item) => total + item.quantity, 0);
  }

  get cartTotal(): number {
    return this.cartItems.value.reduce((total, item) => 
      total + (item.groceryItem.price * item.quantity), 0);
  }
}