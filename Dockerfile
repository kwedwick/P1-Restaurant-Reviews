#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["P1RestaurantReviewer/P1RestaurantReviewer.csproj", "P1RestaurantReviewer/"]
COPY ["P1RestaurantReviewer.DataAccess/P1RestaurantReviewer.DataAccess.csproj", "P1RestaurantReviewer.DataAccess/"]
COPY ["P1RestaurantReviewer.Domain/P1RestaurantReviewer.Domain.csproj", "P1RestaurantReviewer.Domain/"]
RUN dotnet restore "P1RestaurantReviewer/P1RestaurantReviewer.csproj"
COPY . .
WORKDIR "/src/P1RestaurantReviewer"
RUN dotnet build "P1RestaurantReviewer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P1RestaurantReviewer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
# need to insert the 3 env variables to run the container
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P1RestaurantReviewer.dll"]