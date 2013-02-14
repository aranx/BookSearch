using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookSearch
{
   public class Book
    {
       public Book() { }
       public string BookName{get;set;}
       public string Price { get; set; }
       public string Publisher { get; set; }
       public string Author { get; set; }
       public string ISBN { get; set; }
       public string PubDate { get; set; }
       public string Translator { get; set; }
    }
}
