using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;

namespace CheckYoPotato.ViewModels
{
    public static class ViewModelLocator
    {
        public static void InitializeDependencies()
        {
            SimpleIoc.Default.Register<SplashViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PhotosPageViewModel>();
        }

        public static SplashViewModel SplashViewModel => SimpleIoc.Default.GetInstance<SplashViewModel>();

        public static LoginViewModel LoginViewModel => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public static MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public static PhotosPageViewModel PhotosPageViewModel => SimpleIoc.Default.GetInstance<PhotosPageViewModel>();
    }
}
