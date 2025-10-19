FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM node:18 AS frontend
WORKDIR /src
COPY FrontEnd/package*.json ./FrontEnd/
WORKDIR /src/FrontEnd
RUN npm install
COPY FrontEnd/ ./
RUN npm run build --configuration=production

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BackEnd/ ./BackEnd/
COPY --from=frontend /src/BackEnd/wwwroot/ ./BackEnd/wwwroot/
WORKDIR /src/BackEnd
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
RUN ls -la wwwroot/
ENTRYPOINT ["dotnet", "GroceryStoreAPI.dll"]