# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT="production"

# add nuget source
ARG CODEARTIFACT_AUTH_TOKEN
ARG NUGET_FEED_URL
RUN dotnet nuget add source "${NUGET_FEED_URL}" -n "king-transports/king-transports" -u "aws" -p "${CODEARTIFACT_AUTH_TOKEN}" --store-password-in-clear-text

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port your application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "KingTransports.Authentication.dll"]