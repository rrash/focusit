package md52f97764f6a45984e221f46f6126f2ed8;


public class ContactsScreen
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("TaskyAndroid.Screens.ContactsScreen, TaskyAndroid, Version=1.0.6360.16426, Culture=neutral, PublicKeyToken=null", ContactsScreen.class, __md_methods);
	}


	public ContactsScreen () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ContactsScreen.class)
			mono.android.TypeManager.Activate ("TaskyAndroid.Screens.ContactsScreen, TaskyAndroid, Version=1.0.6360.16426, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
