FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

# Copy fsproj and restore as distinct layers
COPY *.fsproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-x64 -p:PublishReadyToRun=true -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build-env /app/out .

# install tini to act as init 
RUN apt-get update && apt-get install -y tini &&\
    rm -rf /var/lib/apt/lists/*

COPY ./entrypoint.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["tini", "--", "entrypoint.sh", "dotnet", "fsharp-beats.dll"]
