# Required Environment Variables:
# - BASE_HREF_BUILD: The base URL for the angular build (e.g., /coastcontracts/). Must include a trailing slash.
# - BASE_HREF_CADDY: The base URL for the caddyfile (e.g., /coastcontracts). Must not include a trailing slash.
# - API_URL: The URL for the API (e.g., http://localhost:5000). Must not include a trailing slash.

# Create build image layer
FROM node:20 AS build

# The base href for the angular build.
ARG BASE_HREF_BUILD=/coastcontracts/
ENV BASE_HREF_BUILD=${BASE_HREF_BUILD}

COPY ./cpu-app/ClientApp ./cpu-app/ClientApp

WORKDIR /cpu-app/ClientApp

RUN npm install

# Build the angular, specify the base href.
# Note: The base href that angular receives must include the trailing slash, in order to serve the static content
# relative to the base href.
RUN npm run build -- --base-href=${BASE_HREF_BUILD}

# Create runtime imag e layer, copy build image contents into this image
FROM caddy:alpine AS final

# The base href for the caddy routing.
ARG BASE_HREF_CADDY=/coastcontracts
# The reverse proxy URL for the API.
ARG API_URL
# Set environment variables for Caddy
ENV BASE_HREF_CADDY=${BASE_HREF_CADDY}
ENV API_URL=${API_URL}

COPY ./cpu-app/ClientApp/Caddyfile /etc/caddy/Caddyfile

COPY --from=build /cpu-app/ClientApp/dist/ /site

EXPOSE 2015
