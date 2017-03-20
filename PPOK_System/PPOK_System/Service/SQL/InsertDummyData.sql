INSERT INTO [dbo].[store] VALUES(
	'123 street','Edmond','OK','73013'
);
INSERT INTO [dbo].[person] VALUES 
	(1, 'Calgary','Michael','calmik@bleh.com','1234567890','01/01/1900','admin'),
	(1, 'Rob', 'Thompson', 'robthom@bleh.com', '2364819238', '09/08/1986', 'customer'),
	(1, 'Lane', 'Wheeler', 'lanwhe@bleh.com', '6283948263', '03/05/1994', 'pharm'),
	(1, 'Weston', 'Vidaurri', 'wesvin@bleh.com', '2385702834', '04/05/1996', 'admin'),
	(1, 'Weston', 'Buck', 'wesbuck@bleh.com', '7839231673', '02/23/1995', 'customer'),
	(1, 'Colby', 'Dial', 'coldia@bleh.com', '3628429834', '06/13/1997', 'pharm');

INSERT INTO [dbo].[contact_preference] VALUES
	(1, 'email', 'yes'),
	(1, 'text', 'no'),
	(2, 'text', 'no'),
	(3, 'email', 'no'),
	(4, 'email', 'yes'),
	(5, 'text', 'no'),
	(6, 'text', 'yes');

INSERT INTO [dbo].[drug] VALUES
    ('Omeprazole Cap Delayed Release 20 MG'),
	('Minocycline HCl Cap 100 MG'),
	('Olopatadine HCl Ophth Soln 0.2% (Base Equivalent)'),
	('Amlodipine Besylate Tab 5 MG'),
	('Raloxifene HCl Tab 60 MG'),
	('Rosuvastatin Calcium Tab 10 MG');

INSERT INTO [dbo].[prescription] VALUES
	(2, 1, '20170306', 30, 1),
	(2, 2, '20170308', 15, 3),
	(2, 3, '20170302', 30, 5),
	(5, 4, '20170302', 30, 5);
	



INSERT INTO [dbo].[message_history] VALUES
	(1, 'yes', '20170308', '20170309'),
	(2, 'no', '20170308', null),
	(3, 'yes', '20170308', '20170309');
