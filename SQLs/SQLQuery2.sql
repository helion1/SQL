SELECT DISTINCT firstname, lastname
FROM Customers cs JOIN Orders o
ON cs.id = o.CustomerId
WHERE orderdate > '2013-01-01' AND 
orderdate < '2014-07-08'
ORDER BY firstname;

SELECT firstname, lastname
FROM Customers 
WHERE id IN(
	SELECT customerid 
	FROM orders) AND upper(country) LIKE 'ARGENTINA';

DECLARE @orderid int;
SET @orderid = (SELECT max(id)+1 FROM orders);
select @orderid; 

--Promedio de gasto por cliente en la tienda:
SELECT customerid, month(orderdate), AVG(totalamount) FROM orders
WHERE customerid = 1
GROUP BY customerid, month(orderdate) ORDER BY 1;


--obtener la cantidad total de productos
SELECT COUNT(*) FROM products p;

--OBTENER LA CANTIDAD DE PRODUCTOS QUE NO SE VENDIERON EN 2014
SELECT COUNT(*) FROM products p
WHERE id NOT IN(
	SELECT ProductId 
	FROM orderitems oi INNER JOIN orders o
	ON oi.orderid = o.id
	WHERE  orderdate > '2014-01-01' AND orderdate <= '2015-01-01');

SELECT COUNT(*) 
FROM products p 
WHERE not EXISTS(
	SELECT productid FROM orderitems oi
	INNER JOIN orders o	ON oi.orderId = o.Id
	WHERE p.id = oi.productid AND YEAR(orderdate) = 2014);


	
--Obtener el nombre de los clientes que realizaron ordenes con 1 solo producto.
SELECT firstname FROM customers
WHERE id IN (
	SELECT customerid 
	FROM orders o INNER JOIN orderitems oi
	ON o.Id = oi.OrderId
	WHERE oi.Quantity = 1);

--OBTENER EL PRODUCTO QUE MAS UNIDADES VENDIO EN 2012

	SELECT top 1 productname, SUM(quantity) 
	FROM orderitems oi INNER JOIN products p
	ON oi.ProductId = p.Id
	GROUP BY productname
	ORDER BY 2 desc;
	

--Obtener el producto que mas ganancia genero entre marzo y octubre de 2013
	SELECT TOP 1 SUM(unitprice * quantity), productid 
	FROM orderitems 
	GROUP BY productid
	ORDER BY 1 desc;

--OBTENER LA CANTIDAD PROMEDIO DE ITEMS QUE COMPRA CADA CLIENTE


--ELIMINAR TODAS LAS ORDENES DE LOS CLEINTES DE ARGENTINA