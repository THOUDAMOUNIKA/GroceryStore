export interface GroceryItem {
  id: number;
  name: string;
  description: string;
  price: number;
  quantity: number;
  category: string;
  imageUrl: string;
  isAvailable: boolean;
  createdAt: Date;
}