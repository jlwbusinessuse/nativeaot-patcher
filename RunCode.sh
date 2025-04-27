dotnet restore ./tests/Cosmos.Patcher.Tests/Cosmos.Patcher.Tests.csproj

dotnet build ./tests/Cosmos.Patcher.Tests/Cosmos.Patcher.Tests.csproj --configuration Debug --no-restore

dotnet test ./tests/Cosmos.Patcher.Tests/Cosmos.Patcher.Tests.csproj --no-build --configuration Debug --logger trx;LogFileName=Cosmos.Patcher.Tests.trx