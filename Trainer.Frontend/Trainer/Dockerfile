FROM node:latest as build
WORKDIR /usr/src/app
EXPOSE 4200

COPY package.json package-lock.json ./

RUN npm install -g @angular/cli@latest
RUN npm install
COPY . .
RUN ng build --configuration production

FROM nginx:alpine as deploy
COPY --from=build /usr/src/app/dist/trainer/*.* /usr/share/nginx/html/
COPY ./config/nginx.conf /etc/nginx/nginx.conf

