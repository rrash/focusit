using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Tasky.Core;
using TaskyAndroid;
using Android.Views;
using Android.Graphics;
using Android.Provider;

namespace TaskyAndroid.Screens {
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "Tasky", MainLauncher = true, Icon="@drawable/icon")]			
	public class HomeScreen : Activity {
		Adapters.TaskListAdapter taskList;
        Adapters.UserAdapter userList;

		IList<Task> tasks;
        IList<User> users;
		Button addTaskButton;
		ListView taskListView;
        ListView userListView;
       
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// set our layout to be the home screen
			SetContentView(Resource.Layout.HomeScreen);
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

            //Find our controls
            //taskListView = FindViewById<ListView> (Resource.Id.TaskList); 
            userListView = FindViewById<ListView>(Resource.Id.UserList);
            addTaskButton = FindViewById<Button> (Resource.Id.AddButton);

			// wire up add task button handler
			if(addTaskButton != null) {
				addTaskButton.Click += (sender, e) => {
					StartActivity(typeof(TaskDetailsScreen));
				};
			}
			
			// wire up task click handler
			if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var taskDetails = new Intent (this, typeof (TaskDetailsScreen));
					taskDetails.PutExtra ("TaskID", tasks[e.Position].ID);
					StartActivity (taskDetails);
				};
                
                

            }
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

            //tasks = TaskManager.GetTasks();
            users = TaskUserManager.GetUsers();

            // create our adapter
           // taskList = new Adapters.TaskListAdapter(this, tasks);
            userList = new Adapters.UserAdapter(this, users);


            //Hook up our adapter to our ListView
            //taskListView.Adapter = taskList;
            userListView.Adapter = userList;

            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = null;
            switch (item.ItemId)
            {
                case Resource.Id.menuTask:
                    {

                        // add your code  
                        intent = new Intent(this, typeof(TaskDetailsScreen));
                        break;
                        //return true;
                    }
                case Resource.Id.menuUser:
                    {
                        // add your code 
                        intent = new Intent(this, typeof(ContactsScreen));
                        break;
                        //return true;
                    }

                default://invalid option
                    return base.OnOptionsItemSelected(item);

            }
            if (intent != null)
            {
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}