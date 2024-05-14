//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Entities.RequestParameters
{
    //Abstarct - Yarim birakilmis class, new'lemek mumkun degil, devranildiginda bu siniftaki butun uyeler ilgili sinifa aktarilir.
    //Kalitilan sinif ise new'lenebilir.
    public abstract class RequestParameters 
    {
        //Search ifadesi butun alanlarda ortak kullanilabilir.
        public String? SearchTerm { get; set; }
    }
}
