using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckYoPotato.Models.Enums;

namespace CheckYoPotato.Adapters
{
    public interface INavigationAdapter
    {
        void Navigate(PageIndex index);
    }
}
