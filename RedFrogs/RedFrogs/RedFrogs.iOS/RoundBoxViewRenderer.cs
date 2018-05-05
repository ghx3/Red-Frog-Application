using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RedFrogs.Controls;
using RedFrogs.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundBoxView), typeof(RoundBoxViewRenderer))]
namespace RedFrogs.iOS
{
    public class RoundBoxViewRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;

            Layer.MasksToBounds = true;
            Layer.CornerRadius = (float)((RoundBoxView)this.Element).CornerRadius / 2.0f;
        }
    }
}