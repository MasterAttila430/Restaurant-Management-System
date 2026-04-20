USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Ettermek')
BEGIN
    ALTER DATABASE Ettermek SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Ettermek;
END
GO

CREATE DATABASE Ettermek;
GO

USE Ettermek;
GO

DROP TABLE IF EXISTS Etelek_Rendelesek;
DROP TABLE IF EXISTS Foglalasok;
DROP TABLE IF EXISTS Rendelesek;
DROP TABLE IF EXISTS Munkasok;
DROP TABLE IF EXISTS SzamlaSor;
DROP TABLE IF EXISTS SzamlaFej;
DROP TABLE IF EXISTS Ettermek;
DROP TABLE IF EXISTS Etelek;
DROP TABLE IF EXISTS Ugyfelek;
DROP TABLE IF EXISTS Orszag;
GO

CREATE TABLE Orszag (
    OrszagID INT IDENTITY PRIMARY KEY,
    OrszagNev NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Ettermek (
    EtteremID INT IDENTITY PRIMARY KEY,
    Nev NVARCHAR(100) NOT NULL,
    Cim NVARCHAR(100) NOT NULL,
    Telefonszam VARCHAR(20) NOT NULL,
    OrszagID INT NOT NULL,
    CONSTRAINT FK_Ettermek_Orszag FOREIGN KEY (OrszagID) REFERENCES Orszag(OrszagID)
);
GO

CREATE TABLE Munkasok (
    MunkasID INT IDENTITY PRIMARY KEY,
    MunkasNev NVARCHAR(100) NOT NULL,
    Fizetes DECIMAL(18,2) CHECK (Fizetes > 0),
    Beosztas VARCHAR(50) NOT NULL,
    CNP VARCHAR(13) UNIQUE NOT NULL,
    EtteremID INT NOT NULL,
    RowVersion ROWVERSION NOT NULL,  --RowVersion III optimistahoz
    CONSTRAINT FK_Munkasok_Ettermek FOREIGN KEY (EtteremID) REFERENCES Ettermek(EtteremID)
);
GO

CREATE TABLE Ugyfelek (
    UgyfelID INT IDENTITY PRIMARY KEY,
    Nev NVARCHAR(100) NOT NULL,
    SzamlazasiCim NVARCHAR(200),
    Telefonszam VARCHAR(20),
    Email VARCHAR(100) CHECK (Email LIKE '%@%.%')
);
GO

CREATE TABLE Etelek (
    EtelID INT IDENTITY PRIMARY KEY,
    Nev NVARCHAR(100) NOT NULL,
    Ar DECIMAL(18,2) CHECK (Ar > 0),
    Kategoria VARCHAR(50) CHECK (Kategoria IN ('Leves','Főétel','Köret','Desszert','Ital')),
    MaxElkeszithetoMennyiseg INT NOT NULL DEFAULT 100,
    MarElkeszitettMennyiseg INT NOT NULL DEFAULT 0,
    CONSTRAINT CK_Etelek_Mennyiseg CHECK (MarElkeszitettMennyiseg <= MaxElkeszithetoMennyiseg)
);
GO

CREATE TABLE Rendelesek (
    RendelesID INT IDENTITY PRIMARY KEY,
    HelybenVagyElvitel VARCHAR(10) CHECK (HelybenVagyElvitel IN ('Helyben','Elvitel')),
    Osszeg DECIMAL(18,2) CHECK (Osszeg > 0),
    Statusz VARCHAR(20) DEFAULT 'Feldolgozas alatt' CHECK (Statusz IN ('Feldolgozas alatt','Kesz','Kiszallitva')),
    Datum DATETIME DEFAULT GETDATE(),
    UgyfelID INT NOT NULL,
    CONSTRAINT FK_Rendelesek_Ugyfelek FOREIGN KEY (UgyfelID) REFERENCES Ugyfelek(UgyfelID)
);
GO

CREATE TABLE Foglalasok (
    FoglalasID INT IDENTITY PRIMARY KEY,
    Idopont DATETIME NOT NULL,
    SzemelyekSzama INT CHECK (SzemelyekSzama > 0),
    UgyfelID INT NOT NULL,
    EtteremID INT NOT NULL,
    CONSTRAINT FK_Foglalasok_Ugyfelek FOREIGN KEY (UgyfelID) REFERENCES Ugyfelek(UgyfelID),
    CONSTRAINT FK_Foglalasok_Ettermek FOREIGN KEY (EtteremID) REFERENCES Ettermek(EtteremID)
);
GO

CREATE TABLE Etelek_Rendelesek (
    EtelID INT NOT NULL,
    RendelesID INT NOT NULL,
    Mennyiseg INT DEFAULT 1 CHECK (Mennyiseg > 0),
    CONSTRAINT PK_Etelek_Rendelesek PRIMARY KEY (EtelID, RendelesID),
    CONSTRAINT FK_EtelRend_Etel FOREIGN KEY (EtelID) REFERENCES Etelek(EtelID),
    CONSTRAINT FK_EtelRend_Rend FOREIGN KEY (RendelesID) REFERENCES Rendelesek(RendelesID)
);
GO

 --- II/a FELADAT: AZ UJ TABLAK LETREHOZASA -----

CREATE TABLE SzamlaFej (
    SzamlaID INT IDENTITY(1,1) PRIMARY KEY,
    Datum DATETIME NOT NULL DEFAULT GETDATE(),
    UgyfelID INT NOT NULL,
    TeljesNetto DECIMAL(18, 2) NOT NULL,
    TeljesBrutto DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_SzamlaFej_Ugyfelek FOREIGN KEY (UgyfelID) REFERENCES Ugyfelek(UgyfelID)
);
GO

CREATE TABLE SzamlaSor (
    SzamlaID INT NOT NULL,
    EtelID INT NOT NULL,
    Mennyiseg INT NOT NULL CHECK (Mennyiseg > 0),
    NettoAr DECIMAL(18, 2) NOT NULL,
    BruttoAr DECIMAL(18, 2) NOT NULL,
    CONSTRAINT PK_SzamlaSor PRIMARY KEY (SzamlaID, EtelID),
    CONSTRAINT FK_SzamlaSor_SzamlaFej FOREIGN KEY (SzamlaID) REFERENCES SzamlaFej(SzamlaID) ON DELETE CASCADE,
    CONSTRAINT FK_SzamlaSor_Etelek FOREIGN KEY (EtelID) REFERENCES Etelek(EtelID)
);
GO

-- adatok beszurasasa--

INSERT INTO Orszag (OrszagNev) VALUES
('Magyarország'),('Románia'),('Horvátország');
GO

INSERT INTO Ettermek (Nev, Cim, Telefonszam, OrszagID) VALUES
('AranyEmber', 'Budapest, Váci út 42', '+3611234567', 1),
('Hegyalja Etkezde', 'Beszterce Axente Sever 2', '+3630987654', 2),
('Tengerparti Grill', 'Balatonfüred, Fő tér 8', '+36704567890', 1),
('Adriatic Sea Food', 'Split, Riva 15', '+38512345678', 3);
GO

INSERT INTO Munkasok (MunkasNev, Fizetes, Beosztas, CNP, EtteremID) VALUES
('Kovács István', 450000.00, 'Séf', '1234567890123', 1),
('Nagy Anna', 280000.00, 'Pincér', '2345678901234', 1),
('Szabó Gábor', 320000.00, 'Vendéglős', '3456789012345', 2),
('Horváth Éva', 290000.00, 'Pincér', '4567890123456', 3),
('Tóth Zsolt', 480000.00, 'Séf', '5678901234567', 4),
('Molnár Petra', 270000.00, 'Pincér', '6789012345678', 4);
GO

INSERT INTO Ugyfelek (Nev, SzamlazasiCim, Telefonszam, Email) VALUES
('Kiss Éva', 'Budapest, Kossuth tér 5', '+36201234567', 'kiss.eva@example.com'),
('Nagy Péter', 'Beszterce Bulevard 3', '+36304567890', 'nagy.peter@example.org'),
('Varga Zoltán', 'Szeged, Fő fasor 22', '+36709876543', 'varga.zoltan@example.net');
GO

INSERT INTO Etelek (Nev, Ar, Kategoria) VALUES
('Libamáj pürével', 4500.00, 'Főétel'),
('Halászlé', 2800.00, 'Leves'),
('Tiramisu', 1800.00, 'Desszert'),
('Grillezett lazac', 5200.00, 'Főétel');
GO

UPDATE Etelek SET MaxElkeszithetoMennyiseg = 50, MarElkeszitettMennyiseg = 10 WHERE Nev = 'Libamáj pürével';
UPDATE Etelek SET MaxElkeszithetoMennyiseg = 100, MarElkeszitettMennyiseg = 30 WHERE Nev = 'Halászlé';
UPDATE Etelek SET MaxElkeszithetoMennyiseg = 70, MarElkeszitettMennyiseg = 15 WHERE Nev = 'Tiramisu';
UPDATE Etelek SET MaxElkeszithetoMennyiseg = 60, MarElkeszitettMennyiseg = 5 WHERE Nev = 'Grillezett lazac';
GO

INSERT INTO Rendelesek (HelybenVagyElvitel, Osszeg, Statusz, UgyfelID) VALUES
('Elvitel', 7300.00, 'Kesz', 1),
('Helyben', 4600.00, 'Kiszallitva', 2),
('Elvitel', 1800.00, 'Feldolgozas alatt', 3);
GO

INSERT INTO Foglalasok (Idopont, SzemelyekSzama, UgyfelID, EtteremID) VALUES
('2024-05-20 18:30:00', 4, 1, 1),
('2024-05-21 19:00:00', 2, 2, 2),
('2024-05-22 20:00:00', 6, 3, 3);
GO

INSERT INTO Etelek_Rendelesek (EtelID, RendelesID, Mennyiseg) VALUES
(1, 1, 1),
(4, 1, 2),
(2, 2, 1),
(3, 3, 1);
GO

--adatok lekerese--

SELECT * FROM Orszag;
SELECT * FROM Ettermek;
SELECT * FROM Munkasok;
SELECT * FROM Ugyfelek;
SELECT * FROM Etelek;
SELECT * FROM Rendelesek;
SELECT * FROM Foglalasok;
SELECT * FROM Etelek_Rendelesek;
SELECT * FROM SzamlaFej;
SELECT * FROM SzamlaSor;
GO
