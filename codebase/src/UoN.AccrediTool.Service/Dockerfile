#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore -r linux-musl-x64
RUN dotnet publish "src/UoN.AccrediTool.Service/UoN.AccrediTool.Service.csproj" -c Release -o out -r linux-musl-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishSingleFile=true

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine-amd64
WORKDIR /app
COPY --from=build-env /app/out .
RUN ls -ltr
ENV ASPNETCORE_URLS https://+:53925;http://+:53924
ENV ASPNETCORE_HTTPS_PORT 53925
ENV Kestrel__Certificates__Default__Path cert-aspnetcore.pfx
ENV Kestrel__Certificates__Default__Password ufo

EXPOSE 53924
EXPOSE 53925
ENTRYPOINT ["./UoN.AccrediTool.Service"]