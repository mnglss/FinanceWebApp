export interface User {
  name: string;
  email: string;
  password: string;
  accepted: boolean;
  role: string;
  createdAt?: string;
  updatedAt?: string;
  deletedAt?: string;
  lastLogin?: string;
}
