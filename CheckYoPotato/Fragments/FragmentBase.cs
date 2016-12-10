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
using CheckYoPotato.Models;
using GalaSoft.MvvmLight.Helpers;

namespace CheckYoPotato.Fragments
{
    public abstract class FragmentBase : Fragment
    {
        private readonly bool _initBindings;

        protected FragmentBase(bool initBindings = true)
        {
            _initBindings = initBindings;
        }

        protected View RootView { get; private set; }

        protected Dictionary<int, List<Binding>> Bindings = new Dictionary<int, List<Binding>>();

        protected abstract void Init(Bundle savedInstanceState);

        protected abstract void InitBindings();

        public abstract int LayoutResourceId { get; }

        protected T FindViewById<T>(int id) where T : View => RootView.FindViewById<T>(id);

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Init(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (RootView == null)
                RootView = inflater.Inflate(LayoutResourceId, container, false);
            if (_initBindings)
                InitBindings();
            return RootView;
        }

        public sealed override void OnStop()
        {
            DetachBindings();
            base.OnStop();
        }

        public void DetachBindings()
        {
            Bindings?.ForEach(pair => pair.Value.ForEach(binding => binding.Detach()));
            Bindings = new Dictionary<int, List<Binding>>();
            Cleanup();
        }

        public void ReattachBindings()
        {
            if (!Bindings.Any() && RootView != null)
                InitBindings();
        }

        protected virtual void Cleanup()
        {

        }
    }
}