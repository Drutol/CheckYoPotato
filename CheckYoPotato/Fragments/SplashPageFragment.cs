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

namespace CheckYoPotato.Fragments
{
    public class SplashPageFragment : FragmentBase
    {
        protected override void Init(Bundle savedInstanceState)
        {

        }

        protected override void InitBindings()
        {

        }

        public override int LayoutResourceId => Resource.Layout.SplashPage;
    }
}