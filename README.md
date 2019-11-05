# ingenico-pos-dotnet-lib
.netstandard2.0 Class library for communication with Asseco Group Ingenico POS Device written in C#
## Info
* Developed in Visual Studio 2019
* C# netstandard2.0
* Tested on Ingenico ict-220 **Probably works on other models**

## Usage
### Donwloading the package from NuGet
### Manually downloading with dependencies

## Development
1. Clone the repository with
```
git clone https://github.com/F4pl0/ingenico-pos-dotnet-lib.git
```
2. Open in Visual Studio 2019 (*To eliminate compatibility issues*)
3. Get Dependent NuGet Packages (If they aren't already in the project)  

**Happy Development**

### Dependencies
* NuGet **MSTest.TestAdapter** *Tests only*
* NuGet **MSTest.TestFramework** *Tests only*
* NuGet **System.IO.Ports** *Required for library*

## Tests
1. **Test01Connection**  
Tests the connectivity with the device on the *PORT* port.
2. **Test02Sale**  
Upon connecting, tests the `POS.Sale( Int64 Amount )` Which returns `SaleResponse`.  
The main test is to check whether the `SaleResponse.Success` is `true`

