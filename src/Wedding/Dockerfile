#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Wedding/Wedding.csproj", "Wedding/"]
RUN dotnet restore "Wedding/Wedding.csproj"
COPY . .
WORKDIR "/src/Wedding"
RUN dotnet build "Wedding.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wedding.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apk update \
    && apk --no-cache add icu-libs fontconfig ttf-dejavu
ENV DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wedding.dll"]