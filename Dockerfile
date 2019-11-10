FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy fsproj and restore as distinct layers
COPY *.fsproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .

COPY ./entrypoint.sh /entrypoint.sh
ENV TINI_VERSION v0.18.0
ADD https://github.com/krallin/tini/releases/download/${TINI_VERSION}/tini /tini
RUN chmod +x /tini /entrypoint.sh

ENTRYPOINT ["/tini", "--", "/entrypoint.sh", "dotnet", "fsharp-beats.dll"]
