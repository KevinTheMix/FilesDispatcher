# FilesDispatcher

## Changelog

* 2022.10.05: added Jump() immediate volume control method
* 2022.09.23
  * Main window controls colour
  * set Volume
  * get CurrentFileName
* 2022.08.14
  * added a .NET Core 6.0 WebAPI
    * [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) requires Visual Studio 2022, because [Visual Studio 2019 does not support it](https://github.com/AvaloniaUI/Avalonia/discussions/7025) (except using preview SDK option)
  * manually edited the WebAPI's _.csproj_ to target `net6.0-windows` instead of default `net6.0` to fix compatibility issues
	* see [Target frameworks in SDK-style projects](https://docs.microsoft.com/en-us/dotnet/standard/frameworks)
