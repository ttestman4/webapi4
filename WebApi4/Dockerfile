#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1903 AS build
WORKDIR /src
COPY ["WebApi4/WebApi4.csproj", "WebApi4/"]
RUN dotnet restore "WebApi4/WebApi4.csproj"
COPY . .
WORKDIR "/src/WebApi4"
RUN dotnet build "WebApi4.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApi4.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi4.dll"]