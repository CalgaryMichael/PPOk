CREATE TABLE [dbo].[store] (
	[store_id]			int				IDENTITY								NOT NULL,
	[address]			varchar(100)													,
	[city]				varchar(30)														,
	[state]				varchar(20)														,
	[zip]				varchar(5)														,
	PRIMARY KEY([store_id])
);


CREATE TABLE [dbo].[user] (
	[user_id]			int				IDENTITY								NOT NULL,
	[store_id]			int				REFERENCES [dbo].[store]([store_id])			,
	[first_name]		varchar(15)														,
	[last_name]			varchar(20)														,
	[email]				varchar(30)														,
	[phone]				varchar(10)												NOT NULL,
	[date_of_birth]		date															,
	[user_type]			varchar(10)												NOT NUll,
	PRIMARY KEY([user_id])
);


CREATE TABLE [dbo].[contact_preference] (
	[preference_id]		int				IDENTITY								NOT NULL,
	[user_id]			int				REFERENCES [dbo].[user]([user_id])				,
	[contact_type]		varchar(20)												NOT NULL,
	[preference]		varchar(10)												NOT NULL,
	PRIMARY KEY([preference_id])
);


CREATE TABLE [dbo].[drug] (
	[drug_id]			int				IDENTITY								NOT NULL,
	[name]				varchar(100)											NOT NULL,
	PRIMARY KEY([drug_id])
);


CREATE TABLE [dbo].[prescription] (
	[rx_id]				int				IDENTITY								NOT NULL,
	[user_id]			int				REFERENCES [dbo].[user]([user_id])				,
	[drug_id]			int				REFERENCES [dbo].[drug]([drug_id])				,
	[date_filled]		datetime														,
	[day_supplied]		int																,
	[number_refills]	int																,
	PRIMARY KEY([rx_id])
);


CREATE TABLE [dbo].[message_history] (
	[message_id]		int				IDENTITY								NOT NULL,
	[rx_id]				int				REFERENCES [dbo].[prescription]([rx_id])		,
	[response]			varchar(100)													,
	[fill_time]			datetime														,
	[pick_up_time]		datetime														,
	PRIMARY KEY([message_id])
);


CREATE TABLE [dbo].[scheduler] (
	[task_id]			int				IDENTITY								NOT NULL,
	[rx_id]				int				REFERENCES [dbo].[prescription]([rx_id])		,
	[response]			varchar(100)													,
	[day_to_send]		datetime														,
	PRIMARY KEY([task_id])
);
