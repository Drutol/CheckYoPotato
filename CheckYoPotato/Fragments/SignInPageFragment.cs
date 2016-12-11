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
using CheckYoPotato.ViewModels;
using GalaSoft.MvvmLight.Helpers;

namespace CheckYoPotato.Fragments
{
    public class SignInPageFragment : FragmentBase
    {
        private LoginViewModel ViewModel;


        protected override void Init(Bundle savedInstanceState)
        {
            ViewModel = ViewModelLocator.LoginViewModel;
        }

        protected override void InitBindings()
        {
            
            LoginPageSignInButton.SetCommand(ViewModel.LoginCommand);

            Bindings.Add(LoginPageProgressSpinner.Id,new List<Binding>());
            Bindings[LoginPageProgressSpinner.Id].Add(
                this.SetBinding(() => ViewModel.Loading, () => LoginPageProgressSpinner.Visibility)
                    .ConvertSourceToTarget(b => b ? ViewStates.Visible : ViewStates.Gone));

            Bindings.Add(LoginPageSignInButton.Id,new List<Binding>());
            Bindings[LoginPageSignInButton.Id].Add(
                this.SetBinding(() => ViewModel.Loading, () => LoginPageSignInButton.Enabled)
                    .ConvertSourceToTarget(b => !b));


        }

        public override int LayoutResourceId => Resource.Layout.LoginPage;

        private Button _loginPageSignInButton;
        private ProgressBar _loginPageProgressSpinner;

        public ProgressBar LoginPageProgressSpinner => _loginPageProgressSpinner ?? (_loginPageProgressSpinner = FindViewById<ProgressBar>(Resource.Id.LoginPageProgressSpinner));

        public Button LoginPageSignInButton => _loginPageSignInButton ?? (_loginPageSignInButton = FindViewById<Button>(Resource.Id.LoginPageSignInButton));
    }
}