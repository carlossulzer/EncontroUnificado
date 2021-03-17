USE EncontroUnif
GO

delete from eventos
delete from agenda
delete from banca
delete from calendario
delete from caracterizacao
delete from horario
delete from integrantes
delete from nucleo
delete from orientador
delete from palestrante
delete from professor
delete from recursos
delete from sala
delete from tipo_evento
delete from voluntario


DBCC CHECKIDENT (caracterizacao, RESEED, 0)
DBCC CHECKIDENT (horario, RESEED, 0)
DBCC CHECKIDENT (nucleo, RESEED, 0)
DBCC CHECKIDENT (professor, RESEED, 0)
DBCC CHECKIDENT (recursos, RESEED, 0)
DBCC CHECKIDENT (sala, RESEED, 0)
DBCC CHECKIDENT (tipo_evento, RESEED, 0)
DBCC CHECKIDENT (eventos, RESEED, 0)
DBCC CHECKIDENT (recursos, RESEED, 0)

insert into recursos (descricao) values ('NENHUM')
insert into caracterizacao (descricao) values ('NENHUMA')
insert into nucleo (descricao) values ('NENHUM')
insert into tipo_evento (descricao) values ('NENHUM')

insert into eventos (cod_caracterizacao, cod_recurso, cod_tipo_evento, cod_nucleo, titulo) values (1,1,1,1,'NENHUM')


