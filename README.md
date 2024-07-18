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
