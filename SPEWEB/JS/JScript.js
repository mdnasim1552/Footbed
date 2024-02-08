
            function checkKey()
            {
                if ((window.event.keyCode < 48 || window.event.keyCode > 57) && window.event.keyCode !=46)
                {
                    
                    alert("You have hited Non-numeric : "+String.fromCharCode(window.event.keyCode));
                    window.event.returnValue=false;
                }
            }
            function PhoneOnlyDigit()
            {
                if (window.event.keyCode < 48 || window.event.keyCode > 57)
                {
                    
                    alert("You hit Non-numeric : "+String.fromCharCode(window.event.keyCode));
                    window.event.returnValue=false;
                   
                }
            }
        
     
    