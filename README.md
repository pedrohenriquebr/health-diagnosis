# Health-Diagnosis

This project is a medical health diagnosis system made to serve nurses that need a tool to automate patient risk ratings, medic history creation, registration of medical appointments, among other things.

## prerequisites

* [.NET Core SDK](https://dotnet.microsoft.com/download) (Windows)
* [MonoDevelop](https://www.monodevelop.com/download/) (Linux)
* Servidor MySQL/MariaDB

## Installation guide

Clone the project and go to the directory 

```console
$ git clone https://www.github.com/pedrohenriquebr/health-diagnosis.git
$ cd health-diagnosis
```

### MySQL Database

> Make sure that your MySQL/MariaDB is turned on.

Install the tables.

```console
$ cd database
$ make install
```

Install the appointments', patients' and nurses's samples.

```console
$ make install-samples
```

### Project building

Build the project:

```console
$ cd ..
$ make
```

> The debug is default target


### Project execution

For debugging:

```console
$ make debug
```


## Image samples


Displaying medic history

![Tentativa de exibiri histórico médico](images/tenta_historico_paciente.jpg)

![Exibindo histórico médico](images/exibe_historico.jpg)

# Contributors

* Thanks to Fabricio for helping translate `README.md` to English

# Help me

Be my patron:

[pedrobraga](https://www.patreon.com/pedrobraga)
