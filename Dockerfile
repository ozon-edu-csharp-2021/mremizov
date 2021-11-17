#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/src/OzonEdu.MerchandiseApi/OzonEdu.MerchandiseApi.csproj", "OzonEdu.MerchandiseApi/"]
RUN dotnet restore "OzonEdu.MerchandiseApi/OzonEdu.MerchandiseApi.csproj"
COPY /src/. .
WORKDIR "/src/OzonEdu.MerchandiseApi"
RUN dotnet build "OzonEdu.MerchandiseApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchandiseApi.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh