function Get-Aspnetwp([string]$name="*")
{
   $processList = Get-Process w3wp -ErrorAction SilentlyContinue;

   if($processList -eq $null) {
      Write-Host "Process with the 'w3wp' name was not found." -ForegroundColor Red;   
   }

   foreach($process in $processList) { 
      $filter = "Handle='" + $process.Id + "'"
      $wmip = Get-WmiObject Win32_Process -filter $filter 

      if($wmip.CommandLine -match "-ap `"(\w+)`"") {
         $appName = $matches[1]
         $process | Add-Member NoteProperty AppPoolName $appName
      }
   }

   $processList | where { $_.AppPoolName -like $name }
}

$process = Get-Aspnetwp("DefaultAppPool")
Write-Host "Process found, name:"$process.Name "pid:"$process.Id  -ForegroundColor White
Write-Host -ForegroundColor Blue

$processId = $process.Id

& "c:\Dropbox\FrontRange\Work\Powershell\AttachToProcess\AttachVsToProcess.exe" /processId:$processId