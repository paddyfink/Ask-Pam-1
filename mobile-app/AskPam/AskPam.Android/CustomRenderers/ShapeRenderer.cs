//
// ShapeRenderer.cs: Draw custom circle shape in Xamarin.Forms using custom platform renderer.
//
// Author:
//   Chris Pellett
//   Github: https://github.com/chrispellett/Xamarin-Forms-Shape
//

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AskPam.Controls;
using AskPam.Droid.CustomRenderers;

[assembly:ExportRenderer (typeof(ShapeView), typeof(ShapeRenderer))]
namespace AskPam.Droid.CustomRenderers
{
	public class ShapeRenderer : ViewRenderer<ShapeView, Shape>
	{
		public ShapeRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<ShapeView> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || this.Element == null)
				return;

			SetNativeControl (new Shape (Resources.DisplayMetrics.Density, Context) {
				ShapeView = Element
			});
		}
	}
}