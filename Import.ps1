Add-PsSnapin Microsoft.SharePoint.Powershell

$sourceWeb
$destinationWeb
$filename
$library

Import-SPWeb -Identity $destinationWeb -Path $filename -IncludeUserSecurity