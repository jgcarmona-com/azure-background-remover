# Nombre del proyecto
$projectName = "BackgroundRemover"

# Ruta al archivo DLL compilado
$dllPath = "$env:USERPROFILE\.dotnet\tools\$projectName\$projectName\bin\Debug\net8.0\$projectName.dll"

# Crear directorios necesarios
New-Item -ItemType Directory -Path "$env:USERPROFILE\.dotnet\tools\$projectName" -Force

# Copiar el proyecto a la ubicación
Copy-Item -Path .\* -Destination "$env:USERPROFILE\.dotnet\tools\$projectName" -Recurse -Force

# Compilar el proyecto
dotnet build "$env:USERPROFILE\.dotnet\tools\$projectName\$projectName\$projectName.csproj" --configuration Debug

# Crear un script ejecutable en una ubicación accesible desde cualquier carpeta
$scriptContent = "dotnet $dllPath `"$args`""
$scriptPath = "$env:USERPROFILE\.dotnet\tools\bgr.cmd"
$scriptContent | Out-File -FilePath $scriptPath -Encoding ASCII

echo "Instalación completada. Ahora puedes usar el comando 'bgr' desde cualquier ubicación."
