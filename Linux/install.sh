#!/usr/bin/env bash
set -euo pipefail

script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
project="$script_dir/PasswordGenerator/PasswordGenerator.csproj"
install_dir="$HOME/.local/opt/password-generator"
applications_dir="$HOME/.local/share/applications"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "Install the .NET SDK before building this app." >&2
  exit 1
fi

mkdir -p "$install_dir" "$applications_dir"
dotnet publish "$project" --configuration Release --runtime linux-x64 \
  --self-contained true --output "$install_dir"

cat > "$applications_dir/password-generator.desktop" <<EOF
[Desktop Entry]
Type=Application
Name=Password Generator
Comment=Generate and copy a random password
Exec=$install_dir/PasswordGenerator
Icon=dialog-password
Terminal=false
StartupWMClass=PasswordGenerator
Categories=Utility;
EOF

chmod 755 "$applications_dir/password-generator.desktop"
echo "Installed Password Generator. Find it in the app launcher."
