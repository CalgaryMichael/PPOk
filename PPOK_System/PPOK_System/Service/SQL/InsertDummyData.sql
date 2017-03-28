INSERT INTO [dbo].[store] VALUES
	('123 street','Edmond','OK','73013');

INSERT INTO [dbo].[person] VALUES 
	(1,'Calgary','Michael','73013','1234567890','calmik@bleh.com','01/01/1900','admin'),
	(1,'Rob','Thompson','73013','2364819238','robthom@bleh.com','09/08/1986','customer'),
	(1,'Lane','Wheeler','73013','6283948263','lanwhe@bleh.com','03/05/1994','pharm'),
	(1,'Weston','Vidaurri','73013','2385702834','wesvin@bleh.com','04/05/1996','admin'),
	(1,'Weston','Buck','73013','7839231673','wesbuck@bleh.com','02/23/1995','customer'),
	(1,'Colby','Dial','73013','3628429834','coldia@bleh.com','06/13/1997','pharm');

INSERT INTO [dbo].[contact_preference] VALUES
	(1,'email','yes'),
	(1,'text','no'),
	(2,'text','no'),
	(3,'email','no'),
	(4,'email','yes'),
	(5,'text','no'),
	(6,'text','yes');

INSERT INTO [dbo].[drug] VALUES
    ('60505006501','Omeprazole Cap Delayed Release 20 MG'),
	('591569550','Minocycline HCl Cap 100 MG'),
	('65027225','Olopatadine HCl Ophth Soln 0.2% (Base Equivalent)'),
	('68382012205','Amlodipine Besylate Tab 5 MG'),
	('2416502','Raloxifene HCl Tab 60 MG'),
	('310075190','Rosuvastatin Calcium Tab 10 MG');

INSERT INTO [dbo].[prescription] VALUES
	(2,1,'03/06/2017',30,1),
	(2,2,'03/08/2017',15,3),
	(2,3,'03/02/2017',30,5),
	(5,4,'03/02/2017',30,5);


INSERT INTO [dbo].[message_history] VALUES
	(1,'yes','20170308','20170309'),
	(2,'no','20170308',null),
	(3,'yes','20170308','20170309');
