# Windows build

The maintained Windows app is the same Avalonia project used on Linux.

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), then run from the repository root:

```powershell
dotnet run --project src/PasswordGenerator/PasswordGenerator.csproj
```

To publish a standalone x64 application:

```powershell
./scripts/publish-windows.ps1
```

The executable is written to `artifacts/windows-x64/PasswordGenerator.exe`.
