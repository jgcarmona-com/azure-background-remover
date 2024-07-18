#!/bin/bash

# Nombre del proyecto
PROJECT_NAME="BackgroundRemover"

# Ruta al archivo DLL compilado
DLL_PATH="$HOME/.local/share/$PROJECT_NAME/$PROJECT_NAME/bin/Debug/net8.0/$PROJECT_NAME.dll"

# Crear directorios necesarios
mkdir -p "$HOME/.local/share/$PROJECT_NAME"

# Copiar el proyecto a la ubicación
cp -r . "$HOME/.local/share/$PROJECT_NAME"

# Compilar el proyecto
dotnet build "$HOME/.local/share/$PROJECT_NAME/$PROJECT_NAME/$PROJECT_NAME.csproj" --configuration Debug

# Crear un script ejecutable en /usr/local/bin
echo "#!/bin/bash
dotnet $DLL_PATH \"\$@\"" | sudo tee /usr/local/bin/bgr > /dev/null

# Dar permisos de ejecución al script
sudo chmod +x /usr/local/bin/bgr

echo "Instalación completada. Ahora puedes usar el comando 'bgr' desde cualquier ubicación."
