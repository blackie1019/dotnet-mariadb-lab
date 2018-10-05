USE Test1;
    DELETE FROM TestTable_01 WHERE ProductName IN ('測試商品1','測試商品2','測試商品3') ;
    DELETE FROM TestTable_01 WHERE ProductName = '測試商品4'