-- Insert additional grocery items
INSERT INTO GroceryItems (Name, Description, Price, Quantity, Category, ImageUrl, IsAvailable, CreatedAt) VALUES
-- Fruits
('Oranges', 'Juicy navel oranges', 3.49, 80, 'Fruits', 'https://images.unsplash.com/photo-1580052614034-c55d20bfee3b?w=300', 1, GETDATE()),
('Grapes', 'Sweet green grapes', 4.99, 60, 'Fruits', 'https://images.unsplash.com/photo-1537640538966-79f369143f8f?w=300', 1, GETDATE()),
('Strawberries', 'Fresh strawberries', 5.99, 40, 'Fruits', 'https://images.unsplash.com/photo-1464965911861-746a04b4bca6?w=300', 1, GETDATE()),

-- Dairy
('Cheese', 'Cheddar cheese block', 6.99, 45, 'Dairy', 'https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=300', 1, GETDATE()),
('Yogurt', 'Greek vanilla yogurt', 1.99, 70, 'Dairy', 'https://images.unsplash.com/photo-1488477181946-6428a0291777?w=300', 1, GETDATE()),
('Butter', 'Unsalted butter', 4.49, 35, 'Dairy', 'https://images.unsplash.com/photo-1589985270826-4b7bb135bc9d?w=300', 1, GETDATE()),

-- Bakery
('Croissants', 'Buttery croissants', 3.99, 25, 'Bakery', 'https://images.unsplash.com/photo-1549903072-7e6e0bedb7fb?w=300', 1, GETDATE()),
('Bagels', 'Everything bagels', 4.49, 30, 'Bakery', 'https://images.unsplash.com/photo-1551024506-0bccd828d307?w=300', 1, GETDATE()),
('Muffins', 'Blueberry muffins', 5.99, 20, 'Bakery', 'https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=300', 1, GETDATE()),
('Donuts', 'Glazed donuts', 6.99, 18, 'Bakery', 'https://images.unsplash.com/photo-1551024601-bec78aea704b?w=300', 1, GETDATE()),

-- Meat
('Ground Beef', 'Lean ground beef', 7.99, 25, 'Meat', 'https://images.unsplash.com/photo-1603048297172-c92544798d5a?w=300', 1, GETDATE()),
('Salmon', 'Fresh Atlantic salmon', 12.99, 15, 'Meat', 'https://images.unsplash.com/photo-1544943910-4c1dc44aab44?w=300', 1, GETDATE()),
('Pork Chops', 'Bone-in pork chops', 9.99, 20, 'Meat', 'https://images.unsplash.com/photo-1602470520998-f4a52199a3d6?w=300', 1, GETDATE()),

-- Grains
('Pasta', 'Spaghetti pasta', 2.99, 85, 'Grains', 'https://images.unsplash.com/photo-1551892374-ecf8754cf8b0?w=300', 1, GETDATE()),
('Quinoa', 'Organic quinoa', 7.99, 40, 'Grains', 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=300', 1, GETDATE()),
('Oats', 'Rolled oats', 3.99, 55, 'Grains', 'https://images.unsplash.com/photo-1559181567-c3190ca9959b?w=300', 1, GETDATE()),

-- Vegetables
('Carrots', 'Fresh carrots', 2.49, 75, 'Vegetables', 'https://images.unsplash.com/photo-1598170845058-32b9d6a5da37?w=300', 1, GETDATE()),
('Broccoli', 'Fresh broccoli', 3.49, 50, 'Vegetables', 'https://images.unsplash.com/photo-1459411621453-7b03977f4bfc?w=300', 1, GETDATE()),
('Spinach', 'Baby spinach leaves', 2.99, 65, 'Vegetables', 'https://images.unsplash.com/photo-1576045057995-568f588f82fb?w=300', 1, GETDATE()),
('Bell Peppers', 'Mixed bell peppers', 4.99, 45, 'Vegetables', 'https://images.unsplash.com/photo-1563565375-f3fdfdbefa83?w=300', 1, GETDATE());

-- Update existing items with correct images
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=300' WHERE Name = 'Tomatoes';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1580052614034-c55d20bfee3b?w=300' WHERE Name = 'Oranges';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1488477181946-6428a0291777?w=300' WHERE Name = 'Yogurt';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1549903072-7e6e0bedb7fb?w=300' WHERE Name = 'Croissants';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1603048297172-c92544798d5a?w=300' WHERE Name = 'Ground Beef';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1578662996442-48f60103fc96?w=300' WHERE Name = 'Quinoa';
UPDATE GroceryItems SET ImageUrl = 'https://images.unsplash.com/photo-1559181567-c3190ca9959b?w=300' WHERE Name = 'Oats';