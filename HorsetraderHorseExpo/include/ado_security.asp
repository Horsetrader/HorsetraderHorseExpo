<%

' Identify sql injection code 
function hasSuspiciousCode(strInput)

    sqlBlackList = Array("--", ";", "/*", "*/", "@@", "@","=",_
                  "char(", "nchar", "varchar", "nvarchar",_
                  "alter", "begin", "cast", "create", "cursor",_
                  "declare", "delete", "drop", "end", "exec",_
                  "execute", "fetch", "insert", "kill", "open",_
                  "select", "sys", "sysobjects", "syscolumns",_
                  "table", "update")

    result = false
    
    strInput = trim(strInput)
    strInput = lcase(strInput)
    
    For Each s in sqlBlackList
        If ( InStr (strInput, s) <> 0 ) Then
          hasSuspiciousCode = true
          Exit Function
        End If
    Next
    
    hasSuspiciousCode = result

end function

function isValidEmail(strInput)

  dim isValidE
  dim regEx
  
  isValidE = True
  set regEx = New RegExp
  
  regEx.IgnoreCase = False
  
  regEx.Pattern = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
  isValidE = regEx.Test(strInput)
  
  isValidEmail = isValidE

end function



function filterInput(strInput)

    if hasSuspiciousCode(strInput) then
        'response.Write("<br/><br/>DEBUG: found suspicious code: " & strInput)
        strInput = ""
    end if

    filterInput = strInput

end function

function getUrlParameter(strParameterName)
    
    o_value = Request.QueryString(strParameterName)
    o_value = filterInput(o_value)

    getUrlParameter = o_value

end function

function getEmailParameter(strParameterName)
    
    o_value = request.QueryString(strParameterName)
    
    if not isValidEmail(o_value) then
        o_value = ""
    end if

    getEmailParameter = o_value

end function


 %>