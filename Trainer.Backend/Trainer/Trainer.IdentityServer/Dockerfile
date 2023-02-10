#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Trainer.IdentityServer/Trainer.IdentityServer.csproj", "Trainer.IdentityServer/"]
COPY ["Trainer.Persistence/Trainer.Persistence.csproj", "Trainer.Persistence/"]
COPY ["Trainer.Application/Trainer.Application.csproj", "Trainer.Application/"]
COPY ["Trainer.Common/Trainer.Common.csproj", "Trainer.Common/"]
COPY ["Trainer.Domain/Trainer.Domain.csproj", "Trainer.Domain/"]
COPY ["Trainer.Enums/Trainer.Enums.csproj", "Trainer.Enums/"]
COPY ["Trainer.Settings/Trainer.Settings.csproj", "Trainer.Settings/"]
RUN dotnet restore "Trainer.IdentityServer/Trainer.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Trainer.IdentityServer"
RUN dotnet build "Trainer.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trainer.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trainer.IdentityServer.dll"]