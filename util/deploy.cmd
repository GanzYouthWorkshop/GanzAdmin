xcopy ..\src\GanzAdmin\bin\Debug\netcoreapp3.1 C:\WEB\GanzAdmin /S /I /E /EXCLUDE:filter_file.txt
xcopy ..\src\GanzAdmin\wwwroot C:\WEB\GanzAdmin\wwwroot /S /I /E /EXCLUDE:filter_file.txt
pause