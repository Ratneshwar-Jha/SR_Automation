dim oFSO
set oFSO = CreateObject("Scripting.FileSystemObject")
currentDirectory = oFSO.GetAbsolutePathName(".")
set oFSO = nothing

dim oWScript
set oWScript = CreateObject("WScript.Shell")
oWScript.Run(currentDirectory & "\bin\Debug\BTIS_CodedUI_Automation.exe")
set oWScript = nothing