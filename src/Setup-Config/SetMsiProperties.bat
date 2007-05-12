echo Executing SQL command on PaintDotNet.msi: UPDATE Property SET Property.Value = 'ALL' WHERE Property.Property = 'FolderForm_AllUsers'
wscript //B wirunsql.vbs ""%1"" "UPDATE Property SET Property.Value = 'ALL' WHERE Property.Property = 'FolderForm_AllUsers'"
wscript //B wirunsql.vbs ""%1"" "DELETE FROM Property WHERE Property='ALLUSERS'"
wscript //B wirunsql.vbs ""%1"" "INSERT INTO Property (Property, Value) VALUES('ALLUSERS', '2')
wscript //B wirunsql.vbs ""%1"" "DELETE FROM Property WHERE Property='ARPNOREPAIR'"
wscript //B wirunsql.vbs ""%1"" "INSERT INTO Property (Property, Value) VALUES('ARPNOREPAIR', '1')