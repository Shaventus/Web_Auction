//------------------------------------------------------------------------------
// <auto-generated>
//    Ten kod �r�d�owy zosta� wygenerowany na podstawie szablonu.
//
//    R�czne modyfikacje tego pliku mog� spowodowa� nieoczekiwane zachowanie aplikacji.
//    R�czne modyfikacje tego pliku zostan� zast�pione w przypadku ponownego wygenerowania kodu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Auctions.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sell
    {
        public int account_idAccount { get; set; }
        public int item_iditem { get; set; }
        public string date { get; set; }
    
        public virtual account account { get; set; }
        public virtual item item { get; set; }
    }
}
