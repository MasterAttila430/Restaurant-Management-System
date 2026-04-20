USE Ettermek;
GO
-- 1. Felhasználói csoportok (Jogosultsági szintek)
CREATE TABLE FelhasznaloCsoportok (
    CsoportID INT IDENTITY(1,1) PRIMARY KEY,
    Megnevezes NVARCHAR(50) NOT NULL UNIQUE -- pl. 'Admin', 'Guest', 'User'
);
GO

-- Alapértelmezett csoportok beszúrása
INSERT INTO FelhasznaloCsoportok (Megnevezes) VALUES ('Admin'), ('User'), ('Guest');
GO

-- 2. Felhasználók tábla (Jelszó hash + Salt tárolással)
CREATE TABLE Felhasznalok (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FelhasznaloNev NVARCHAR(50) NOT NULL UNIQUE,
    JelszoHash NVARCHAR(255) NOT NULL, -- PBKDF2 hash hossza miatt
    JelszoSalt NVARCHAR(255) NOT NULL, -- Egyedi só minden userhez
    CsoportID INT NOT NULL,
    CONSTRAINT FK_Felhasznalok_Csoportok FOREIGN KEY (CsoportID) REFERENCES FelhasznaloCsoportok(CsoportID)
);
GO
-- Ha már létezik, eldobjuk, hogy újra létrehozhassuk
DROP PROCEDURE IF EXISTS sp_User_Register;
GO

CREATE PROCEDURE sp_User_Register
    @Nev NVARCHAR(50),
    @Hash NVARCHAR(255),
    @Salt NVARCHAR(255),
    @CsoportID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
            -- Ellenőrzés: létezik-e már a név?
            IF EXISTS (SELECT 1 FROM Felhasznalok WHERE FelhasznaloNev = @Nev)
            BEGIN
                -- THROW helyett RAISERROR (16-os súlyosság = hiba)
                RAISERROR ('Ez a felhasználónév már foglalt!', 16, 1);
                ROLLBACK TRANSACTION; -- Fontos: kézzel visszavonjuk
                RETURN; -- Kilépünk
            END

            INSERT INTO Felhasznalok (FelhasznaloNev, JelszoHash, JelszoSalt, CsoportID)
            VALUES (@Nev, @Hash, @Salt, @CsoportID);
            
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Hiba esetén visszavonjuk, ha van nyitott tranzakció
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        -- Hiba továbbdobása RAISERROR-ral
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO