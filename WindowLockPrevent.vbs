set wsc = CreateObject("WScript.Shell")
Do
WScript.Sleep (60*1000)
wsc.SendKeys ("{F15}")
Loop
