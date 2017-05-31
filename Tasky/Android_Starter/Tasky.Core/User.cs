using System;
using Android.OS;

namespace Tasky.Core
{
    /// <summary>
    /// Task business object
    /// </summary>
    public class User
    {
        public User()
        {
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}