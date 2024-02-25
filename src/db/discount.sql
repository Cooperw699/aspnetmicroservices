CREATE TABLE Coupon (
	ID SERIAL PRIMARY KEY NOT NULL,
	ProductName VARCHAR(30) NOT NULL,
	Description TEXT,
	AMOUNT INT
);


INSERT INTO Coupon (ProductName, Description, amount)
VALUES ('IPhone X','IPhone Discount',150);

INSERT INTO Coupon (ProductName, Description, amount)
VALUES ('Samsung 10','Samsung Discount',100);