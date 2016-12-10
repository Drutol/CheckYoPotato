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
using FFImageLoading;
using FFImageLoading.Views;
using GalaSoft.MvvmLight.Helpers;

namespace CheckYoPotato.Fragments
{
    public class PhotosPageFragment : FragmentBase
    {
        private PhotosPageViewModel ViewModel;

        protected override void Init(Bundle savedInstanceState)
        {
            ViewModel = ViewModelLocator.PhotosPageViewModel;
        }

        protected override void InitBindings()
        {
            Bindings.Add(PhotosPageImage.Id,new List<Binding>());
            Bindings[PhotosPageImage.Id].Add(this.SetBinding(() => ViewModel.ImageLink).WhenSourceChanges(() =>
                {
                    ImageService.Instance.LoadUrl(ViewModel.ImageLink).Into(PhotosPageImage);
                }
            ));

            PhotosPagePullPhotoButton.SetCommand(ViewModel.RefreshPhotoCommand);

            ViewModel.RefreshPhotoCommand.Execute(null);
        }

        public override int LayoutResourceId => Resource.Layout.PhotosPage;


        #region Views

        private ImageViewAsync _photosPageImage;
        private Button _photosPagePullPhotoButton;

        public ImageViewAsync PhotosPageImage => _photosPageImage ?? (_photosPageImage = FindViewById<ImageViewAsync>(Resource.Id.PhotosPageImage));

        public Button PhotosPagePullPhotoButton => _photosPagePullPhotoButton ?? (_photosPagePullPhotoButton = FindViewById<Button>(Resource.Id.PhotosPagePullPhotoButton));

        #endregion
    }
}