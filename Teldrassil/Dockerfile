#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#该文件用于创新最终Docker映像的方案，例如启用https，就配置 EXPOSE 443

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Teldrassil/Teldrassil.csproj", "Teldrassil/"]
RUN dotnet restore "Teldrassil/Teldrassil.csproj"
COPY . .
WORKDIR "/src/Teldrassil"
RUN dotnet build "Teldrassil.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teldrassil.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teldrassil.dll"]
