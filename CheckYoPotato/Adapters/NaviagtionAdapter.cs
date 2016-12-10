using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CheckYoPotato.Adapters;
using CheckYoPotato.Models.Enums;

namespace CheckYoPotato
{
    public class NaviagtionAdapter : INavigationAdapter
    {
        public delegate void NavigationRequest(Fragment fragment);


        public void Navigate(PageIndex index)
        {
            
        }
    }
}