using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Graphics;

namespace XamarinMemeGen.Droid
{
    [Activity(Label = "XamarinMemeGen.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Spinner memesSpinner;
        EditText TopText;
        EditText BottomText;
        Button GenerateMemeButton;
        ImageView ResultMeme;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Spinner memesSpinner = FindViewById<Spinner>(Resource.Id.spinnerMemes);
            EditText TopText = FindViewById<EditText>(Resource.Id.TopText);
            EditText BottomText = FindViewById<EditText>(Resource.Id.BottomText);
            Button GenerateMemeButton = FindViewById<Button>(Resource.Id.GenerateMemeButton);
            ImageView ResultMeme = FindViewById<ImageView>(Resource.Id.ResultMeme);

            GenerateMemeButton.Click += async delegate
            {
                byte[] response = await Memes.GenerateMeme(memesSpinner.SelectedItem.ToString(), TopText.Text, BottomText.Text);
                Bitmap bm = BitmapFactory.DecodeByteArray(response, 0, response.Length);
                ResultMeme.SetImageBitmap(bm);
            };

            TopText.EditorAction

            //Calls the Shared Portable Class Library to get a list with all available meme's.
            ObservableCollection<string> memes = await Memes.GetMemesList();

            //Set the list of memes to our Spinner and enable it
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, memes);
            memesSpinner.Adapter = adapter;

        }

        void HandleTextChanged(object sender, EventArgs e) {

        }

    }
}


