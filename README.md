# Background Remover CLI

Esta aplicación permite eliminar el fondo de imágenes utilizando el servicio de Azure Computer Vision. Puede ser utilizada desde cualquier carpeta de tu sistema.

## Instalación

### En Ubuntu

1. Clonar el repositorio y navegar al directorio del proyecto:

    ```sh
    git clone <URL_DEL_REPOSITORIO>
    cd <NOMBRE_DEL_REPOSITORIO>
    ```

2. Ejecutar el script de instalación:

    ```sh
    chmod +x install.sh
    ./install.sh
    ```

### En Windows

1. Clonar el repositorio y navegar al directorio del proyecto:

    ```powershell
    git clone <URL_DEL_REPOSITORIO>
    cd <NOMBRE_DEL_REPOSITORIO>
    ```

2. Ejecutar el script de instalación:

    ```powershell
    .\install.ps1
    ```

## Uso

Una vez instalado, puedes usar el comando `bgr` desde cualquier carpeta.

### Procesar una imagen

```sh
bgr -i <ruta_a_la_imagen>
```	
### Procesar todas las imágenes en una carpeta
```sh
bgr -f <ruta_a_la_carpeta>
```
## Configuración
Asegúrate de tener un archivo appsettings.json con tus credenciales de Azure en el directorio de instalación del proyecto. El contenido del archivo debe ser similar a este:

```json
{
  "Azure": {
    "Endpoint": "YOUR_AZURE_ENDPOINT",
    "Key": "YOUR_AZURE_KEY"
  }
}
```

### Paso 3: Configurar el proyecto para la distribución

Asegúrate de que tu proyecto está organizado correctamente y que el archivo `appsettings.json` está presente en el directorio del proyecto.

### Paso 4: Probar la instalación

Antes de compartir el script, prueba la instalación en tu entorno para asegurarte de que todo funciona correctamente. Ejecuta los scripts de instalación en Ubuntu y Windows y verifica que el comando `bgr` funcione como se espera.

### Paso 5: Compartir el repositorio

Finalmente, comparte el repositorio con los scripts de instalación para que otros usuarios puedan instalar y utilizar la aplicación fácilmente desde cualquier carpeta en su sistema.

Con estos pasos, tendrás una aplicación CLI que se puede instalar y utilizar en Ubuntu y Windows desde cualquier carpeta.