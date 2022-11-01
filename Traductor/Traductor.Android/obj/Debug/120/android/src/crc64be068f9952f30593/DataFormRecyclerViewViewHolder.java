package crc64be068f9952f30593;


public class DataFormRecyclerViewViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Syncfusion.Android.DataForm.DataFormRecyclerViewViewHolder, Syncfusion.SfDataForm.XForms.Android", DataFormRecyclerViewViewHolder.class, __md_methods);
	}


	public DataFormRecyclerViewViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == DataFormRecyclerViewViewHolder.class) {
			mono.android.TypeManager.Activate ("Syncfusion.Android.DataForm.DataFormRecyclerViewViewHolder, Syncfusion.SfDataForm.XForms.Android", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}

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
