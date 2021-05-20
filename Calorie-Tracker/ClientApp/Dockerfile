# Stage 1: Compile and Build angular codebase

# Use official node image as the base image
FROM node:latest as build

# Set the working directory
WORKDIR /ClientApp

# Add the source code to app
COPY . .

# Install all the dependencies
RUN npm install -g npm@7.13.0

# Generate the build of the application
RUN npm run build

# Stage 2: Serve app with nginx server

# Use official nginx image as the base image
FROM nginx:latest

# Copy the build output to replace the default nginx contents.
COPY --from=build /ClientApp/dist/ClientApp /usr/share/nginx/html
