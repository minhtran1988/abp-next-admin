FROM nginx:latest
LABEL maintainer="colin.in@foxmail.com"

# Copy the contents of the dist directory after vue is built to the deployment directory of nginx default configuration in the docker container
COPY ./dist /usr/share/nginx/html
 
# Replace the default configuration in the docker container with the local configuration
COPY ./docker/nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./docker/nginx/default.conf /etc/nginx/conf.d/default.conf

# port mapping
EXPOSE 80
