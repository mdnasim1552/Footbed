using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEENTITY.C_34_Mgt
{
    [Serializable]
    public class EntityClassCurrency
    {
        public string code { get; set; }
        public string codedesc { get; set; }
        public string curdesc { get; set; }
        public string cursymbol { get; set; }
        public string curword { get; set; }
        public string curstatus { get; set; } 

        public EntityClassCurrency(string code, string codedesc, string curdesc, string cursymbol, string curword, string curstatus) 
        {
            this.code = code;
            this.codedesc = codedesc;
             this.curdesc = curdesc;
             this.cursymbol = cursymbol;
             this.curword = curword;
             this.curstatus = curstatus;
            
        
        }

    }
}
