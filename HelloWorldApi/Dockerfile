# TODO ESTO ES PARA HACER LA IMAGEN 

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV MINOMRE_ES=Gustavo

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["HelloWorldApi.csproj","./"]
RUN dotnet restore "HelloWorldApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HelloWorldApi.csproj" -c ${configuration} -o /app/build

RUN ls

FROM build AS publish
ARG configuration=Realese
RUN dotnet publish "HelloWorldApi.csproj" -c ${configuration} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "HelloWorldApi.dll" ]

