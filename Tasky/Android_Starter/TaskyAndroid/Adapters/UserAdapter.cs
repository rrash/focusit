using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tasky.Core;

namespace TaskyAndroid.Adapters
{
    
        public class UserAdapter : BaseAdapter<User>
        {
            Activity context = null;
            IList<User> users = new List<User>();

            public UserAdapter(Activity context, IList<User>users) : base()
            {
                this.context = context;
                this.users = users;
            }

            public override User this[int position]
            {
                get { return users[position]; }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override int Count
            {
                get { return users.Count; }
            }

     
        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
            {
                // Get our object for position
                var item = users[position];

            //Try to reuse convertView if it's not  null, otherwise inflate it from our item layout
            // gives us some performance gains by not always inflating a new view
            // will sound familiar to MonoTouch developers with UITableViewCell.DequeueReusableCell()

               var view = (convertView ??
                     context.LayoutInflater.Inflate(
                      Android.Resource.Layout.SimpleListItemChecked,
                       parent,
                       false)) as CheckedTextView;

            
                view.SetText(item.UserName == "" ? "<new User>" : item.UserName, TextView.BufferType.Normal);
                //view.Checked = item.Done;

            //Finally return the view
            return view;
            }
        }
    
}