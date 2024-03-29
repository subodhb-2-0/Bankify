#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PublicAPI/PublicAPI.csproj", "PublicAPI/"]
COPY ["Logger/Logger.csproj", "Logger/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Services.Abstractions/Services.Abstractions.csproj", "Services.Abstractions/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "PublicAPI/PublicAPI.csproj"
COPY . .
WORKDIR "/src/PublicAPI"
RUN dotnet build "PublicAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublicAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublicAPI.dll"]