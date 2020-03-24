FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env

WORKDIR /app

# Copy fsproj and restore as distinct layers
COPY *.fsproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -r linux-musl-x64 -p:PublishSingleFile=true -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1-alpine

COPY --from=build-env /app/out/fsharp-beats /usr/local/bin/
COPY ./entrypoint.sh /usr/local/bin/

RUN apk add --no-cache tini &&\
    chmod +x /usr/local/bin/entrypoint.sh

ENTRYPOINT ["tini", "--", "entrypoint.sh", "fsharp-beats"]
