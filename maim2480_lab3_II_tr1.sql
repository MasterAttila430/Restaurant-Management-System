USE Ettermek;
GO

CREATE TRIGGER trg_Ettermek_Delete
ON Ettermek
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    --foglalasokbol torles
    DELETE FROM Foglalasok
    WHERE EtteremID IN (SELECT EtteremID FROM deleted);
    
	--munkasokbol
    DELETE FROM Munkasok
    WHERE EtteremID IN (SELECT EtteremID FROM deleted);
    
    --vegul az egeszet 
    DELETE FROM Ettermek
    WHERE EtteremID IN (SELECT EtteremID FROM deleted);
    
    PRINT 'Étterem és kapcsolódó adatok (foglalások, munkások) sikeresen törölve.';
END
GO
