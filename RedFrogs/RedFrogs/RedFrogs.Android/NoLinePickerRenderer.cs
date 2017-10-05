using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using RedFrogs.Droid;

[assembly: ExportRenderer(typeof(Picker), typeof(NoLinePickerRenderer))]
namespace RedFrogs.Droid
{
    class NoLinePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}