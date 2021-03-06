using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CheckYoPotato.Listeners;
using CheckYoPotato.Models.Enums;
using CheckYoPotato.Resources;
using CheckYoPotato.ViewModels;
using Com.Mikepenz.Materialdrawer;
using Com.Mikepenz.Materialdrawer.Model;
using Com.Mikepenz.Materialdrawer.Model.Interfaces;

namespace CheckYoPotato.Activities
{
    public partial class MainActivity
    {
        private Drawer _drawer;

        private PrimaryDrawerItem GetBasePrimaryItem()
        {
            var btn = new PrimaryDrawerItem();
            btn.WithIconTintingEnabled(true);
            btn.WithTextColorRes(Resource.Color.BrushText);
            btn.WithIconColorRes(Resource.Color.BrushNoSearchResults);
            btn.WithSelectedColorRes(Resource.Color.BrushAnimeItemBackground);
            btn.WithSelectedTextColorRes(Resource.Color.AccentColour);
            btn.WithSelectedIconColorRes(Resource.Color.AccentColourDark);
            return btn;
        }

        private SecondaryDrawerItem GetBaseSecondaryItem()
        {
            var btn = new SecondaryDrawerItem();
            btn.WithIconTintingEnabled(true);
            btn.WithTextColorRes(Resource.Color.BrushText);
            btn.WithIconColorRes(Resource.Color.BrushNoSearchResults);
            btn.WithSelectedColorRes(Resource.Color.BrushAnimeItemBackground);
            btn.WithSelectedTextColorRes(Resource.Color.AccentColour);
            btn.WithSelectedIconColorRes(Resource.Color.AccentColourDark);
            return btn;
        }

        private void BuildDrawer()
        {
            var builder = new DrawerBuilder().WithActivity(this);
            builder.WithSliderBackgroundColorRes(Resource.Color.BrushHamburgerBackground);
            builder.WithStickyFooterShadow(true);


            var animeButton = GetBasePrimaryItem();
            animeButton.WithName("Yo Fridge");
            animeButton.WithIdentifier((int)PageIndex.PageFridge);
            animeButton.WithIcon(Resource.Drawable.icon_fridge);

            var searchButton = GetBasePrimaryItem();
            searchButton.WithName("Yo Fridge Chat");
            searchButton.WithIdentifier((int)PageIndex.PageFridgeChat);
            searchButton.WithIcon(Resource.Drawable.icon_chat);

            var seasonalButton = GetBasePrimaryItem();
            seasonalButton.WithName("Yo Fridge Images");
            seasonalButton.WithIdentifier((int)PageIndex.PagePhotos);
            seasonalButton.WithIcon(Resource.Drawable.icon_photo);
         
            var settingsButton = GetBaseSecondaryItem();
            settingsButton.WithName("Login");
            settingsButton.WithIdentifier((int)PageIndex.PageLogin);
            //settingsButton.WithIcon(Resource.Drawable.icon_settings);

            builder.AddStickyDrawerItems(settingsButton);

            builder.WithDrawerItems(new List<IDrawerItem>()
            {
                animeButton,
                searchButton,
                seasonalButton,
            });

            _drawer = builder.Build();
            _drawer.StickyFooter.SetBackgroundColor(new Color(ResourceExtension.BrushAnimeItemInnerBackground));
            _drawer.OnDrawerItemClickListener = new HamburgerItemClickListener(OnHamburgerItemClicked);
        }

        private void OnHamburgerItemClicked(View view, int i, IDrawerItem arg3)
        {
            var page = (PageIndex)arg3.Identifier;
            ViewModelLocator.MainViewModel.Navigate(page);
            _drawer.SetSelection(arg3, false);
            _drawer.CloseDrawer();
        }
    }
}