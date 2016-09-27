Add-PsSnapin Microsoft.SharePoint.Powershell

$sourceWeb
$destinationWeb
$filename
$library

Export-SPWeb -Identity $sourceWeb -Path $filename -ItemUrl $library -IncludeUserSecurity -IncludeVersions All -Force


