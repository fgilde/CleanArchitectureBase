$search = "CleanArchitectureBase";
$replace = "CleanArchitectureBase";

gci -r -include "*.*","*.config","*.cfg" |
 foreach-object { $a = $_.fullname; ( get-content $a ) |
 foreach-object { $_ -replace $search, $replace }  | 
set-content $a }
