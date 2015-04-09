USE [DevNet]
GO

/****** Object:  Table [dbo].[States]    Script Date: 4/5/2015 6:02:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[States](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](max) NULL,
	[StateAbbreviation] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.States] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

-- --------------------------------------------------------------------------------
-- Add sample data
-- --------------------------------------------------------------------------------
USE [DevNet]
GO

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
