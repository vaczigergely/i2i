FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY l2l.sln ./l2l.sln
COPY l2l.Data/l2l.Data.csproj ./l2l.Data/l2l.Data.csproj
COPY l2l.Data.Tests/l2l.Data.Tests.csproj ./l2l.Data.Tests/l2l.Data.Tests.csproj
RUN dotnet restore

COPY . ./
RUN dotnet test -v n