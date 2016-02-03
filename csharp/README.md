# C# Application

## .NET required 

  * [aspnet](https://get.asp.net/)
  * DNX for .NET Core: `dnvm upgrade -r coreclr`
  * Windows only: 
    * DNX for the .NET Framerowk: `dnvm upgrade -r clr`
  * Mac or Linux:
    * DNX for Mono: `dnvm upgrade -r mono`
  * Show the DNX versions installed: `dnvm list`
  
  
### Compile and Run

```bash
dnu restore
dnu build
dnx web
```