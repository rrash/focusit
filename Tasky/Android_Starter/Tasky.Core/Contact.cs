using System;

namespace Tasky.Core {
	/// <summary>
	/// Task business object
	/// </summary>
	public class Contact {
		public Contact ()
		{
		}

        public int ID { get; set; }
        public long Id { get; set; }
        public int UserID { get; set; }
		public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PhotoId { get; set; }
        // TODO: add this field to the user-interface
    }
}