# Kafka Message Broker + .NET Core

Prova de conceito utilizando o message broker **Kadka** junto com o **.NET Core**.

## Referências de Estudos

https://www.hugopicado.com/2017/11/22/getting-started-with-net-core-and-kafka.html

## Testando

1. Abrir um Prompt de Comando e Executar o servidor do Kafka no Docker com o docker-compose

```bash
cd KafkaServer
docker-compose up
```

2. Abrir outro prompt de comando e Iniciar o projeto do Consumidor

```bash
cd KafkaConsumer
dotnet run 127.0.0.1:9092 TopicoTeste
```

3. Abrir outro prompt de comando e Iniciar o projeto do Produtor

```bash
cd KafkaProducer
dotnet run 127.0.0.1:9092 TopicoTeste
```