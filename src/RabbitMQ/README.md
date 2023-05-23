# Information
## Scripts
### latest RabbitMQ 3.11 with management UI?
`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management`
or 
`docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management`

### Basic rabbitMQ client
`docker run -d --name rabbitmq -p 5672:5672 --hostname my-rabbit --name some-rabbit rabbitmq:3`

## Links
[link](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html)
[link](https://code-maze.com/aspnetcore-rabbitmq/)

## Docker Builds
run command `docker build .`
or `docker build . -f Dockerfile.Receiver -t 'receiver:latest'`
or `docker build . -f Dockerfile.AutomatedSender -t 'autosender:latest'`
or `docker build . -f Dockerfile.Sender -t 'sender:latest'`