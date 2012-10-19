using System;
using Android.Widget;
using Android.Views;

namespace MonoDroid.Dialog
{
	public class ProgressBarElement : Element
	{
		public ProgressBarElement (string caption) : base(caption)
		{
		}

		public override Android.Views.View GetView (Android.Content.Context context, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			ProgressBar bar;
			View vw = DroidResources.LoadProgressBarLayout(context, convertView, parent, out bar);
			return vw;
			//return base.GetView (context, convertView, parent);
		}
	}
}

