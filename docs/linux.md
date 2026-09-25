# Linux install notes

The maintained app is written in C# with Avalonia and runs on both Linux and Windows. The Linux install script publishes a self-contained x86-64 application, so the .NET runtime is not needed after installation.

## Install on Omarchy / Arch Linux

Install the .NET SDK with `omarchy pkg add dotnet-sdk`, then run:

```sh
./scripts/install-linux.sh
```

The app is installed in `~/.local/opt/password-generator` and appears in the desktop app launcher. Re-run the install script after pulling source changes to update it.

The app generates 1–100 character passwords from uppercase letters, lowercase letters, and digits using a cryptographically secure random generator. It only places a password on the clipboard when you press **Copy**.
