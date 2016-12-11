using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using CheckYoPotato.Fragments;
using CheckYoPotato.Models.Enums;
using CheckYoPotato.ViewModels;
using GalaSoft.MvvmLight.Helpers;

namespace CheckYoPotato.Activities
{
    [Activity(Label = "CheckYoPotato", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
         ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/icon",
         Theme = "@style/Theme.AppCompat.NoActionBar")]
    public partial class MainActivity : Activity
    {
        public static Activity CurrentContext { get; private set; }
        private readonly List<Binding> _bindings = new List<Binding>();

        private MainViewModel ViewModel;
        private FragmentBase _lastPage;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            ViewModelLocator.InitializeDependencies();
            ViewModel = ViewModelLocator.MainViewModel;
            ViewModel.NavigationRequested += ViewModelOnNavigationRequested;
            ViewModel.Navigate(PageIndex.PageLogin);
            CurrentContext = this;

            _bindings.Add(this.SetBinding(() => ViewModel.CurrentStatus, () => MainPageCurrentStatus.Text));


            BuildDrawer();

            MainPageHamburgerButton.Click += (sender, args) => _drawer.OpenDrawer();


        }

        private void ViewModelOnNavigationRequested(PageIndex page)
        {
            FragmentBase fragment = null;
            switch (page)
            {
                case PageIndex.PageSplash:
                    fragment = new SplashPageFragment();
                    break;
                case PageIndex.PageLogin:
                    fragment = new SignInPageFragment();
                    break;
                case PageIndex.PageFridge:
                    break;
                case PageIndex.PageFridgeChat:
                    break;
                case PageIndex.PagePhotos:
                    fragment = new PhotosPageFragment();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
            _lastPage = fragment;
            var trans = FragmentManager.BeginTransaction();
            trans.SetCustomAnimations(Resource.Animator.animation_slide_btm,
                Resource.Animator.animation_fade_out,
                Resource.Animator.animation_slide_btm,
                Resource.Animator.animation_fade_out);
            trans.Replace(Resource.Id.MainContentFrame, fragment);
            trans.Commit();

        }
    }
}

