using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Tasky.Core;
using TaskyAndroid;

namespace TaskyAndroid.Screens
{
    /// <summary>
    /// View/edit a Task
    /// </summary>
    [Activity(Label = "UserDetailsScreen")]
    public class UserDetailsScreen : Activity
    {
        User user = new User();
        Button cancelDeleteButton;
        EditText notesTextEdit;
        EditText nameTextEdit;
        Button saveButton;




        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            int contactID = Intent.GetIntExtra("ContactID", 0);
            string contactName = Intent.GetStringExtra("ContactName");
            //if (contactID > 0) {
            //task = TaskManager.GetTask(taskID);
            //}

            // set our layout to be the home screen
            SetContentView(Resource.Layout.UserDetails);
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
            nameTextEdit = FindViewById<EditText>(Resource.Id.UserNameText);
            saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            // find all our controls
            cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);
            // set the cancel delete based on whether or not it's an existing user
            cancelDeleteButton.Text = (user.UserID == 0 ? "Cancel" : "Delete");

            nameTextEdit.Text = contactName;


            // button clicks 
            cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
            saveButton.Click += (sender, e) => { Save(); };


            void Save()
            {
                user.UserName = nameTextEdit.Text;

                TaskUserManager.SaveUser(user);
                Finish();
            }

            void CancelDelete()
            {
                if (user.UserID != 0)
                {
                    TaskUserManager.DeleteUser(user.UserID);
                }
                Finish();
            }
        }
    }
}