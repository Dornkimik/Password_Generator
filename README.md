# Password Generator

A desktop password generator for Windows and Linux. Choose a password length and the character types to include, then copy the result when you’re ready.

![Password Generator application preview](assets/password-generator-preview.svg)

## Features

- Generate passwords from 4 to 100 characters.
- Choose uppercase letters, lowercase letters, numbers, and symbols independently.
- Include at least one character from every selected group.
- Optionally exclude characters that can be easy to confuse, such as `0`, `O`, `1`, `l`, and `I`.
- Use .NET’s cryptographic random number generator.

## Privacy and security

Password generation happens locally in the app. The app does not save generated passwords or send them to a server. A password is placed on the system clipboard only when you select **Copy**. Clipboard contents may be accessible to other applications running on your computer.

## Run from source

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), clone this repository, and run the app from its directory:

```sh
dotnet run --project src/PasswordGenerator/PasswordGenerator.csproj
```

To build the solution:

```sh
dotnet build Password_Generator.sln
```

## Install

### Linux (x64)

Install the .NET SDK for your distribution. On Omarchy, you can use:

```sh
omarchy pkg add dotnet-sdk
```

Then run the install script from the repository directory:

```sh
./scripts/install-linux.sh
```

The script publishes a self-contained Linux x64 application to `~/.local/opt/password-generator` and adds it to your application launcher.

### Windows (x64)

From PowerShell in the repository directory, publish a standalone application:

```powershell
./scripts/publish-windows.ps1
```

The output is written to `artifacts/windows-x64/PasswordGenerator.exe`.

## Project structure

- `src/PasswordGenerator` — maintained cross-platform Avalonia application
- `scripts` — Linux installation and Windows publishing scripts
- `legacy/windows-wpf` — archived original WPF implementation
