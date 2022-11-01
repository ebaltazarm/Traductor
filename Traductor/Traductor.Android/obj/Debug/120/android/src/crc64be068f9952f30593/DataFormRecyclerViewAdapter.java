package crc64be068f9952f30593;


public class DataFormRecyclerViewAdapter
	extends androidx.recyclerview.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroidx/recyclerview/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroidx_recyclerview_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroidx/recyclerview/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_getItemId:(I)J:GetGetItemId_IHandler\n" +
			"";
		mono.android.Runtime.register ("Syncfusion.Android.DataForm.DataFormRecyclerViewAdapter, Syncfusion.SfDataForm.XForms.Android", DataFormRecyclerViewAdapter.class, __md_methods);
	}


	public DataFormRecyclerViewAdapter ()
	{
		super ();
		if (getClass () == DataFormRecyclerViewAdapter.class) {
			mono.android.TypeManager.Activate ("Syncfusion.Android.DataForm.DataFormRecyclerViewAdapter, Syncfusion.SfDataForm.XForms.Android", "", this, new java.lang.Object[] {  });
		}
	}

	public DataFormRecyclerViewAdapter (android.content.Context p0, crc64be068f9952f30593.ScrollPanel p1)
	{
		super ();
		if (getClass () == DataFormRecyclerViewAdapter.class) {
			mono.android.TypeManager.Activate ("Syncfusion.Android.DataForm.DataFormRecyclerViewAdapter, Syncfusion.SfDataForm.XForms.Android", "Android.Content.Context, Mono.Android:Syncfusion.Android.DataForm.ScrollPanel, Syncfusion.SfDataForm.XForms.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();


	public void onBindViewHolder (androidx.recyclerview.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (androidx.recyclerview.widget.RecyclerView.ViewHolder p0, int p1);


	public androidx.recyclerview.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native androidx.recyclerview.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);


	public long getItemId (int p0)
	{
		return n_getItemId (p0);
	}

	private native long n_getItemId (int p0);

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
