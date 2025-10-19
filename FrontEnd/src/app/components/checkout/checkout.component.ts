import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { CartItem } from '../../models/order.model';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;
  cartItems: CartItem[] = [];
  deliverySlots: Date[] = [];
  total = 0;
  loading = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router
  ) {
    this.checkoutForm = this.formBuilder.group({
      deliverySlot: ['', Validators.required],
      paymentMethod: ['credit-card', Validators.required]
    });
  }

  ngOnInit(): void {
    this.cartService.cartItems$.subscribe(items => {
      this.cartItems = items;
      this.calculateTotal();
      if (items.length === 0) {
        this.router.navigate(['/cart']);
      }
    });

    this.loadDeliverySlots();
  }

  loadDeliverySlots(): void {
    this.orderService.getDeliverySlots().subscribe(slots => {
      this.deliverySlots = slots.map(slot => new Date(slot));
    });
  }

  private calculateTotal(): void {
    this.total = this.cartItems.reduce((sum, item) => 
      sum + (item.groceryItem.price * item.quantity), 0) + 5; // +5 for delivery
  }

  onSubmit(): void {
    if (this.checkoutForm.invalid || this.cartItems.length === 0) return;

    this.loading = true;
    this.error = '';

    const orderRequest = {
      items: this.cartItems.map(item => ({
        groceryItemId: item.groceryItem.id,
        quantity: item.quantity
      })),
      deliverySlot: this.checkoutForm.value.deliverySlot,
      paymentMethod: this.checkoutForm.value.paymentMethod
    };

    this.orderService.createOrder(orderRequest).subscribe({
      next: (order) => {
        this.cartService.clearCart();
        this.router.navigate(['/orders'], { 
          queryParams: { success: 'true', orderId: order.id } 
        });
      },
      error: (error) => {
        this.error = error.error?.message || 'Order creation failed';
        this.loading = false;
      }
    });
  }

  formatDate(date: Date): string {
    return date.toLocaleDateString() + ' ' + date.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'});
  }
}