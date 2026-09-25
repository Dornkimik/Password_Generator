$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$project = Join-Path $repoRoot "src/PasswordGenerator/PasswordGenerator.csproj"
$output = Join-Path $repoRoot "artifacts/windows-x64"

dotnet publish $project `
    --configuration Release `
    --runtime win-x64 `
    --self-contained true `
    --output $output

Write-Host "Published Password Generator to $output"
