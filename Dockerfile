# ----- Build Stage -----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /build

# Copy project files for restore
COPY ["src/AutoFix.Api/AutoFix.Api.csproj", "src/AutoFix.Api/"]
COPY ["src/AutoFix.Client/AutoFix.Client.csproj", "src/AutoFix.Client/"]
COPY ["src/AutoFix.Application/AutoFix.Application.csproj", "src/AutoFix.Application/"]
COPY ["src/AutoFix.Domain/AutoFix.Domain.csproj", "src/AutoFix.Domain/"]
COPY ["src/AutoFix.Contracts/AutoFix.Contracts.csproj", "src/AutoFix.Contracts/"]
COPY ["src/AutoFix.Infrastructure/AutoFix.Infrastructure.csproj", "src/AutoFix.Infrastructure/"]
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]

# Restore dependencies (only once)
RUN dotnet restore "src/AutoFix.Api/AutoFix.Api.csproj"

# Copy all source code
COPY . .

# Build and publish
RUN dotnet publish "src/AutoFix.Api/AutoFix.Api.csproj" -c Release -o /app

# ----- Final Stage -----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

# Install timezone data for TimeZoneInfo support
RUN apt-get update && apt-get install -y tzdata && \
    ln -fs /usr/share/zoneinfo/America/Montreal /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    rm -rf /var/lib/apt/lists/*

ENV TZ=America/Montreal

WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "AutoFix.Api.dll"]