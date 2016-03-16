using Android.Bluetooth;

namespace Exposeum
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    /// <summary>
    ///   The main activity is implemented as a ListActivity. When the user
    ///   clicks on a item in the ListView, we will display the appropriate activity.
    /// </summary>
    [Activity(Label = "@string/app_name", Icon = "@drawable/Logo", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Locale)]
    	public class MainActivity : ListActivity
	{

        const int EnableBluetoothRequestCode = 123321;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			ListAdapter = new MainMenuAdapter (this);

            BluetoothManager bluetoothManager = (BluetoothManager)GetSystemService(BluetoothService);
            BluetoothAdapter bluetoothAdapter = bluetoothManager.Adapter;

            if (bluetoothAdapter == null || !bluetoothAdapter.IsEnabled)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableBtIntent, EnableBluetoothRequestCode);
            }
        }

        protected override void OnListItemClick (ListView l, View v, int position, long id)
		{
			Intent startActivity = (Intent)ListAdapter.GetItem (position);
			StartActivity (startActivity);
		}
        
    }
}
