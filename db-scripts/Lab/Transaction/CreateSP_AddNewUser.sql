DELIMITER //
CREATE PROCEDURE AddNewProduct(IN productName NVARCHAR(40))
  BEGIN
    INSERT INTO Product (Product.Name) VALUES (productName);
  END
//
DELIMITER ;