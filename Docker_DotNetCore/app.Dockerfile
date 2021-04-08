# Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

# install Node.js
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
# need gulp as a globally available cli tool
RUN npm -g install gulp

# install packages in app and cache until package json changed
# node_modules is generated under app folder
WORKDIR /app
COPY package.json ./package.json
RUN npm install

# call dotnet to restore package
COPY *.csproj ./
RUN dotnet restore

# copy project and build our app
COPY . ./
RUN dotnet publish AspNetcore.csproj -c Release -o out --no-restore

# build the ts js lib (which is simply copying the node_modules in the wwwroot/lib)
RUN gulp -f gulpfile.js min 
RUN cp -R ./wwwroot/ /out/wwwroot/


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app

COPY --from=build-env /app/out .

# start the app
ENTRYPOINT ["dotnet","AspNetCore.dll"] 