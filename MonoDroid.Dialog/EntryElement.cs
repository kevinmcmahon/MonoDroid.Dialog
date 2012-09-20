﻿using System;
using System.Drawing;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace MonoDroid.Dialog
{
	public class EntryElement : Element
	{
		public string Value
		{
			get
			{
				return val;
			}
			set
			{
				val = value;
				if (entry != null)
					entry.Text  = value;
			}
		}
		
		private string val;
		private string hint;
		private bool isPassword;
		private EditText entry;
		private Context _context = null;

		public event EventHandler Changed;

		public EntryElement(string caption, string hint, string value)
			: base(caption)
		{
			Value = value;
			this.hint = hint;
		}

		public EntryElement(string caption, string hint, string value, bool isPassword)
			: base(caption)
		{
			Value = value;
			this.isPassword = isPassword;
			this.hint = hint;
		}

		public override string Summary()
		{
			return Value;
		}

		SizeF ComputeEntryPosition()
		{
			Section s = Parent as Section;
			if (s.EntryAlignment.Width != 0)
				return s.EntryAlignment;

			SizeF max = new SizeF(-1, -1);
			foreach (var e in s.Elements)
			{
				var ee = e as EntryElement;
				if (ee == null)
					continue;
			}
			s.EntryAlignment = new SizeF(25 + Math.Min(max.Width, 160), max.Height);
			return s.EntryAlignment;
		}

		public override View GetView(Context context, View convertView, ViewGroup parent)
		{
            _context = context;
			var cell = new RelativeLayout(context);

			if (entry == null)
			{
				var _entry = new EditText(context)
								 {
									 Tag = 1,
									 Hint = hint ?? "",
									 Text = Value ?? "",
								 };

				if(isPassword)
				{
					_entry.InputType = Android.Text.InputTypes.TextVariationPassword;
				}

				entry = _entry;

				entry.TextChanged += delegate
				{
					FetchValue();
				};
			}
			var tvparams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
														  ViewGroup.LayoutParams.WrapContent);
			tvparams.SetMargins(5,3,5,0);
            tvparams.AddRule(LayoutRules.CenterVertical);
            tvparams.AddRule(LayoutRules.AlignParentLeft);

			var tv = new TextView(context) {Text = Caption, TextSize = 16f};

			var eparams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
														  ViewGroup.LayoutParams.WrapContent);
			eparams.SetMargins(5, 3, 5, 0);
            eparams.AddRule(LayoutRules.CenterVertical);
            eparams.AddRule(LayoutRules.AlignParentRight);

			cell.AddView(tv,tvparams);
			cell.AddView(entry,eparams);
			return cell;
		}

		public void FetchValue()
		{
			if (entry == null)
				return;

			var newValue = entry.Text.ToString();
			var diff = newValue != Value;
			Value = newValue;

			if (diff && Changed != null)
			{
				Changed(this, EventArgs.Empty);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				entry.Dispose();
				entry = null;
			}
		}

		public override bool Matches(string text)
		{
			return (Value != null ? Value.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1 : false) || base.Matches(text);
		}
	}
}