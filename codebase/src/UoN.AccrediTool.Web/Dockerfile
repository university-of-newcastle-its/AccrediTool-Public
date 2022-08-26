#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app
COPY . ./
RUN apk add --update nodejs npm
WORKDIR /app/src/UoN.AccrediTool.Web
RUN npm install
WORKDIR /app
RUN dotnet restore -r linux-musl-x64
RUN dotnet publish "src/UoN.AccrediTool.Web/UoN.AccrediTool.Web.csproj" -c Release -o out -r linux-musl-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishSingleFile=true

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
RUN ls -ltr
ENV ASPNETCORE_URLS https://+:44392
ENV ASPNETCORE_HTTPS_PORT 44392
ENV Kestrel__Certificates__Default__Path cert-aspnetcore.pfx
ENV Kestrel__Certificates__Default__Password ufo

EXPOSE 44392
ENTRYPOINT ["./UoN.AccrediTool.Web"]