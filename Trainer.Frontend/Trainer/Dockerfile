FROM node:latest as build
WORKDIR /app
EXPOSE 4200

COPY src ./src
COPY *.json ./

RUN npm install -g @angular/cli@latest
RUN npm install
RUN ng build --configuration production

COPY . /app



FROM nginx:alpine as deploy
WORKDIR /KTCUI
COPY --from=build /app/dist/trainer/*.* /usr/share/nginx/html/
