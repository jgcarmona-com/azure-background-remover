
# BackgroundRemover

## Description

BackgroundRemover is a command-line tool to remove the background from images using Azure AI services. This tool can process individual images or all images in a specified folder. It is designed to be installed and used on both Windows and Linux systems.

## Features

- Remove background from individual images.
- Batch process images in a folder.
- Cross-platform support (Windows and Linux).

## Prerequisites

- .NET 8.0 SDK
- Azure AI Service credentials

## Installation

### Windows

1. Clone the repository:

    ```sh
    git clone <REPOSITORY_URL>
    cd <REPOSITORY_NAME>
    ```

2. Navigate to the `scripts` folder:

    ```sh
    cd scripts
    ```

3. Run the installation script:

    ```sh
    .\install.ps1
    ```

4. Restart your terminal to apply the changes to the PATH.

### Linux

1. Clone the repository:

    ```sh
    git clone <REPOSITORY_URL>
    cd <REPOSITORY_NAME>
    ```

2. Navigate to the `scripts` folder:

    ```sh
    cd scripts
    ```

3. Make the installation script executable and run it:

    ```sh
    chmod +x install.sh
    ./install.sh
    ```

4. Restart your terminal to apply the changes to the PATH.

## Configuration

Before using the tool, you need to configure your Azure AI Service credentials. Update the `appsettings.json` file in the root of the project with your Azure endpoint and key:

```json
{
    "Azure": {
        "Endpoint": "YOUR_AZURE_ENDPOINT",
        "Key": "YOUR_AZURE_KEY"
    }
}
```

## Usage

### Individual Image

To remove the background from a single image, use the `-i` or `--image` option followed by the path to the image:

```sh
bgr -i <image_path>
```

### Folder

To remove the background from all images in a folder, use the `-f` or `--folder` option followed by the path to the folder:

```sh
bgr -f <folder_path>
```

## Example

### Windows

```sh
bgr -i images\me.jpg
```

```sh
bgr -f images
```

### Linux

```sh
bgr -i images/me.jpg
```

```sh
bgr -f images
```

## Troubleshooting

### Common Issues

- **FileNotFoundException**: Ensure that the `appsettings.json` file is in the same directory as the `BackgroundRemover.dll`.
- **UnauthorizedAccessException**: Ensure you have the necessary permissions to access the files and directories being processed.
- **Configuration Errors**: Double-check your Azure endpoint and key in the `appsettings.json` file.

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.
