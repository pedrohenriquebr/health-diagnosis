# Health-Diagnosis

Este projeto é um sistema de diagnóstico médico, que tem como propósito atender enfermeiros que precisam de uma ferramenta
para automatizar a classficação de risco de pacientes, criação de histórico médico, cadastro de consultas e
dentre outras tarefas.

> Até o presente momento, o ambiente de desenvolvimento só funciona em sistemas Linux

## Guia de instalação

Clone o projeto e entre no diretório:

```bash
git clone https://www.github.com/pedrohenriquebr/health-diagnosis.git
cd health-diagnosis
```

Instale a base de dados SQL

```bash
./install_database.sh
```

> Certifique-se que seu servidor MySQL/MariaDB esteja ligado.

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

Menu

![Menu Principal](images/menu.jpg)

Cadastro de um paciente

![Cadastro de um paciente](images/cadastra_paciente.jpg)

Criando uma consulta

![Criando uma consulta](images/consulta.jpg)

Exemplo de relatório gerado

![Gerando relatório](images/relatorio_exemplo.jpg)

Edição de relatório

![Editando relatório](images/edita_relatorio.jpg)

Exibindo histórico médico

![Tentativa de exibiri histórico médico](images/tenta_historico_paciente.jpg)

![Exibiindo histórico médico](images/exibe_historico.jpg)
