using RedFrogs.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(NoLineEntryRenderer))]
namespace RedFrogs.iOS
{
    class NoLineEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            this.Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}