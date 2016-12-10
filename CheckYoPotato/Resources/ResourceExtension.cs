using Android.Support.V4.Content.Res;
using CheckYoPotato.Activities;

namespace CheckYoPotato.Resources
{
    public static class ResourceExtension
    {
        public static readonly int BrushText = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushText, null);

        public static readonly int AccentColour = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.AccentColour, null);

        public static readonly int BrushAnimeItemInnerBackground = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushAnimeItemInnerBackground, null);

        public static readonly int BrushSelectedDialogItem = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushSelectedDialogItem, null);

        public static readonly int BrushFlyoutBackground = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushFlyoutBackground, null);

        public static readonly int BrushRowAlternate1 = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushRowAlternate1, null);

        public static readonly int BrushRowAlternate2 = ResourcesCompat.GetColor(MainActivity.CurrentContext.Resources,
            Resource.Color.BrushRowAlternate2, null);

    }
}