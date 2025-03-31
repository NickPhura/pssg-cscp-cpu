# Create build image layer
FROM node:8.9.4 AS build

WORKDIR /path/to/angular/project

COPY . .

RUN npm install
RUN npm run build -- --base-href /coastcontracts --configuration production

# Create runtime image layer, copy build image contents into this image
FROM caddy:alpine AS final

COPY ./Caddyfile /etc/caddy/Caddyfile
COPY --from=build /dist/ /site

ENV BASE_URL=
ENV API_URL=

EXPOSE 2015