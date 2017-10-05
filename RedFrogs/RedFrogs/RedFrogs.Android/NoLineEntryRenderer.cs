using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using RedFrogs.Droid;

[assembly: ExportRenderer(typeof(Entry), typeof(NoLineEntryRenderer))]
namespace RedFrogs.Droid
{
    class NoLineEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}