USE [DevNet]
GO

-- --------------------------------------------------------------------------------
-- Add Data
-- --------------------------------------------------------------------------------

-- States
Set Identity_insert States On
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 1, 'Alabama', 'AL' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 2, 'Alaska', 'AK' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 3, 'Arizona', 'AZ' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 4, 'Arkansas', 'AR' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 5, 'California', 'CA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 6, 'Colorado', 'CO' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 7, 'Connecticut', 'CT' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 8, 'Delaware', 'DE' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 9, 'Florida', 'FL' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 10, 'Georgia', 'GA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 11, 'Hawaii', 'HI' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 12, 'Idaho', 'ID' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 13, 'Illinois', 'IL' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 14, 'Indiana', 'IN' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 15, 'Iowa', 'IA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 16, 'Kansas', 'KS' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 17, 'Kentucky', 'KY' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 18, 'Louisiana', 'LA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 19, 'Maine', 'ME' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 20, 'Maryland', 'MD' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 21, 'Massachusetts', 'MA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 22, 'Michigan', 'MI' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 23, 'Minnesota', 'MN' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 24, 'Mississippi', 'MS' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 25, 'Missouri', 'MO' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 26, 'Montana', 'MT' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 27, 'Nebraska', 'NE' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 28, 'Nevada', 'NV' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 29, 'New Hampshire', 'NH' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 30, 'New Jersey', 'NJ' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 31, 'New Mexico', 'NM' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 32, 'New York', 'NY' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 33, 'North Carolina', 'NC' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 34, 'North Dakota', 'ND' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 35, 'Ohio', 'OH' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 36, 'Oklahoma', 'OK' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 37, 'Oregon', 'OR' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 38, 'Pennsylvania', 'PA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 39, 'Rhode Island', 'RI' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 40, 'South Carolina', 'SC' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 41, 'South Dakota', 'SD' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 42, 'Tennessee', 'TN' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 43, 'Texas', 'TX' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 44, 'Utah', 'UT' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 45, 'Vermont', 'VT' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 46, 'Virginia', 'VA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 47, 'Washington', 'WA' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 48, 'West Virginia', 'WV' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 49, 'Wisconsin', 'WI' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 50, 'Wyoming', 'WY' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 51, 'American Samoa', 'AS' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 52, 'District of Columbia', 'DC' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 53, 'Federated States of Micronesia', 'FM' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 54, 'Guam', 'GU' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 55, 'Marshall Islands', 'MH' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 56, 'Northern Mariana Islands', 'MP' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 57, 'Palau', 'PW' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 58, 'Puerto Rico', 'PR' )	
INSERT INTO States ( StateID, StateName, StateAbbreviation ) VALUES ( 59, 'Virgin Islands', 'VI' )	
Set Identity_insert States Off

-- FavoriteIDEs
Set Identity_insert FavoriteIDEs On
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 1, 'Eclipse' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 2, 'Visual Studio' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 3, 'Vim' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 4, 'Aptana' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 5, 'Qt Creator' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 6, 'MonoDevelop' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 7, 'IntelliJ' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 8, 'Xcode' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 9, 'NetBeans' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 10, 'Xamarin' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 11, 'Unity' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 12, 'Eclipse PTP' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 13, 'Sublime Text 2' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 14, 'FP Haskell Center' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 15, 'Emacs' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 16, 'Intel Parallel Studio' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 17, 'Nide' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 18, 'PhpStorm' )	
INSERT INTO FavoriteIDEs ( FavoriteIDEID, FavoriteIDEName ) VALUES ( 19, 'InetlliJ' )	
Set Identity_insert FavoriteIDEs Off

-- ProgrammingLanguages
Set Identity_insert ProgrammingLanguages On
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 1, 'Java' )
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 2, 'C/C++' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 3, 'C#' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 4, 'Ruby' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 5, 'VB.NET' )		
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 6, 'Haskell' )
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 7, 'PHP' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 8, 'Clojure' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 9, 'Objective-C' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 10, 'Go' )		
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 11, 'Fortran' )
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 12, 'JavaScript' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 13, 'Lua' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 14, 'Scheme' )	
INSERT INTO ProgrammingLanguages ( ProgrammingLanguageID, ProgrammingLanguageName ) VALUES ( 15, 'F#' )		
Set Identity_insert ProgrammingLanguages Off

-- SoftwareSpecialties
Set Identity_insert SoftwareSpecialties On
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 1, 'Web Development' )
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 2, 'Desktop Development' )
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 3, 'Game Development' )
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 4, 'Mobile Development' )
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 5, 'Parallel Programming' )
INSERT INTO SoftwareSpecialties ( SoftwareSpecialtyID, SoftwareSpecialtyName ) VALUES ( 6, 'Embedded Devices' )
Set Identity_insert SoftwareSpecialties Off

-- User
INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[Address]
           ,[City]
           ,[StateID]
           ,[DateOfBirth]
           ,[FavoriteIDEID]
           ,[SoftwareSpecialtyID]
           ,[ProgrammingLanguageID]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[UserName])
     VALUES
           (1
           ,'Admin'
           ,''
           ,'123 SomeWhere'
           ,'SomeCity'
           ,1
           ,'01/01/2015'
           ,1
           ,1
           ,1
           ,'admin@example.com'
           ,1
           ,null
           ,null
           ,null
           ,0
           ,0
           ,null
           ,0
           ,0
           ,'admin@example.com')


-- Role
INSERT INTO
  [AspNetRoles] ([Id],[Name]) VALUES (1,'Admin')


-- Assign Admin to User
INSERT INTO [AspNetUserRoles] ([UserId], [RoleId]) VALUES ('efd336e1-495f-408a-a517-1800336220c1',1)


GO

SELECT * FROM States
SELECT * FROM FavoriteIDEs
SELECT * FROM ProgrammingLanguages
SELECT * FROM SoftwareSpecialties

SELECT * FROM [dbo].[AspNetUsers]
SELECT * FROM [dbo].[AspNetRoles]
SELECT * FROM [dbo].[AspNetUserRoles]
