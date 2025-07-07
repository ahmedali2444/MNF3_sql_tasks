
USE StoreDB
GO
-- 1
SELECT * FROM production.products where list_price > 1000
-- 2
SELECT * FROM sales.customers WHERE state IN ('CA' , 'NY')
-- 3
select * FROM sales.orders where order_date BETWEEN '2022-12-31' AND '2024-1-1'
-- 4
SELECT * FROM sales.customers WHERE email LIKE '%@gmail.com'
-- 5
SELECt * FROM sales.staffs WHERE active = 0
-- 6
SELECT TOP 5 * FROM production.products ORDER BY list_price desc
-- 7
SELECT TOP 10 * FROM sales.orders ORDER BY order_date desc
-- 8
SELECT TOP 3 * FROM sales.customers ORDER BY last_name
-- 9
SELECT * FROM sales.customers WHERE phone IS NULL
-- 10
SELECt * FROM sales.staffs WHERE manager_id IS NOT NULL
-- 11
SELECT category_id , COUNt(*) as product_quantity
FROM production.products
GROUP BY category_id
ORDER BY category_id
-- 12
SELECT state , COUNt(customer_id) as NumberOfCustomers
FROM sales.customers
GROUP BY state
-- 13
SELECT brand_id , AVG(list_price) as average_price 
FROM production.products
GROUP BY brand_id
ORDER BY brand_id
-- 14
SELECT staff_id , COUNT(order_id) as NumberOfOrders
FROM sales.orders
GROUP BY staff_id
ORDER BY staff_id
-- 15
SELECT customer_id , COUNT(order_id) as NumberOfOrders
FROM sales.orders 
GROUP BY customer_id HAVING COUNT(order_id) > 2
ORDER BY customer_id
-- 16
SELECT product_name , list_price
FROM production.products
where list_price between 500 AND 1500
ORDER BY list_price
-- 17
SELECT customer_id , first_name , last_name , city FROM sales.customers
WHERE city LIKE 'S%'
-- 18
SELECT * FROM sales.orders WHERE order_status IN (2, 4)
-- 19 
SELECT * FROM production.products WHERE category_id IN (1, 2, 3)
-- 20
SELECT * FROM sales.staffs WHERE (store_id = 1 OR phone IS NULL)
