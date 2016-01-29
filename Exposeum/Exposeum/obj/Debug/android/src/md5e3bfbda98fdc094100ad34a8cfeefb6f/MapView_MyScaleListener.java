package md5e3bfbda98fdc094100ad34a8cfeefb6f;


public class MapView_MyScaleListener
	extends android.view.ScaleGestureDetector.SimpleOnScaleGestureListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onScale:(Landroid/view/ScaleGestureDetector;)Z:GetOnScale_Landroid_view_ScaleGestureDetector_Handler\n" +
			"";
		mono.android.Runtime.register ("Exposeum.MapView+MyScaleListener, Exposeum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MapView_MyScaleListener.class, __md_methods);
	}


	public MapView_MyScaleListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MapView_MyScaleListener.class)
			mono.android.TypeManager.Activate ("Exposeum.MapView+MyScaleListener, Exposeum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MapView_MyScaleListener (md5e3bfbda98fdc094100ad34a8cfeefb6f.MapView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MapView_MyScaleListener.class)
			mono.android.TypeManager.Activate ("Exposeum.MapView+MyScaleListener, Exposeum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Exposeum.MapView, Exposeum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public boolean onScale (android.view.ScaleGestureDetector p0)
	{
		return n_onScale (p0);
	}

	private native boolean n_onScale (android.view.ScaleGestureDetector p0);

	java.util.ArrayList refList;
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
