use diagnostico_de_saude;

insert into enfermeiro values(md5('pedro'), 'Pedro Braga', '1999-11-25', 'M', 'pedro', md5('pedro123'), true, curdate(), null);

insert into paciente values(md5(concat('Rodolfo', '2000-10-10')), 'Rodolfo', '2000-10-10', 'M', md5('pedro'), '2018-11-01', null);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-08-13 12:59:00')), md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-08-13 12:59:00', 80, 76, 1.77, true, 60, '120/80', 10, 38.5, 100);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-10-29 11:37:00')), md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-10-29 11:37:00', 90, 79, 1.77, false, 80, '130/90', 15, 36.5, 118);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-11-20 10:07:00')), md5('pedro'), md5(concat('Rodolfo', '2000-10-10')), '2018-11-20 10:07:00', 100, 89, 1.76, false, 120, '135/92', 12, 36.5, 126);

insert into paciente values(md5(concat('Rodolfa', '2010-08-09')), 'Rodolfa', '2010-08-09', 'F', md5('pedro'), '2017-12-03', null);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-08-13 12:59:00')), md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-08-13 12:59:00', 50, 50, 1.60, true, 63, '120/80', 12, 36.8, 89);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-10-29 11:37:00')), md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-10-29 11:37:00', 48, 48, 1.60, true, 60, '120/80', 10, 36.5, 83);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-11-20 10:07:00')), md5('pedro'), md5(concat('Rodolfa', '2010-08-09')), '2018-11-20 10:07:00', 45, 47, 1.58, false, 120, '120/80', 9, 36, 80);

insert into paciente values(md5(concat('Ronald', '2007-07-07')), 'Ronald', '2007-07-07', 'M', md5('pedro'), '2018-10-14', null);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-08-13 12:59:00')), md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-08-13 12:59:00', 50, 50, 1.45, true, 100, '120/80', 12, 38.2, 102);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-10-29 11:37:00')), md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-10-29 11:37:00', 52, 51, 1.45, true, 90, '120/80', 13, 36.3, 108);
insert into consulta values(md5(concat(md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-11-20 10:07:00')), md5('pedro'), md5(concat('Ronald', '2007-07-07')), '2018-11-20 10:07:00', 50, 50, 1.45, true, 80, '110/75', 12, 40.9, 89);

insert into enfermeiro values(md5('victor'), 'Victor Augusto', '2002-07-18', 'M', 'victor', md5('victor123'), true, curdate(), null);
insert into paciente values(md5(concat('Abibe', '2001-11-11')), 'Abibe', '2001-11-11', 'M', md5('victor'), '2017-10-08', null);
insert into paciente values(md5(concat('Ana', '2008-10-09')), 'Aba', '2008-10-09', 'F', md5('victor'), '2018-09-09', null);
insert into paciente values(md5(concat('André', '1999-08-18')), 'André', '1999-08-18', 'M', md5('victor'), '2016-12-31', null);

insert into enfermeiro values(md5('lucas'), 'Lucas Vale', '2001-07-07', 'M', 'lucas', md5('lucas123'), true, curdate(), null);
insert into paciente values(md5(concat('Adalberto', '1997-10-15')), 'Adalberto', '1997-10-15', 'M', md5('lucas'), '2017-10-15', null);
insert into paciente values(md5(concat('Roberta', '1990-01-01')), 'Roberta', '1990-01-01', 'F', md5('lucas'), '2018-01-01', null);
insert into paciente values(md5(concat('Patrick', '1995-03-21')), 'Patrick', '1995-03-21', 'M', md5('lucas'), '2018-11-11', null);

insert into enfermeiro values(md5('batman'), 'Batman', '1972-10-10', 'M', 'batman', md5('batman123'), false, curdate(), null);
insert into paciente values(md5(concat('Bruce', '1972-03-15')), 'Bruce', '1972-03-15', 'M', md5('batman'), '2018-11-20', null);
insert into paciente values(md5(concat('Alfred', '1940-09-19')), 'Alfred', '1940-09-19', 'M', md5('batman'), '2018-10-23', null);
insert into paciente values(md5(concat('James', '1958-10-28')), 'James', '1958-10-28', 'M', md5('batman'), '2017-12-15', null);

insert into enfermeiro values(md5('robin'), 'Robin', '1999-10-10', 'M', 'robin', md5('robin123'), false, curdate(), null);
insert into paciente values(md5(concat('Sherlock', '1975-12-12')), 'Sherlock', '1975-12-12', 'M', md5('robin'), '2016-11-21', null);
insert into paciente values(md5(concat('Mycroft', '1968-11-01')), 'Mycroft', '1968-11-01', 'M', md5('robin'), '2016-11-22', null);
insert into paciente values(md5(concat('Eurus', '1976-10-19')), 'Eurus', '1976-10-19', 'F', md5('robin'), '1981-11-21', null);