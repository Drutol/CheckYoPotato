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

namespace CheckYoPotato.Activities
{
    public partial class MainActivity
    {
        private ImageButton _mainPageHamburgerButton;
        private TextView _mainPageCurrentStatus;
        private FrameLayout _mainContentFrame;

        public ImageButton MainPageHamburgerButton => _mainPageHamburgerButton ?? (_mainPageHamburgerButton = FindViewById<ImageButton>(Resource.Id.MainPageHamburgerButton));

        public TextView MainPageCurrentStatus => _mainPageCurrentStatus ?? (_mainPageCurrentStatus = FindViewById<TextView>(Resource.Id.MainPageCurrentStatus));

        public FrameLayout MainContentFrame => _mainContentFrame ?? (_mainContentFrame = FindViewById<FrameLayout>(Resource.Id.MainContentFrame));
    }
}