@set "sitePath=%~dp0"
 
@echo ÐÂ½¨³ÌÐò³Ø
@C:\Windows\System32\inetsrv\appcmd.exe add apppool /name:"EasyPlat" /managedRuntimeVersion:"v4.0"
@C:\Windows\System32\inetsrv\appcmd.exe stop site "Default Web Site"
@C:\Windows\System32\inetsrv\appcmd.exe add site /name:"EasyPlat" /bindings:http/*:8070: /applicationDefaults.applicationPool:"EasyPlat" /physicalPath:%sitePath%EasyPlat
 
Pause