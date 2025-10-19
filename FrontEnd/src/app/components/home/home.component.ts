import { Component, OnInit } from '@angular/core';
import { GroceryService } from '../../services/grocery.service';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { GroceryItem } from '../../models/grocery-item.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  groceryItems: GroceryItem[] = [];
  categories: string[] = [];
  searchTerm = '';
  selectedCategory = '';
  loading = true;

  constructor(
    private groceryService: GroceryService,
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadGroceryItems();
    this.loadCategories();
  }

  loadGroceryItems(): void {
    this.loading = true;
    this.groceryService.getGroceryItems(this.searchTerm, this.selectedCategory)
      .subscribe({
        next: (items) => {
          this.groceryItems = items || [];
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading grocery items:', error);
          this.groceryItems = [];
          this.loading = false;
        }
      });
  }

  loadCategories(): void {
    this.groceryService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories || [];
      },
      error: (error) => {
        console.error('Error loading categories:', error);
        this.categories = [];
      }
    });
  }

  onSearch(): void {
    this.loadGroceryItems();
  }

  onCategoryChange(): void {
    this.loadGroceryItems();
  }

  addToCart(item: GroceryItem): void {
    if (!this.authService.isLoggedIn) {
      alert('Please login to add items to cart');
      return;
    }
    this.cartService.addToCart(item);
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn;
  }
}