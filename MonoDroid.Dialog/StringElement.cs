using System;

using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Android.Graphics;

namespace MonoDroid.Dialog
{
	public class StringElement : Element
	{
		public int FontSize { get; set; }

		public string Value {
			get { return _value; }
			set {
				_value = value;
				if (_text != null)
					_text.Text = _value;
			}
		}

		private string _value;
		public object Alignment;

		public bool DetailTextSingleLine {
			get;
			set;
		}

		/// <summary>
		/// When the element is pressed, this is the background color.
		/// </summary>
		/// <value>
		/// The press background resource.
		/// </value>
		public int PressBackgroundResource {
			get;
			set;
		}

		/// <summary>
		/// When the element is released, this is the background color
		/// </summary>
		/// <value>
		/// The release background resource.
		/// </value>
		public Android.Graphics.Color ReleaseBackgroundColor {
			get;
			set;
		}

		/// <summary>
		/// When the button is clicked, this can be sent is as an argument
		/// </summary>
		/// <value>
		/// The command argument.
		/// </value>
		public object CommandArgument {
			get;
			set;
		}

		public StringElement (string caption)
            : base(caption, (int)DroidResources.ElementLayout.dialog_labelfieldright)
		{
		}

		public StringElement (string caption, int layoutId)
            : base(caption, layoutId)
		{
		}

		public StringElement (string caption, string value)
            : base(caption, (int)DroidResources.ElementLayout.dialog_labelfieldright)
		{
			Value = value;
		}
		
		public StringElement (string caption, string value, Action clicked)
            : base(caption, (int)DroidResources.ElementLayout.dialog_labelfieldright)
		{
			Value = value;
			this.Click = clicked;
		}

		public StringElement (string caption, string value, int layoutId)
            : base(caption, layoutId)
		{
			Value = value;
		}

		public StringElement (string caption, string value, int layoutId, Action clicked, int pressBackgroundResource, Color releaseBackgroundColor)
			: base(caption, layoutId)
		{
			Value = value;
			this.Click = clicked;
			PressBackgroundResource = pressBackgroundResource;
			ReleaseBackgroundColor = releaseBackgroundColor;

		}
		
		public StringElement (string caption, Action clicked)
            : base(caption, (int)DroidResources.ElementLayout.dialog_labelfieldright)
		{
			Value = null;
			this.Click = clicked;
		}

		public override View GetView (Context context, View convertView, ViewGroup parent)
		{
			View view = DroidResources.LoadStringElementLayout (context, convertView, parent, LayoutId, out _caption, out _text);
			if (view != null) {
				_caption.Text = Caption;
				_caption.TextSize = FontSize;
				_text.Text = Value;
				_text.TextSize = FontSize;
				_text.SetSingleLine (DetailTextSingleLine);

				if (String.IsNullOrEmpty (_caption.Text)) {
					_caption.Visibility = ViewStates.Gone;
				}

				if (Click != null) {
					view.Touch += delegate(object sender, View.TouchEventArgs e) {
						if (e.Event.Action == MotionEventActions.Up) {
							view.SetBackgroundColor (ReleaseBackgroundColor);
							this.Click ();
						} else if (e.Event.Action == MotionEventActions.Cancel) {
							view.SetBackgroundColor (ReleaseBackgroundColor);
						} else {
							if (PressBackgroundResource > 0) {
								view.SetBackgroundResource (PressBackgroundResource);
							}
						}
										
					};
				}
			}
			return view;
		}
			
		public override void Selected ()
		{
			base.Selected ();
			
			if (this.Click != null) {
				Click ();
			}
		}

		public override string Summary ()
		{
			return Value;
		}

		public override bool Matches (string text)
		{
			return (Value != null ? Value.IndexOf (text, StringComparison.CurrentCultureIgnoreCase) != -1 : false) ||
				base.Matches (text);
		}

		protected TextView _caption;
		protected TextView _text;

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				//_caption.Dispose();
				_caption = null;
				//_text.Dispose();
				_text = null;
			}
		}
	}
}