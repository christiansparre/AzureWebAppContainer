FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AzureWebAppContainer/AzureWebAppContainer.csproj", "AzureWebAppContainer/"]
RUN dotnet restore "AzureWebAppContainer/AzureWebAppContainer.csproj"
COPY . .
WORKDIR "/src/AzureWebAppContainer"
RUN dotnet build "AzureWebAppContainer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AzureWebAppContainer.csproj" -c Release -o /app
COPY README.md version.* /app/

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AzureWebAppContainer.dll"]