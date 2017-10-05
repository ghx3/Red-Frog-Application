using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using RedFrogs.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(Picker), typeof(NoLinePickerRenderer))]
namespace RedFrogs.iOS
{
    class NoLinePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            this.Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}