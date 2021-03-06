USE [OvertimeManager]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyKeyId] [uniqueidentifier] NOT NULL,
	[CompanyName] [varchar](255) NOT NULL,
	[StateCode] [varchar](2) NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeKeyId] [uniqueidentifier] NOT NULL,
	[LastName] [varchar](75) NOT NULL,
	[FirstName] [varchar](75) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeHour]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeHour](
	[EmployeeHourKeyId] [uniqueidentifier] NOT NULL,
	[EmployeeKeyId] [uniqueidentifier] NOT NULL,
	[CompanyKeyId] [uniqueidentifier] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[HourlyWage] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_EmployeeHour] PRIMARY KEY CLUSTERED 
(
	[EmployeeHourKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateCode]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateCode](
	[StateKeyId] [uniqueidentifier] NOT NULL,
	[StateCode] [varchar](2) NOT NULL,
	[StateName] [varchar](75) NOT NULL,
 CONSTRAINT [PK_StateCode] PRIMARY KEY CLUSTERED 
(
	[StateCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateOvertimeRule]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateOvertimeRule](
	[StateOvertimeRuleKeyId] [uniqueidentifier] NOT NULL,
	[StateCode] [varchar](2) NOT NULL,
	[RuleName] [varchar](500) NOT NULL,
	[RuleTypeName] [varchar](255) NOT NULL,
	[RuleEqualToGreaterThreshold] [int] NOT NULL,
	[OvertimeRate] [decimal](10, 2) NOT NULL,
	[HourlyWageToUse] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_StateOvertimeRule] PRIMARY KEY CLUSTERED 
(
	[StateOvertimeRuleKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StateOvertimeRuleType]    Script Date: 6/15/2021 10:29:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateOvertimeRuleType](
	[StateOvertimeRuleTypeKeyId] [uniqueidentifier] NOT NULL,
	[RuleTypeName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_StateOvertimeRuleType] PRIMARY KEY CLUSTERED 
(
	[RuleTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_StateCode] FOREIGN KEY([StateCode])
REFERENCES [dbo].[StateCode] ([StateCode])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_StateCode]
GO
ALTER TABLE [dbo].[EmployeeHour]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeHour_Company] FOREIGN KEY([CompanyKeyId])
REFERENCES [dbo].[Company] ([CompanyKeyId])
GO
ALTER TABLE [dbo].[EmployeeHour] CHECK CONSTRAINT [FK_EmployeeHour_Company]
GO
ALTER TABLE [dbo].[EmployeeHour]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeHour_Employee] FOREIGN KEY([EmployeeKeyId])
REFERENCES [dbo].[Employee] ([EmployeeKeyId])
GO
ALTER TABLE [dbo].[EmployeeHour] CHECK CONSTRAINT [FK_EmployeeHour_Employee]
GO
ALTER TABLE [dbo].[StateOvertimeRule]  WITH CHECK ADD  CONSTRAINT [FK_StateOvertimeRule_StateCode] FOREIGN KEY([StateCode])
REFERENCES [dbo].[StateCode] ([StateCode])
GO
ALTER TABLE [dbo].[StateOvertimeRule] CHECK CONSTRAINT [FK_StateOvertimeRule_StateCode]
GO
ALTER TABLE [dbo].[StateOvertimeRule]  WITH CHECK ADD  CONSTRAINT [FK_StateOvertimeRule_StateOvertimeRuleType] FOREIGN KEY([RuleTypeName])
REFERENCES [dbo].[StateOvertimeRuleType] ([RuleTypeName])
GO
ALTER TABLE [dbo].[StateOvertimeRule] CHECK CONSTRAINT [FK_StateOvertimeRule_StateOvertimeRuleType]
GO
