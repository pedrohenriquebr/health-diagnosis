alter user 'root'@'localhost' identified with mysql_native_password by 'root';
set @@global.time_zone = '-3:00';

drop database if exists diagnostico_de_saude;
create database if not exists diagnostico_de_saude;
use diagnostico_de_saude;

delimiter :
create procedure criacao()
begin
	create table if not exists enfermeiro
	(
		id varchar(32) primary key,
		nome varchar(100) not null,
		nascimento date not null,
		sexo varchar(1) not null,
		login varchar(100) not null,
		senha varchar(32) not null,
		stt bool not null,
		dataAdmissao date not null,
		dataDemissao date
	);

	create table if not exists paciente
	(
		id varchar(32) primary key,
		nome varchar(100) not null,
		nascimento date not null,
		sexo varchar(1) not null,
		enfermeiro varchar(32) not null,
		dataEntrada date not null,
		dataSaida date,
		foreign key (enfermeiro) references enfermeiro(id)
	);

	create table if not exists consulta
	(
		id varchar(32) primary key,
		enfermeiro varchar(32) not null,
		paciente varchar(32) not null,
		datahora datetime not null,
		massaCorporal float,
		circAbdominal float,
		altura float,
		jejum boolean,
		glicemia float,
		pressaoArterial varchar(7),
		respiracao int,
		temperatura float,
		batimentoCardio int,
		foreign key (enfermeiro) references enfermeiro(id),
		foreign key (paciente) references paciente(id)
	);

	create table if not exists historicoMedico
	(
		paciente varchar(32) primary key,
		dadosConsulta text,
		foreign key (paciente) references paciente(id)
	);

	create table if not exists relatorio
	(
		id varchar(32) primary key,
		conteudo text not null,
		datahora datetime not null,
		paciente varchar(32) not null,
		enfermeiro varchar(32) not null,
		foreign key (paciente) references paciente(id),
		foreign key (enfermeiro) references enfermeiro(id)
	);

	create table if not exists problema
	(
		id varchar(32) primary key,
		nome varchar(100) not null,
		stt boolean not null,
		detalhes text not null,
		descricao text not null
	);

	create table if not exists solucao
	(
		id varchar(32) primary key,
		nome text not null,
		descricao text not null
	);

	create table if not exists problemaSolucao
	(
		problema varchar(32) not null,
		solucao varchar(32) not null,
		foreign key (problema) references problema(id),
		foreign key (solucao) references solucao(id),
		primary key (problema, solucao)
	);

	create table if not exists problemaRelatorio
	(
		problema varchar(32) not null,
		relatorio varchar(32) not null,
		foreign key (problema) references problema(id),
		foreign key (relatorio) references relatorio(id),
		primary key (problema, relatorio)
	);
end:
delimiter ;

delimiter :
create procedure problema()
begin
	#IMC
	#{
		insert into problema value
		(
			md5("Baixo Peso"),
			"Baixo Peso",
			true,
			"imc <= 18",
			"Seu IMC está abaixo de 18. Isso indica que sua massa está abaixo do considerado normal para alguém com sua altura. Entretanto, suas chances de sofrer de comorbidade são baixas."
		);

		insert into problema value
		(
			md5("Peso Normal"),
			"Peso Normal",
			false,
			"imc > 18 && imc < 25",
			"Seu peso está compatível com sua altura. Entretanto, suas chances de sofrer de comorbidade são médias."
		);

		insert into problema value
		(
			md5("Sobrepeso"),
			"Sobrepeso",
			true,
			"imc >= 25 && imc < 30",
			"Sua massa está acima do ideal saudável. Entretanto, suas chances de sofrer de comorbidade são médias."
		);

		insert into problema value
		(
			md5("Obesidade Grau I"),
			"Obesidade Grau I",
			true,
			"imc >= 30 && imc < 35",
			"Sua massa é superior ao considerado saudável. Essa é, contudo, a forma mais branda dessa condição. Entretanto, suas chances de sofrer de comorbidade são moderadas."
		);

		insert into problema value
		(
			md5("Obesidade Grau II"),
			"Obesidade Grau II",
			true,
			"imc >= 35 && imc < 40",
			"Sua massa é bem superior ao considerado saudável. Sua condição é preocupante. Suas chances de sofrer de comorbidade são graves."
		);

		insert into problema value
		(
			md5("Obesidade Grau III"),
			"Obesidade Grau III",
			true,
			"imc >= 40",
			"Sua massa é excessivamente superior ao considerado saudável. Sua condição é perigosa. As suas chances de sofrer de comorbidade são muito graves."
		);
	#}

	#Temperatura
	#{
		insert into problema value
		(
			md5("Hipotermia Profunda"),
			"Hipotermia Profunda",
			true,
			"temperatura <= 27",
			"Sua temperatura está fatalmente baixa. Há sinais de amnésia, torna-se impossível usar as mãos, diminui-se o pulso, respiração e a atividade celular. Provoca-se falha dos órgãos vitais, levando à morte clínica."
		);

		insert into problema value
		(
			md5("Hipotermia Grave"),
			"Hipotermia Grave",
			true,
			"temperatura > 27 && temperatura <= 30",
			"Há imobilidade e inconsciência, as pupilas se dilatam e a freqüência cardíaca diminui, se tornando quase imperceptível. Se não houver o devido tratamento, a morte é inevitável"
		);

		insert into problema value
		(
			md5("Hipotermia Moderada"),
			"Hipotermia Moderada",
			true,
			"temperatura > 30 && temperatura <= 33",
			"Seus arrepios são mais intensos, e seus movimentos lentos. As extremidades ficam azuladas, há um pouco de confusão, com alterações na memória e na fala. Encontra-se num estado de quase inconsciência."
		);

		insert into problema value
		(
			md5("Hipotermia Leve"),
			"Hipotermia Leve",
			true,
			"temperatura > 33 && temperatura < 36",
			"Há sensação de frio, tremores, letargia motora e espasmos musculares. A pele fica fria, as extremidades do corpo apresentam tonalidade cinzenta ou levemente arroxeada. Provavemente, haverá confusão mental."
		);

		insert into problema value
		(
			md5("Temperatura Normal"),
			"Temperatura Normal",
			false,
			"temperatura >=36 && temperatura <= 37",
			"Sua temperatura está dentro dos limites do considerado normal e saudável."
		);

		insert into problema value
		(
			md5("Febre"),
			"Febre",
			true,
			"temperatura > 37 && temperatura < 39",
			"Sua temperatura está acima do considerado normal. Pode haver suor, tremedeira, dor de cabeça, dores musculares, perda de apetite, desidratação e fraqueza geral."
		);

		insert into problema value
		(
			md5("Pirexia"),
			"Pirexia",
			true,
			"temperatura >= 39 && temperatura <= 4",
			"Sua temperatura está bastante elevada. Sintomas como dores musculares, desidratação e fraqueza são intensificados."
		);

		insert into problema value
		(
			md5("Hiperpirexia"),
			"Hiperpirexia",
			true,
			"temperatura > 40",
			"Sua temperatura está extremamente elevada. Podem haver alucinaçções, confusão, irritabilidade, convulsão e desidratação."
		);
	#}

	#Pulso
	#{
		insert into problema value
		(
			md5("Bradisfigmia"),
			"Bradisfigmia",
			true,
			"pulso <= 59",
			"A frequência de seus batimentos cardíacos escontra-se abaxo do normal. Pode ocorrer fraqueza, cansaço, falta de ar, dor no peito, desmaios e tonturas."
		);

		insert into problema value
		(
			md5("Normocardia"),
			"Normocardia",
			false,
			"pulso >= 60 && pulso <= 100",
			"A frequência cardíaca está dentro dos limites do considerado normal."
		);

		insert into problema value
		(
			md5("Taquisfigmia"),
			"Taquisfigmia",
			true,
			"pulso > 101",
			"A frequência cardíaca está acima do normal. Podem ocorrer tonturas e vertigens, falta de ar e cansaço. Porém, pode ser apenas uma resposta normal e saudável do corpo a algum estímulo, como um susto ou a prática de exercícios físicos."
		);
	#}

	#Respiração
	#{
		insert into problema value
		(
			md5("Bradipneia"),
			"Bradipneia",
			true,
			"respiracao <= 11",
			"Sua taxa de respiração está abaixo do normal. Pode haver tontura, cansaço, fraqueza, dores no peito, desmaios e confusão mental."
		);

		insert into problema value
		(
			md5("Eupneia"),
			"Eupneia",
			false,
			"respiracao > 11 && respiracao <= 20",
			"Sua taxa de respiração está dentro dos limites da normalidade."
		);

		insert into problema value
		(
			md5("Taquipneia"),
			"Taquipneia",
			true,
			"respiracao > 20",
			"Sua taxa de respiração está maior que a normal. Os órgãos respiratórios se movimentam de forma semelhante a produzida por grandes esforços físicos."
		);
	#}

	#Circunferência Abdominal
	#{
		insert into problema value
		(
			md5("Sem Risco de Complicações Metabólicas"),
			"Sem Risco de Complicações Metabólicas",
			false,
			"(sexo =='F' && circAbdiminal <= 70) || (sexo =='M' && circAbdominal <= 60)",
			"Sua condição não apresenta risco de complicações metabólicas por conta de sua circunferência abdominal."
		);

		insert into problema value
		(
			md5("Risco de Complicações Metabólicas Aumentado"),
			"Risco de Complicações Metabólicas Aumentado",
			true,
			"(sexo == 'F' && circAbdominal > 70 && circAbdiminal <= 81) || (sexo == 'M' && circAbdominal > 60 && circAbdominal <= 95)",
			"Sua circunferência abdominal aumenta seu risco de desenvolver complicações metabólicas."
		);

		insert into problema value
		(
			md5("Risco de Complicações Metabólicas Aumentado Substancialmente"),
			"Risco de Complicações Metabólicas Aumentado Substancialmente",
			true,
			"(sexo == 'F' && circAbdominal > 81) || (sexo == 'M' && circAbdominal > 95)",
			"Sua circunferência abdominal aumenta substancialmente seu risco de desenvolver complicações metabólicas."
		);
	#}

	#Glicemia
	#{
		insert into problema value
		(
			md5("Hipoglicemia"),
			"Hipoglicemia",
			true,
			"(jejum == true && glicemia <= 50) || (jejum == false && glicemia <= 50)",
			"Seu nível de açúcar no sangue está abaixo do normal. Pode ocorrer confusão mental, comportamento anormal, dificuldade em realizar atividades simples e rotineiras, palpitações, tremores e fome excessiva."
		);

		insert into problema value
		(
			md5("Glicemia Normal"),
			"Glicemia Normal",
			false,
			"(jejum == true && glicemia > 50 && glicemia <= 110) || (jejum == false && glicemia > 50 && glicemia <= 140)",
			"Seu nível de açúcar no sangue está dentro dos limites da normalidade."
		);

		insert into problema value
		(
			md5("Pré-diabetes"),
			"Pré-diabetes",
			true,
			"(jejum == true && glicemia > 110 && glicemia <= 126) || (jejum == false && glicemia > 140 && glicemia <= 200)",
			"Seu nível de açúcar no sangue está acima do normal. É possível que não haja quaisquer outros sintomas."
		);

		insert into problema value
		(
			md5("Diabetes"),
			"Diabetes",
			true,
			"(jejum == true && glicemia > 126) || (jejum == false && glicemia > 200)",
			"Seu nível de açúcar no sangue está muito acima do normal, sendo apresentado o quadro de diabetes. É normal, nesse caso, ocorrer aumento da sede, secura constante da boca, vontade frequente de urinar, cansaço frequente, visão turva, lentidão e dificuldade de cicatrização e infecções frequentes."
		);
	#}

	#Pressão Arterial
	#{
		insert into problema value
		(
			md5("Hipotensão"),
			"Hipotensão",
			true,
			"pas <= 90 && pad <= 60",
			"A pressão arterial está abaixo do normal. Podem ocorrer tontura, desmaios, cefaleia, visão embaçada e palidez."
		);

		insert into problema value
		(
			md5("Pressão Arterial Normal"),
			"Pressão Arterial Normal",
			false,
			"pas > 90 && pas <= 130 && pad > 60 && pad <= 85",
			"A pressão arterial está dentro dos limites da normalidade."
		);

		insert into problema value
		(
			md5("Pressão Arterial Normal Limítrofe"),
			"Pressão Arterial Normal Limítrofe",
			true,
			"pas > 130 && pas <= 139 && pad > 85 && pad <= 89",
			"A pressão arterial está no limite do considerado normal. Há um certo risco cardiovascular."
		);

		insert into problema value
		(
			md5("Hipertensão Leve"),
			"Hipertensão Leve",
			true,
			"pas > 139 && pas <= 159 && pad > 89 && pad <= 99",
			"A pressão arterial está no acima do padrão de normalidade. Podem ocorrer sintomas como tontura, visão turva e desmaios."
		);

		insert into problema value
		(
			md5("Hipertensão Moderada"),
			"Hipertensão Moderada",
			true,
			"pas > 159 && pas <= 179 && pad > 99 && pad <= 109",
			"A pressão arterial está bem acima do padrão de normalidade. Há um maior risco de ocorrer tontura, dores de cabeça, dores no peito, zumbidos no ouvido e visão turva."
		);

		insert into problema value
		(
			md5("Hipertensão Grave"),
			"Hipertensão Grave",
			true,
			"pas >= 180 && pad >= 110",
			"A pressão arterial muito acima do normal. Sintomas como tontura, desmaios, cefaleia, visão embaçada e palidez podem ser intensificados."
		);

		insert into problema value
		(
			md5("Hipertensão Sistólica Isolada"),
			"Hipertensão Sistólica Isolada",
			true,
			"pas >= 140 && pad < 90",
			"Mais comumente presente em idosos, pode ser causada pelo enrijecimente das artérias. Apresenta-se o risco de eventos cardiovasculares, AVCs e problemas de insuficiência cardíaca."
		);
	#}
end:
delimiter ;

delimiter :
create procedure solucao()
begin
	#IMC
	#{
		insert into solucao  values
		(
			md5("Baixo Peso"),
			"Recomendação - Baixo Peso",
			"Indica-se a manutenção de consultas regulares com um nutricionista, para a determinação e o controle de uma dieta apropriada. O tabagismo é contraindicado."
		);

		insert into solucao values
		(
			md5("Peso Normal"),
			"Recomendação - Peso Normal",
			"Sua saúde parece apresentar boas condições quanto ao peso. Entretanto, somente consultas com nutricionistas podem garantir se a alimentação é ideal."
		);

		insert into solucao values
		(
			md5("Sobrepeso"),
			"Recomendação - Sobrepeso",
			"Indicam-se consultas regulares com nutricionistas, para que seja aferida uma dieta com base nas especificações do paciente, aumento de atividades físicas e controle de stress."
		);

		insert into solucao values
		(
			md5("Obesidade Grau I"),
			"Recomendação - Obesidade Grau I",
			"Recomendam-se consultas com clínico geral, endocrinologista e nutricionista para que sejam aferidos corretamente a dieta, os exercícios e os medicamentos a serem tomados."
		);

		insert into solucao values
		(
			md5("Obesidade Grau II"),
			"Recomendação - Obesidade Grau II",
			"Recomendam-se consultas com clínico geral, endocrinologista e nutricionista para que sejam aferidos corretamente a dieta, os exercícios e os medicamentos a serem tomados."
		);

		insert into solucao values
		(
			md5("Obesidade Grau III"),
			"Recomendação - Obesidade Grau III",
			"Indicam-se consultas com clínico geral, endocrinologista e nutricionista para que sejam aferidos corretamente a dieta, os exercícios e os medicamentos a serem tomados."
		);
	#}

	#Temperatura
	#{
		insert into solucao values
		(
			md5("Hipotermia Profunda"),
			"Recomendação - Hipotermia Profunda",
			"Indica-se a prática de exercícios para aumentar a circulação do sangue, buscar massagistas e angiologistas. Deve-se comer e beber regularmente e o álcool é contraindicado."
		);

		insert into solucao values
		(
			md5("Hipotermia Grave"),
			"Recomendação - Hipotermia Grave",
			"Indica-se a prática de exercícios para aumentar a circulação do sangue, buscar massagistas e e angiologistas, deve comer e beber regularmente e o álcool é contraindicado."
		);

		insert into solucao values
		(
			md5("Hipotermia Moderada"),
			"Recomendação - Hipotermia Moderada",
			"Indica-se a prática de exercícios para aumentar a circulação do sangue, buscar massagistas e angiologistas."
		);

		insert into solucao values
		(
			md5("Hipotermia Leve"),
			"Recomendação - Hipotermia Leve",
			"Indica-se a prática de exercícios para aumentar a circulação do sangue, buscar massagistas e angiologistas."
		);

		insert into solucao values
		(
			md5("Temperatura Normal"),
			"Recomendação - Temperatura Normal",
			"Sua saúde parece apresentar boas condições quanto à temperatura, mantenha com regularidade os check-ups."
		);

		insert into solucao values
		(
			md5("Febre"),
			"Recomendação - Febre",
			"Indica-se uma bateria de exames com um clínico geral para que ele afira qual a causa da febre e, portanto, possa prescrever o medicamente que melhor resolva o problema."
		);

		insert into solucao values
		(
			md5("Pirexia"),
			"Recomendação - Pirexia",
			"Indica-se a ingestão correta das medicações antipiréticas prescritas por um clínico geral."
		);

		insert into solucao values
		(
			md5("Hiperpirexia"),
			"Recomendação - Hiperpirexia",
			"Indicam-se banhos frios com esponjas molhadas sobre a pele, hidratação líquida intravenosa ou oral e utilização correta dos medicamentos prescritos."
		);
	#}

	#Pulso
	#{
		insert into solucao values
		(
			md5("Bradisfigmia"),
			"Recomendação - Bradisfigmia",
			"Recomenda-se a busca por um profissional da área cardiológica e, se prescrito, o correto uso de medicamentos."
		);

		insert into solucao values
		(
			md5("Normocardia"),
			"Recomendação - Normocardia",
			"Sua saúde apresenta boas condições quanto aos batimentos cardíacos, porém consultas regulares com cardiologistas são indicadas para a manutenção desse estado."
		);

		insert into solucao values
		(
			md5("Taquisfigmia"),
			"Recomendação - Taquisfigmia",
			"Indicam-se consultas regulares com cardiologistas e o cumprimento correto das prescrições médicas."
		);
	#}

	#Respiração
	#{
		insert into solucao values
		(
			md5("Bradipneia"),
			"Recomendação - Bradipneia",
			"Indica-se a consulta com um otorrinolaringologista para definir a melhor forma de se garantir a oxigenação adequada dos pulmões e evitar a parada do funcionamento pulmonar."
		);

		insert into solucao values
		(
			md5("Eupneia"),
			"Recomendação - Eupneia",
			"Recomenda-se seguir corretamente a medicação indicada por um otorrinolaringologista, que podem incluir remédios contra inflamação ou suplementação de oxigênio, além da prática de exercícios para aumento da capacidade pulmonar e aptidão física geral."
		);

		insert into solucao values
		(
			md5("Taquipneia"),
			"Recomendação - Taquipneia",
			"Recomenda-se uma consulta com um clínico geral, pneumologista ou cardiologista e o correto uso de medicamentos, caso este seja prescrito."
		);
	#}

	#Circunferência Abdominal
	#{
		insert into solucao values
		(
			md5("Sem Risco de Complicações Metabólicas"),
			"Recomendação - Sem Risco de Complicações Metabólicas",
			"Sua saúde parece apresentar boas condições quanto ao metabolismo, porém consultas regulares a nutricionistas são indicadas para que esse quadro se mantenha."
		);

		insert into solucao values
		(
			md5("Risco de Complicações Metabólicas Aumentado"),
			"Recomendação - Risco de Complicações Metabólicas Aumentado",
			"Indicam-se consultas com clínicos gerais ou endocrinologistas e seguir corretamente os medicamentos prescritos. Uma avaliação com um nutricionista também será necessária para a formulação de uma dieta adequada a seu caso."
		);

		insert into solucao values
		(
			md5("Risco de Complicações Metabólicas Aumentado Substancialmente"),
			"Recomendação - Risco de Complicações Metabólicas Aumentado Substancialmente",
			"Indicam-se alterações alimentícias, modificação comportamental e aumento de atividades físicas acompanhadas por nutricionistas e, se necessárias, cirurgias podem ser recomendadas para que o quadro não se agrave."
		);
	#}

	#Glicemia
	#{
		insert into solucao values
		(
			md5("Hipoglicemia"),
			"Recomendação - Hipoglicemia",
			"Indica-se a consulta com um nutricionista para a formulação de dietas balanceadas, com fracionamento das refeições, seguir as restrições médicas e evitar jejuns prolongados e alimentos açucarados."
		);

		insert into solucao values
		(
			md5("Glicemia Normal"),
			"Recomendação - Glicemia Normal",
			"Sua saúde parece apresentar boas condições quanto a glicemia, porém consultas regulares a endocrinologistas são indicadas para que esse quadro se mantenha."
		);

		insert into solucao values
		(
			md5("Pré-diabetes"),
			"Recomendação - Pré-diabetes",
			"Indica-se uma consulta com um nutricionista para a formulação de dietas adequadas e seguir corretamente os medicamentos indicados."
		);

		insert into solucao values
		(
			md5("Diabetes"),
			"Recomendação - Diabetes",
			"Deve-se consultar um endocrinologista e seguir corretamente as medicações indicadas, manter a fracionalidade na alimentação, consumir fibras, cereais e alimentos diets, beber bastante água e evitar todo tipo de açúcar, adoçante e gordura saturada."
		);
	#}

	#Pressão Arterial
	#{
		insert into solucao values
		(
			md5("Hipotensão"),
			"Recomendação - Hipotensão",
			"Indica-se a adição de sal à dieta, aumento na ingestão de água, uso de meias de compressão, realizar consultas regulares com o cardiologista e seguir as prescrições médicas corretamente."
		);

		insert into solucao values
		(
			md5("Pressão Arterial Normal"),
			"Recomendação - Pressão Arterial Normal",
			"Sua saúde parece apresentar boas condições quanto à pressão arterial, mas mantenha a regularidade dos check-ups."
		);

		insert into solucao values
		(
			md5("Pressão Arterial Normal Limítrofe"),
			"Recomendação - Pressão Arterial Normal Limítrofe",
			"Indica-se a manutenção do peso ideal, redução na ingestão de sódio, aumento na ingestão de potássio, redução ou abandono da ingestão de álcool, prática de exercícios físicos e suplementação de cálcio e magnésio. Contraindica-se o tabagismo. Deve-se entrar em contato com um profissional da saúde para se obter as especificações do tratamento."
		);

		insert into solucao values
		(
			md5("Hipertensão Leve"),
			"Recomendação - Hipertensão",
			"Recomendam-se avaliações regulares com cardiologistas, aumento na ingestão de vitamina D, de amêndoas e de nozes, redução no consumo de sal e a prática de atividades físicas com moderação. Contraindica-se o tabagismo e o consumo de bebidas alcóolicas."
		);

		insert into solucao values
		(
			md5("Hipertensão Moderada"),
			"Recomendação - Hipertensão Moderada",
			"Indicam-se consultas regulares com cardiologistas, para que o melhor tratamento seja aferido quanto às especificações do paciente."
		);

		insert into solucao values
		(
			md5("Hipertensão Grave"),
			"Recomendação - Hipertensão Grave",
			"Indicam-se consultas regulares com cardiologistas, para que o melhor tratamento seja aferido quanto às especificações do paciente."
		);

		insert into solucao values
		(
			md5("Hipertensão Sistólica Isolada"),
			"Recomendação - Hipertensão Sistólica Isolada",
			"Indica-se a redução da ingestão de sal, implementação de exercícios anaeróbicos e perda de peso. A ingestão de bebidas alcoólicas e o tabagismo são contraindicados."
		);
	#}
end:
delimiter ;

delimiter :
create procedure problemaSolucao()
begin
	#IMC
	#{
		insert into problemaSolucao values
		(
			md5("Baixo Peso"),
			md5("Baixo Peso")
		);

		insert into problemaSolucao values
		(
			md5("Peso Normal"),
			md5("Peso Normal")
		);

		insert into problemaSolucao values
		(
			md5("Sobrepeso"),
			md5("Sobrepeso")
		);

		insert into problemaSolucao values
		(
			md5("Obesidade Grau I"),
			md5("Obesidade Grau I")
		);

		insert into problemaSolucao values
		(
			md5("Obesidade Grau II"),
			md5("Obesidade Grau II")
		);

		insert into problemaSolucao values
		(
			md5("Obesidade Grau III"),
			md5("Obesidade Grau III")
		);
	#}

	#Temperatura
	#{
		insert into problemaSolucao values
		(
			md5("Hipotermia Profunda"),
			md5("Hipotermia Profunda")
		);

		insert into problemaSolucao values
		(
			md5("Hipotermia Grave"),
			md5("Hipotermia Grave")
		);

		insert into problemaSolucao values
		(
			md5("Hipotermia Moderada"),
			md5("Hipotermia Moderada")
		);

		insert into problemaSolucao values
		(
			md5("Hipotermia Leve"),
			md5("Hipotermia Leve")
		);

		insert into problemaSolucao values
		(
			md5("Temperatura Normal"),
			md5("Temperatura Normal")
		);

		insert into problemaSolucao values
		(
			md5("Febre"),
			md5("Febre")
		);

		insert into problemaSolucao values
		(
			md5("Pirexia"),
			md5("Pirexia")
		);

		insert into problemaSolucao values
		(
			md5("Hiperpirexia"),
			md5("Hiperpirexia")
		);
	#}

	#Pulso
	#{
		insert into problemaSolucao values
		(
			md5("Bradisfigmia"),
			md5("Bradisfigmia")
		);

		insert into problemaSolucao values
		(
			md5("Normocardia"),
			md5("Normocardia")
		);

		insert into problemaSolucao values
		(
			md5("Taquisfigmia"),
			md5("Taquisfigmia")
		);
	#}

	#Respiração
	#{
		insert into problemaSolucao values
		(
			md5("Bradipneia"),
			md5("Bradipneia")
		);

		insert into problemaSolucao values
		(
			md5("Eupneia"),
			md5("Eupneia")
		);

		insert into problemaSolucao values
		(
			md5("Taquipneia"),
			md5("Taquipneia")
		);
	#}

	#Circunferência Abdominal
	#{
		insert into problemaSolucao values
		(
			md5("Sem Risco de Complicações Metabólicas"),
			md5("Sem Risco de Complicações Metabólicas")
		);

		insert into problemaSolucao values
		(
			md5("Risco de Complicações Metabólicas Aumentado"),
			md5("Risco de Complicações Metabólicas Aumentado")
		);

		insert into problemaSolucao values
		(
			md5("Risco de Complicações Metabólicas Aumentado Substancialmente"),
			md5("Risco de Complicações Metabólicas Aumentado Substancialmente")
		);
	#}

	#Glicemia
	#{
		insert into problemaSolucao values
		(
			md5("Hipoglicemia"),
			md5("Hipoglicemia")
		);

		insert into problemaSolucao values
		(
			md5("Glicemia Normal"),
			md5("Glicemia Normal")
		);

		insert into problemaSolucao values
		(
			md5("Pré-diabetes"),
			md5("Pré-diabetes")
		);

		insert into problemaSolucao values
		(
			md5("Diabetes"),
			md5("Diabetes")
		);
	#}

	#Pressão Arterial
	#{
		insert into problemaSolucao values
		(
			md5("Hipotensão"),
			md5("Hipotensão")
		);

		insert into problemaSolucao values
		(
			md5("Pressão Arterial Normal"),
			md5("Pressão Arterial Normal")
		);

		insert into problemaSolucao values
		(
			md5("Pressão Arterial Normal Limítrofe"),
			md5("Pressão Arterial Normal Limítrofe")
		);

		insert into problemaSolucao values
		(
			md5("Hipertensão Leve"),
			md5("Hipertensão Leve")
		);

		insert into problemaSolucao values
		(
			md5("Hipertensão Moderada"),
			md5("Hipertensão Moderada")
		);

		insert into problemaSolucao values
		(
			md5("Hipertensão Grave"),
			md5("Hipertensão Grave")
		);

		insert into problemaSolucao values
		(
			md5("Hipertensão Sistólica Isolada"),
			md5("Hipertensão Sistólica Isolada")
		);
	#}
end:
delimiter ;



delimiter :
create procedure main()
begin
	call criacao();
    call problema();
    call solucao;
    call problemaSolucao();
	set @nome = 'Administrador';
	set @login = 'admin';
	set @pass = 'admin123';
	set @birth  = curdate();
	set @sex  = 'X';
	insert into enfermeiro values(md5(@login), @nome, @birth , @sex, @login, md5(@pass), true,@birth, null);
end:
delimiter ;

call main();

create trigger problema before delete on problema for each row delete from problemaSolucao where problema = old.id;
create trigger solucao before delete on solucao for each row delete from problemaSolucao where solucao = old.id;
