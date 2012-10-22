using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MonoDroid.Dialog;
using System;
using Android.Graphics;

namespace DialogSampleApp
{
	//
	// NOTE: with the new update you will have to add all the dialog_* prefixes to your main application.
	//       This is because the current version of Mono for Android will not add resources from assemblies
	//       to the main application like it does for libraries in Android/Java/Eclipse...  This could
	//       change in a future version (it's slated for 1.0 post release) but for now, just add them 
	//       as in this sample...
	//
	[Activity(Label = "MD.D Sample", MainLauncher = true, WindowSoftInputMode = SoftInput.AdjustPan)]
	public class Activity1 : Activity
	{
		ProgressBar progressBar;
		ListView listView;

		protected void StartNew ()
		{
			StartActivity (typeof(Activity2));
		}

		void btnString_Click()
		{
			Console.WriteLine("OK");
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.main);
			progressBar = FindViewById<ProgressBar> (Resource.Id.dialog_progressbar);
			listView = FindViewById<ListView> (Resource.Id.dialog_listView);

			var s = new Section("Test Header", "Test Footer")
			{
				new StringElement("Do Something", "Foo", (int)DroidResources.ElementLayout.dialog_labelfieldright, delegate {
					Console.WriteLine("OK");
				}, Resource.Color.creme, Android.Graphics.Color.Transparent),
				new ButtonElement("DialogActivity", () => StartNew()),
				new BooleanElement("Push my button", true),
				new BooleanElement("Push this too", false),
				new StringElement("Text label", "The Value"),
				new BooleanElement("Push my button", true),
				new BooleanElement("Push this too", false),
			};

			//s.BackgroundColor = Color.DarkRed;

			var root = new RootElement ("Test Root Elem")
                {
                    s,
//                    new Section("Part II")
//                        {
//                            new StringElement("This is the String Element", "The Value", delegate {
//						Console.WriteLine("Clicked string element.");}
//                           ),
//                            new CheckboxElement("Check this out", true),
//                            new EntryElement("Username",""){
//                                Hint = "Enter Login"
//                            },
//                            new EntryElement("Password", "") {
//                                Hint = "Enter Password",
//                                Password = true,
//                            },
//							//new ProgressBarElement("")
//                        },
//					new Section("Part III")
//						{
//							new ButtonElement("Hide Progress", () => HideProgress())
//						}
                };

			var da = new DialogAdapter (this, root);
			listView.Adapter = da;
			//var lv = new ListView(this) {Adapter = da};
			//SetContentView(listView);
		}

		void HideProgress ()
		{
			progressBar.Visibility = ViewStates.Gone;
		}
	}
}