dim x, y, d

set d = CreateObject("Scripting.Dictionary")
set x = CreateObject("IpPbxSrv.PBXConfig")

set y = x.getuserbyaddress("everyone")

for each myUser in y
	' Is user in the dictionary ?
	If d.Exists(CStr(myUser.userId)) Then
		' Has the state changed ?
		If CInt(d.Item(CStr(myUser.userId))) <> myUser.state Then
			' Update it
			d.Item(CStr(myUser.userId)) = CStr(myUser.state)
		End If
	Else
		' Add it
		d.Add CStr(myUser.userId), CStr(myUser.state)
	End If
next

set y = nothing
set x = nothing
set d = nothing