##################################################################################################
#               CPU App Multi-Stage Dockerfile (Angular + .NET)                                  #
#                                                                                                #
#  Context:                                                                                      #
#    - Build context must be the repo root: bcgov/pssg-cscp-cpu                                  #
#    - This Dockerfile is located at: cpu-app/Dockerfile                                         #
#                                                                                                #
#  How to Build and Run Locally:                                                                 #
#    1. Build the image (from repo root):                                                        #
#         cd bcgov/pssg-cscp-cpu                                                                 #
#         docker build -t cpu-app:local -f cpu-app/Dockerfile .                                  #
#                                                                                                #
#    2. Run the container:                                                                       #
#         docker run --rm -it -p 8080:8080 cpu-app:local                                         #
#                                                                                                #
#    3. Verify security (inside the running container):                                          #
#         docker exec -it <container_id_or_name> sh                                              #
#         whoami        # should print 'appuser'                                                 #
#         touch /app/testfile  # should succeed                                                  #
#                                                                                                #
#  Security & Best Practice Highlights:                                                          #
#    - Multi-stage build for Angular frontend & .NET backend                                     #
#    - All build steps run as root (default)                                                     #
#    - Runtime container runs as non-root user (appuser)                                         #
#    - All files in /app are owned by appuser:appgroup                                           #
#    - Never run as root in production!                                                          #
#                                                                                                #
##################################################################################################


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

####################################################################
# Security Best Practice:                                          #
#   - Create non-root user and group (appuser:appgroup)            #
#   - all app files/folders are owned by appuser                   #
#   - Only switch to appuser AFTER all files are in place          #
####################################################################

# 1. Create non-root user and group
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# 2. Copy published app from build stage as root
COPY --from=dotnet-build /app/publish ./

# 3. Set ownership of all app files to appuser:appgroup (key for security!)
RUN chown -R appuser:appgroup /app

# 4. Accept build-time arguments and set environment variables
ARG BUILD_ID
ARG BUILD_VERSION
ENV BUILD_ID=${BUILD_ID}
ENV BUILD_VERSION=${BUILD_VERSION}

# 5. Inject version info as appuser-owned files
USER appuser
RUN echo "${BUILD_ID}" > /app/build_id.txt && echo "${BUILD_VERSION}" > /app/version.txt

EXPOSE 8080

ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "cpu-app.dll"]