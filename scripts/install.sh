#!/bin/bash

# Project name
PROJECT_NAME="BackgroundRemover"

# Base directory for the installation
INSTALL_DIR="/usr/local/share/$PROJECT_NAME"

# Create necessary directories
sudo mkdir -p "$INSTALL_DIR"

# Compile the project
sudo dotnet publish "../BackgroundRemover/BackgroundRemover.csproj" --configuration Debug --output "$INSTALL_DIR"

# Copy appsettings.json to the installation directory if it exists
if [ -f "../appsettings.json" ]; then
    sudo cp "../appsettings.json" "$INSTALL_DIR"
fi

# Create an executable script in /usr/local/bin
echo "#!/bin/bash
dotnet $INSTALL_DIR/$PROJECT_NAME.dll \"\$@\"" | sudo tee /usr/local/bin/bgr > /dev/null

# Make the script executable
sudo chmod +x /usr/local/bin/bgr

echo "Installation completed. You can now use the 'bgr' command from any location."
