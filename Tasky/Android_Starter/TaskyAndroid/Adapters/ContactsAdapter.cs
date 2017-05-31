using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.Views;
using Tasky.Core;
using Android.Content;
using Android.Provider;

namespace TaskyAndroid.Adapters
{
    /// <summary>
    /// Adapter that presents Contacts in a row-view
    /// </summary>
    public class ContactsAdapter : BaseAdapter<Contact>
    {
        IList<Contact> _contactList = new List<Contact>();
        //List<Contact> _contactList;
        Activity _activity;

        public ContactsAdapter(Activity activity) : base()
        {
            _activity = activity;
            
            FillContacts();
        }
        public override int Count
        {
            get { return _contactList.Count; }
        }
        public override Contact this[int position]
        {
            get { return _contactList[position]; }
        }
        
        public override long GetItemId(int position)
        {
            return _contactList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.ContactListItem, parent, false);
            var contactName = view.FindViewById<TextView>(Resource.Id.ContactName);
            var contactImage = view.FindViewById<ImageView>(Resource.Id.ContactImage);
            contactName.Text = _contactList[position].DisplayName;

            if (_contactList[position].PhotoId == null)
            {
                contactImage = view.FindViewById<ImageView>(Resource.Id.ContactImage);
                contactImage.SetImageResource(Resource.Drawable.contactImage);
            }
            else
            {
                var contactUri = ContentUris.WithAppendedId(
                    ContactsContract.Contacts.ContentUri, _contactList[position].Id);
                var contactPhotoUri = Android.Net.Uri.WithAppendedPath(contactUri,
                    Contacts.Photos.ContentDirectory);
                contactImage.SetImageURI(contactPhotoUri);
            }
            return view;
        }

        void FillContacts()
        {
            var uri = ContactsContract.Contacts.ContentUri;

            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName,
                ContactsContract.Contacts.InterfaceConsts.PhotoId
            };

            var cursor =_activity.ManagedQuery(uri, projection, null,null, null);

            //_contactList = new List<Contact>();

            if (cursor.MoveToFirst())
            {
                do
                {
                    _contactList.Add(new Contact
                    {
                        Id = cursor.GetLong(
                    cursor.GetColumnIndex(projection[0])),
                        DisplayName = cursor.GetString(
                    cursor.GetColumnIndex(projection[1])),
                        PhotoId = cursor.GetString(
                    cursor.GetColumnIndex(projection[2]))
                    });
                } while (cursor.MoveToNext());
            }
        }

       /* class Contact
        {
            public long Id { get; set; }
            public string DisplayName { get; set; }
            public string PhotoId { get; set; }
        }*/
    }


}