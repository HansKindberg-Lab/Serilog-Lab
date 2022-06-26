# Serilog-Lab

This is a web-application to lab with Serilog.

![.github/workflows/Docker-deploy.yml](https://github.com/HansKindberg-Lab/Serilog-Lab/actions/workflows/Docker-deploy.yml/badge.svg)

Web-application, without configuration, pushed to Docker Hub: https://hub.docker.com/r/hanskindberg/serilog-lab

## 1 Log-levels

- Microsoft.Extensions.Logging.LogLevel.Trace = Serilog.Events.LogEventLevel.Verbose
- Microsoft.Extensions.Logging.LogLevel.Debug = Serilog.Events.LogEventLevel.Debug
- Microsoft.Extensions.Logging.LogLevel.Information = Serilog.Events.LogEventLevel.Information
- Microsoft.Extensions.Logging.LogLevel.Warning = Serilog.Events.LogEventLevel.Warning
- Microsoft.Extensions.Logging.LogLevel.Error = Serilog.Events.LogEventLevel.Error
- Microsoft.Extensions.Logging.LogLevel.Critical = Serilog.Events.LogEventLevel.Fatal
- Microsoft.Extensions.Logging.LogLevel.None = does not exist in Serilog

## 2 Output-template

In this lab I use:

- "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} - [{Level}] - {SourceContext}: {Message}{NewLine}{Exception}"

By default the different sinks have their own "outputTemplate", see below.

### 2.1 Console

Default: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"

### 2.2 Debug

Default: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"

### 2.3 File

Default: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"

## 3 Seq

Running Seq locally with Docker:

- [Getting A Local Seq Instance Up And Running](https://www.jabbermouth.co.uk/2021/01/26/getting-a-local-seq-instance-up-and-running/)
- [Running Seq in a Docker container](https://docs.datalust.co/docs/getting-started-with-docker#running-seq-in-a-docker-container)

From first link above:

	mkdir D:\SeqData
	docker run -e ACCEPT_EULA=Y --name seq -d --restart always -p 8090:80 -p 5341:5341 -v D:/SeqData:/data datalust/seq:latest

In my case I do it like this:

	mkdir C:\Data\Seq\Data
	docker run -e ACCEPT_EULA=Y --name seq -d --restart always -p 56789:80 -p 5341:5341 -v C:/Data/Seq/Data:/data datalust/seq:latest

Url: http://localhost:56789

## 4 Fluentd

- [A Serilog sink sending log events over HTTP.](https://github.com/FantasticFiasco/serilog-sinks-http)
- [Sample application of Serilog.Sinks.Http sending log events to Fluentd.](https://github.com/FantasticFiasco/serilog-sinks-http-sample-fluentd)
- https://github.com/FantasticFiasco/serilog-sinks-http-sample-fluentd/blob/master/serilog/Program.cs#L13

In my case Fluentd sends logs to Elasticsearch therefore we need to format the logs for it:

	Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter, Serilog.Formatting.Elasticsearch

NuGet-references needed:

- Serilog.Formatting.Elasticsearch
- Serilog.Sinks.Http

I use secrets to laborate with configuration for Fluentd. Fluentd is setup at my work and the configuration for it is confidential.

Path to your secrets file for this solution: C:\Users\{USERNAME}\AppData\Roaming\Microsoft\UserSecrets\8fb36c14-a5cc-429a-87d1-d782ef9b3eb8\secrets.json

In the secrets file you can start to configure something like this:

	{
		"Serilog": {
			"WriteTo": {
				"Fluentd": {
					"Name": "Http",
					"Args": {
						"queueLimitBytes": null,
						"requestUri": "http://fluentd.example.org:9880/example.tag",
						"textFormatter": "Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter, Serilog.Formatting.Elasticsearch"
					}
				}
			}
		}
	}

## 5 Configuration example for forwarded headers

	{
		"ForwardedHeaders": {
			"ForwardedHeaders": "All"
		}
	}

## 6 Links

- [Bootstrap logging with Serilog + ASP.NET Core](https://nblumhardt.com/2020/10/bootstrap-logger/)
- [Writing logs to Elasticsearch with Fluentd using Serilog in ASP.NET Core](https://andrewlock.net/writing-logs-to-elasticsearch-with-fluentd-using-serilog-in-asp-net-core/)