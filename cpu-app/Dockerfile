# ======================
# Angular  Build Stage
# ======================
FROM node:20 AS angular-build

ARG BUILD_ID
ARG BUILD_VERSION

WORKDIR /app

COPY ./cpu-app/ClientApp/ ./cpu-app/ClientApp/

WORKDIR /app/cpu-app/ClientApp

RUN npm install
RUN npm run build -- --output-path=dist

# ======================
# .NET Build Stage
# ======================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

ARG BUILD_ID
ARG BUILD_VERSION

WORKDIR /src

COPY . .

# Copy angular dist into the dotnet app
COPY --from=angular-build /app/cpu-app/ClientApp/dist/ ./cpu-app/wwwroot/

RUN dotnet publish cpu-app/cpu-app.csproj -c Release -o /app/publish

# ======================
# Runtime Image
# ======================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Accept build-time arguments
ARG BUILD_ID
ARG BUILD_VERSION

# Export to environment variables
ENV BUILD_ID=${BUILD_ID}
ENV BUILD_VERSION=${BUILD_VERSION}


COPY --from=dotnet-build /app/publish ./

# Inject version info
RUN echo "${BUILD_ID}" > /app/build_id.txt && echo "${BUILD_VERSION}" > /app/version.txt

EXPOSE 8080

ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "cpu-app.dll"]