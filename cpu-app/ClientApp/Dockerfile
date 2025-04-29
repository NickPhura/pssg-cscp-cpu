# Required Environment Variables:
# - API_URL: The URL for the API (e.g., http://localhost:5000). Must not include a trailing slash.

# Create build image layer
FROM node:20 AS build

COPY ./cpu-app/ClientApp ./cpu-app/ClientApp

WORKDIR /cpu-app/ClientApp

RUN npm install

# Build the angular
RUN npm run build

# Create runtime image layer, copy build image contents into this image
FROM caddy:alpine AS final

# The reverse proxy URL for the API.
ARG API_URL
ENV API_URL=${API_URL}

COPY ./cpu-app/ClientApp/Caddyfile /etc/caddy/Caddyfile

COPY --from=build /cpu-app/ClientApp/dist/ /site

EXPOSE 2015
