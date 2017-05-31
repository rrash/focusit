using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Tasky.Core;
using TaskyAndroid.Adapters;
using Android.Views;
using Android.Graphics;
using Android.Provider;

namespace TaskyAndroid.Screens {
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "ContactsScreen")]			
	public class ContactsScreen : Activity {
       // IList<Contact> contacts;
        ContactsAdapter contactsAdapter;
        ListView contactsListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set our layout to be the home screen
            SetContentView(Resource.Layout.ContactsScreen);
            View titleView = Window.FindViewById(Android.Resource.Id.Title);
            if (titleView != null)
            {
                IViewParent parent = titleView.Parent;
                if (parent != null && (parent is View))
                {
                    View parentView = (View)parent;
                    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75, 0xFF)); //38, 117 ,255
                } 
            }
             contactsAdapter = new ContactsAdapter(this);
             contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
             contactsListView.Adapter = contactsAdapter;

            // wire up task click handler
            if (contactsListView != null)
            {
                contactsListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                   // string[] items;
                   // var t= items[e.position]
                    var userDetails = new Intent(this, typeof(UserDetailsScreen));
                    userDetails.PutExtra("ContactID", contactsAdapter[e.Position].Id);
                    userDetails.PutExtra("ContactName", contactsAdapter[e.Position].DisplayName);
                    //contactDetails.PutExtra("ContactID", contacts[e.Position].Id);
                    StartActivity(userDetails);
                };

            }

        }
           
    }
}