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

	mkdir D:\SeqData
	docker run -e ACCEPT_EULA=Y --name seq -d --restart always -p 8090:80 -p 5341:5341 -v D:/SeqData:/data datalust/seq:latest

In my case I do it like this:

	mkdir C:\Data\Seq\Data
	docker run -e ACCEPT_EULA=Y --name seq -d --restart always -p 56789:80 -p 5341:5341 -v C:/Data/Seq/Data:/data datalust/seq:latest

Url: http://localhost:56789

## 4 Configuration example for forwarded headers

	{
		"ForwardedHeaders": {
			"ForwardedHeaders": "All"
		}
	}

## 5 Links

- [Bootstrap logging with Serilog + ASP.NET Core](https://nblumhardt.com/2020/10/bootstrap-logger/)