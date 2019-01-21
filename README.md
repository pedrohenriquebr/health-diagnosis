# Health-Diagnosis

Este projeto é um sistema de diagnóstico médico, que tem como propósito atender enfermeiros que precisam de uma ferramenta
para automatizar a classficação de risco de pacientes, criação de histórico médico, cadastro de consultas e 
dentre outras tarefas.

## Pré-requisitos

* [.NET Core SDK](https://dotnet.microsoft.com/download) (Windows)
* [MonoDevelop](https://www.monodevelop.com/download/) (Linux)
* Servidor MySQL/MariaDB

> Até o presente momento, o ambiente de desenvolvimento só funciona em sistemas Linux

## Guia de instalação

Clone o projeto e entre no diretório:

```bash
git clone https://www.github.com/pedrohenriquebr/health-diagnosis.git
cd health-diagnosis
```

### Base de dados MySQL

> Certifique-se que seu servidor MySQL/MariaDB esteja ligado.

Faça a instalação das tabelas.

```bash
cd database
make install
```

Instale amostras de consultas, pacientes e enfermeiros.

```bash
make install-samples
```

### Construção do projeto

Para release:

```bash
make build-release
```

Para depuração:

```bash
make build-debug
```

### Execução do projeto

Para release:

```bash
make release
```

Para depuração:

```bash
make debug
```

## Amostra de imagens


Exibindo histórico médico

![Tentativa de exibiri histórico médico](images/tenta_historico_paciente.jpg)

![Exibindo histórico médico](images/exibe_historico.jpg)

# Me ajude

Seja meu patrono:

[pedrobraga](https://www.patreon.com/pedrobraga)

