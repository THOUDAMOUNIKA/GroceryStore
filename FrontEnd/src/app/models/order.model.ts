export interface Order {
  id: number;
  totalAmount: number;
  status: string;
  deliverySlot: Date;
  paymentMethod: string;
  paymentStatus: string;
  createdAt: Date;
  items: OrderItem[];
}

export interface OrderItem {
  id: number;
  itemName: string;
  quantity: number;
  price: number;
  total: number;
}

export interface CreateOrderRequest {
  items: { groceryItemId: number; quantity: number }[];
  deliverySlot: Date;
  paymentMethod: string;
}

import { GroceryItem } from './grocery-item.model';

export interface CartItem {
  groceryItem: GroceryItem;
  quantity: number;
}