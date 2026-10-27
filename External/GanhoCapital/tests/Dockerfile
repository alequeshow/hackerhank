FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

# Copy source and test projects from the build context
COPY src/ ./src/
COPY tests/ ./tests/

# Restore dependencies for both test projects
RUN dotnet restore tests/GanhoCapital.Tests/GanhoCapital.Tests.csproj
RUN dotnet restore tests/GanhoCapital.Integration.Tests/GanhoCapital.Integration.Tests.csproj

# Build the source project and test projects
RUN dotnet build src/GanhoCapital/GanhoCapital.csproj --no-restore
RUN dotnet build tests/GanhoCapital.Tests/GanhoCapital.Tests.csproj --no-restore
RUN dotnet build tests/GanhoCapital.Integration.Tests/GanhoCapital.Integration.Tests.csproj --no-restore

# Run tests for all test projects
CMD ["sh", "-c", "dotnet test tests/GanhoCapital.Tests/GanhoCapital.Tests.csproj --no-build --verbosity normal && dotnet test tests/GanhoCapital.Integration.Tests/GanhoCapital.Integration.Tests.csproj --no-build --verbosity normal"]