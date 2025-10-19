export interface User {
  id: number;
  email: string;
  name: string;
  address: string;
  phone: string;
  token?: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  name: string;
  address: string;
  phone: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}