#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS base
WORKDIR /app
COPY . .
RUN dotnet test

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1803 AS build
WORKDIR /src
COPY ["UnitTest/UnitTest.csproj", "UnitTest/"]
COPY ["WebApplicationCore/Core.csproj", "WebApplicationCore/"]
COPY ["WebApiCore3/WebApiCore3.csproj", "WebApiCore3/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "UnitTest/UnitTest.csproj"
COPY . .
WORKDIR "/src/UnitTest"
RUN dotnet build "UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitTest.dll"]