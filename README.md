# ingenico-pos-dotnet-lib
.netstandard2.0 Class library for communication with Asseco Group Ingenico POS Device written in C#
## Info
* Developed in Visual Studio 2019
* C# netstandard2.0
* Tested on Ingenico ict-220 **Probably works on other models**

## Download
### Donwloading the package from NuGet
You can download the IngenicoPOS Package form NuGet, which will automatically download all of the necessary dependencies, or you can install it with:
* Package Manager
```
Install-Package IngenicoPOS -Version 1.0.1
```
* .NET CLI
```
dotnet add package IngenicoPOS --version 1.0.1
```
* Paket CLI
```
paket add IngenicoPOS --version 1.0.1
```
or by adding PackageReference:
```xml
<PackageReference Include="IngenicoPOS" Version="1.0.1" />
```

### Manually downloading the assembly
1. Download the latest release IngenicoPOS.dll from [IngenicoPOS Releases](https://github.com/F4pl0/ingenico-pos-dotnet-lib/releases)
2. Put the IngenicoPOS.dll to your project dependencies
3. Add the IngenicoPOS.dll as a project dependency  

**NOTE: Make sure the project also depends on System.IO.Ports**

## Usage
### Import
To use the available classes, you will need to import them into the project (unless you really want to type IngenicoPOS. every single time):
```csharp
using IngenicoPOS;
```
### Connection
To Connect with the POS device, we will initialize POS class and connect to it
```csharp
const string PORT = "COM9";  // Port of the POS Device (COM0 is just an example)

POS posDevice = new POS(PORT);
posDevice.Connect();  // Will return true if connection is made, otherwise will return false

// To check whether the posDevice is connected, we can do it with
if ( posDevice.IsConnected ) {
    // The device is connected
} else {
    // Nope :/
}
```
### Sale
To charge a card, we can use the `POS.Sale( Amount )`:
```csharp
const string PORT = "COM9";  // Port of the POS Device (COM0 is just an example)

POS posDevice = new POS(PORT);

if ( posDevice.Connect() ) {
    // We will charge the card for 100,00
    SaleResponse res = posDevice.Sale(10000);

    if ( res.Success ) {
        // Sale was successful :D
    } else{
        // Sale wasn't successful :(
    }
} else {
    // Device not connected :/
}
```

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

### Tests
1. **Test01Connection**  
Tests the connectivity with the device on the *PORT* port.
2. **Test02Sale**  
Upon connecting, tests the `POS.Sale( Int64 Amount )` Which returns `SaleResponse`.  
The main test is to check whether the `SaleResponse.Success` is `true`

## Classes
**NOTE: Only public variables/functions are listed**

### POS
This is the main class of the library.
#### Constructors
  Constructor    |   Description
 -------------   | -------------
POS (string PORT, int baudRate = 115200) | Default constructor that requires COM Port of the Device Serial and possibly baud rate

#### Variables
   Variable  |      Type     |   Description
------------ | ------------- | -------------
IsConnected       | bool   | Boolean whether the device is connected
NextTransactionNo | int    | Number of next transaction
POSPrints         | bool   | Boolean whether the POS device should print the reciept
CurrencyISO       | int    | ISO Code of the currency
CashierID         | int    | Cashier ID
Language          | string | Language from `Consts.Language`

#### Functions
   Function  |  Return Type  |   Description
------------ | ------------- | -------------
Connect()           | bool         | Function to connect to the POS Device
Sale( Int64 Amount) | SaleResponse | Function to charge the card

### Consts
Class with constants prior to the communication.
#### Subclasses / Constant sets
  Sublcass    |   Description
 -------------   | -------------
TransactionType | Strings for different transaction types
TransactionFlag | Response types from the POS
Language       | Different languages for the POS
CardDataSource | Differet Card payment methods

### Utils
Static Functions for the core functionality of the library.

#### Functions
   Function  |  Return Type  |   Description
------------ | ------------- | -------------
BuildMessage( ECRMessage msg ) | string   | Builds ready-to-send string from ECRMessage

### ECRMessage
This class represents the message that is sent to the POS ( from ECR/PC.. )
#### Constructors
  Constructor    |   Description
 -------------   | -------------
ECRMessage () | Default constructor

#### Variables
   Variable  |      Type     |   Description
------------ | ------------- | -------------
TerminalID        | int   | Terminal ID (Self-Explanatory)
NextTransactionNo | int   | Next transaction number (Self-Explanatory)
CashierID         | int   | Cashier ID (Self-Explanatory)
CurrencyISO       | int   | 3-Digit ISO Code of the currency
TransactionAmount | Int64 | Amount of money to charge the card
TransactionAmountCash | Int64 | Part of the transaction that is charged by the card
TransactionType  | string | Type of transaction (from `Consts.TransactionType`)
AuthorizationCode| string | Authorization code for Offline Sale
InputLabel       | string | Label for Input Data
InsurancePolicyNumber | string | Insurance policy number which may be transferred to the host and printed on the receipt
InstallmentsNumber| string | Number of installments
LanguageID       | string | Language (from `Consts.Language`)
PrintData        | string | Data that should be printed on receipt. Data could be delimited by Carriage Return or Form Feed
PayservicesData  | string | TLV based data (Product code, service value,...)
TransactionActivationCode  | string | Transaction Activation Code for Mobile Payment. Regular purchase request shall be sent with TAC value to perform Mobile Payment InstantPaymentRef| string | Reference number for Instant Payment – mandatory for Instant Payment Refund (same value as in response on Instant Payment Purchase or Inquiry)
QRCodeData       | string | QR code data for Instant Payment (Pull mode).
POSPrints        | bool   | Flag whether the POS should print the reciept

### POSMessage
This class represents the message that POS device sends
#### Constructors
  Constructor    |   Description
 -------------   | -------------
POSMessage () | Default constructor
POSMessage (string message) | Constructor for automatic string parsing

#### Variables
   Variable  |      Type     |   Description
------------ | ------------- | -------------
TransactionFlag    | string   | Status of the transaction (from `Consts.TransactionFlag`)
TransactionType    | string   | Value from the request message
TransactionAmount  | Int64    | Value from the request message
TransactionDate    | string   | Date of the transaction (DDMMYY)
TransactionTime    | string   | Time of the transaction (HHMMSS)
CardDataSource     | string   | Card Payment method (from `Consts.CardDataSource`)
AuthorizationCode  | string   | Authorization code ( if any )

#### Functions
   Function  |  Return Type  |   Description
------------ | ------------- | -------------
ParseAssign(string msg) | void | Parses the string and assignes the values to the object

### SaleResult
`POS.Sale()` returns this class as a result to the transaction.
#### Constructors
  Constructor    |   Description
 -------------   | -------------
SaleResult(bool Success, POSMessage message) | Constructor for assigning all of the variables

#### Variables
   Variable  |      Type     |   Description
------------ | ------------- | -------------
Success | bool  | True if the transaction was successful, otherwise false
Message | POSMessage  | The message that POS last sent. Used for inspecting the cause of the failed/successful transaction etc...