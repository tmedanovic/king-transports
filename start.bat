@echo off
echo "Please wait while starting, if everything goes well, Consul dashbord will open.."
start   cmd.exe /C "docker-compose -f docker-compose.yml up"
TIMEOUT /T 60
start   cmd.exe /C "cd Services\KingTransports.Authentication & dotnet run"
TIMEOUT /T 10
start   cmd.exe /C "cd Services\KingTransports.AccountingService & dotnet run"
TIMEOUT /T 10
start   cmd.exe /C "cd Services\KingTransports.FleetService & dotnet run"
TIMEOUT /T 10
start   cmd.exe /C "cd Services\KingTransports.TicketingService & dotnet run"
TIMEOUT /T 10
start   cmd.exe /C "cd Services\KingTransports.Gateway & dotnet run"
TIMEOUT /T 10
start http://localhost:8500