DELIMITER //

CREATE PROCEDURE QUERY_USERS_COUNT (OUT param1 INT)
    BEGIN
        SELECT COUNT(*) INTO param1 FROM User;
    END
//

DELIMITER ;