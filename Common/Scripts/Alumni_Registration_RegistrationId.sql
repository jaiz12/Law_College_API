USE [law-college]
GO
/****** Alumni Registration: server-generated RegistrationId ******/
/****** Format SGLCG/ALUM/<year>/<NN>, numbering restarts every year:
        SGLCG/ALUM/2026/01, SGLCG/ALUM/2026/02, ... SGLCG/ALUM/2027/01 ******/
/****** Safe to run more than once. Run after RegistrationScript.sql ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF COL_LENGTH('dbo.Alumni_Registrations', 'RegistrationId') IS NULL
BEGIN
    ALTER TABLE [dbo].[Alumni_Registrations] ADD [RegistrationId] [varchar](50) NULL;
END
ELSE IF COL_LENGTH('dbo.Alumni_Registrations', 'RegistrationId') < 50
BEGIN
    IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Alumni_Registrations_RegistrationId')
        DROP INDEX [UX_Alumni_Registrations_RegistrationId] ON [dbo].[Alumni_Registrations];
    ALTER TABLE [dbo].[Alumni_Registrations] ALTER COLUMN [RegistrationId] [varchar](50) NULL;
END
GO
-- Number rows registered before this change (or with an id in an older
-- format), per year in registration order, after any ids already issued.
WITH Pending AS (
    SELECT r.Id, r.RegistrationId, YEAR(r.CreatedDate) AS Yr,
           ROW_NUMBER() OVER (PARTITION BY YEAR(r.CreatedDate) ORDER BY r.Id) AS Seq
    FROM [dbo].[Alumni_Registrations] r
    WHERE r.RegistrationId IS NULL
       OR r.RegistrationId NOT LIKE 'SGLCG/ALUM/[0-9][0-9][0-9][0-9]/%'
)
UPDATE p
SET RegistrationId = CONCAT('SGLCG/ALUM/', p.Yr, '/', FORMAT(p.Seq + ISNULL((
        SELECT MAX(CAST(SUBSTRING(x.RegistrationId, 17, 10) AS INT))
        FROM [dbo].[Alumni_Registrations] x
        WHERE x.RegistrationId LIKE CONCAT('SGLCG/ALUM/', p.Yr, '/%')
    ), 0), '00'))
FROM Pending p;
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Alumni_Registrations_RegistrationId')
   AND NOT EXISTS (
        SELECT RegistrationId FROM [dbo].[Alumni_Registrations]
        WHERE RegistrationId IS NOT NULL
        GROUP BY RegistrationId HAVING COUNT(*) > 1)
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Alumni_Registrations_RegistrationId]
        ON [dbo].[Alumni_Registrations] ([RegistrationId])
        WHERE [RegistrationId] IS NOT NULL;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Alumni_Registration_Create] ******/
-- RegistrationId is generated here, not passed in.
-- returnvalue: 1 = registered, 2 = already registered (same name, email and
-- mobile number), 0 = error. On success also returns the new RegistrationId.
CREATE OR ALTER PROCEDURE [dbo].[sp_Alumni_Registration_Create]
(
    @FullName VARCHAR(200),
    @Email VARCHAR(200),
    @MobileNumber VARCHAR(10),
    @CourseProgramme VARCHAR(100),
    @BatchGraduationYear VARCHAR(20),
    @CurrentProfessionRole VARCHAR(200) = NULL,
    @CurrentOrganisationChamber VARCHAR(300) = NULL,
    @CurrentCityLocation VARCHAR(150) = NULL,
    @ConnectionPreferences TEXT = NULL,
    @LinkedInProfile VARCHAR(500) = NULL,
    @ProfilePhoto VARCHAR(500) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM dbo.Alumni_Registrations
        WHERE Email = @Email AND MobileNumber = @MobileNumber AND FullName = @FullName
    )
    BEGIN
        SELECT 2 AS returnvalue, CAST(NULL AS VARCHAR(50)) AS RegistrationId;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Prefix VARCHAR(20) = CONCAT('SGLCG/ALUM/', YEAR(GETDATE()), '/');
        DECLARE @Next INT;

        -- UPDLOCK + HOLDLOCK: two registrations at the same moment can't
        -- get the same number.
        SELECT @Next = ISNULL(MAX(CAST(SUBSTRING(RegistrationId, LEN(@Prefix) + 1, 10) AS INT)), 0) + 1
        FROM dbo.Alumni_Registrations WITH (UPDLOCK, HOLDLOCK)
        WHERE RegistrationId LIKE @Prefix + '%';

        DECLARE @RegistrationId VARCHAR(50) = CONCAT(@Prefix, FORMAT(@Next, '00'));

        INSERT INTO dbo.Alumni_Registrations
        (
            RegistrationId,
            FullName,
            Email,
            MobileNumber,
            CourseProgramme,
            BatchGraduationYear,
            CurrentProfessionRole,
            CurrentOrganisationChamber,
            CurrentCityLocation,
            ConnectionPreferences,
            LinkedInProfile,
            ProfilePhoto
        )
        VALUES
        (
            @RegistrationId,
            @FullName,
            @Email,
            @MobileNumber,
            @CourseProgramme,
            @BatchGraduationYear,
            @CurrentProfessionRole,
            @CurrentOrganisationChamber,
            @CurrentCityLocation,
            @ConnectionPreferences,
            @LinkedInProfile,
            @ProfilePhoto
        );

        COMMIT TRANSACTION;

        SELECT 1 AS returnvalue, @RegistrationId AS RegistrationId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        -- 2601/2627: unique index hit by a concurrent duplicate.
        SELECT CASE WHEN ERROR_NUMBER() IN (2601, 2627) THEN 2 ELSE 0 END AS returnvalue,
               CAST(NULL AS VARCHAR(50)) AS RegistrationId;
    END CATCH
END;
GO
