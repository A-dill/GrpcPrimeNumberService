DOTNET = dotnet

CLIENT_OUTPUT_DIR = bin/client
SERVER_OUTPUT_DIR = bin/server

server:
	$(DOTNET) build GrpcPrimeNumberService/GrpcPrimeNumberService.csproj -c Release -o $(SERVER_OUTPUT_DIR)

client:
	$(DOTNET) build PrimeNumberClient/PrimeNumberClient.csproj -c Release -o $(CLIENT_OUTPUT_DIR)

all: server client

clean:
	for /r "$(CLIENT_OUTPUT_DIR)" %%F in (*) do del "%%F"
	rd /s /q "$(CLIENT_OUTPUT_DIR)"
	
	for /r "$(SERVER_OUTPUT_DIR)" %%F in (*) do del "%%F"
	rd /s /q "$(SERVER_OUTPUT_DIR)"
run-server:
	$(DOTNET) run --project GrpcPrimeNumberService/GrpcPrimeNumberService.csproj

run-client:
	$(DOTNET) run --project PrimeNumberClient/PrimeNumberClient.csproj
