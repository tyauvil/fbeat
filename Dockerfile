FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app

# Copy fsproj and restore as distinct layers
COPY fsharp-beats.fsproj ./
RUN dotnet restore fsharp-beats.fsproj

# Copy everything else and build
COPY . ./
RUN dotnet publish fsharp-beats.fsproj -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-buster-slim

ENV TINI_SUBREAPER=1

COPY --from=build-env /app/out/fsharp-beats /usr/local/bin/
COPY ./entrypoint.sh /usr/local/bin/

ENTRYPOINT ["entrypoint.sh"]
CMD ["fsharp-beats"]
