
function IsDecimal(eid)
 {
   try
   {   //||([0-9]+\.[0-9]+$))
   var a=5;
  
       var decimalval=document.getElementById(eid).value;
       var regex=new RegExp("^([0-9]+(\.[0-9]+)*$)"); 
 
       if(decimalval!="")
       {
           if(!regex.test(decimalval))
            {
             alert("Please Insert Actual Integer Value");
             return false;   
            } 
          return true;
       }
       alert("Please Fill up the fieled value");
       return false;
 }
 
 catch(e)
 {
  alert("Error: " +e);
  return false; 
  }
 
}  