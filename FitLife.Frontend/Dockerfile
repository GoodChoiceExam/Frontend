FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY FitLife.Frontend.slnx .
COPY FitLife.Frontend/FitLife.Frontend.csproj FitLife.Frontend/
RUN dotnet restore FitLife.Frontend.slnx

COPY . .
RUN dotnet publish FitLife.Frontend/FitLife.Frontend.csproj -c Release -o /app/publish

FROM nginx:alpine AS runtime
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80