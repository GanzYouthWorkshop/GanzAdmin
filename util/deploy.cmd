xcopy ..\src\GanzAdmin\bin\Debug\net6.0 C:\WEB\GanzAdmin /S /I /E /EXCLUDE:filter_file.txt
xcopy ..\src\GanzAdmin\wwwroot C:\WEB\GanzAdmin\wwwroot /S /I /E /EXCLUDE:filter_file.txt

xcopy ..\src\GanzWebpage\bin\Debug\net6.0 C:\WEB\GanzWeb /S /I /E /EXCLUDE:filter_file.txt
xcopy ..\src\GanzWebpage\wwwroot C:\WEB\GanzWeb\wwwroot /S /I /E /EXCLUDE:filter_file.txt

pause