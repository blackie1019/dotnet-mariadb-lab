USE Test1;
CREATE TABLE TestTable_01(
    ProductId INT NOT NULL AUTO_INCREMENT,
    ProductName NVARCHAR(50) NOT NULL,
    ProductPrice DECIMAL(20) NOT NULL,
    PRIMARY KEY (ProductId)
)