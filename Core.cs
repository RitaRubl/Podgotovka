using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podgotovka
{
    internal static class Core
    {
        public static Entities Context = new Entities();
        public static User CurrentUser = null;
    }

    public partial class Product
    {
        public bool IsOver15  => Discount > 15;
    }
}
