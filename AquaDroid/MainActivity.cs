using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using System.Linq;
using Android.Content.PM; 

namespace AquaDroid
{
    [Activity(Label = "AquaDroid", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        Button btnPaired;
        ListView devicelist;
         
        private BluetoothAdapter myBluetooth = null;
        private List<BluetoothDevice> pairedDevices;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            btnPaired = (Button)FindViewById(Resource.Id.button1);
            devicelist = (ListView)FindViewById(Resource.Id.listView1);
            myBluetooth = BluetoothAdapter.DefaultAdapter;
            if (myBluetooth == null)
            {
                //Show a mensag. that thedevice has no bluetooth adapter
                Toast.MakeText(ApplicationContext, "Bluetooth Device Not Available", ToastLength.Long).Show();
                //finish apk
                Finish();
            }
            else
            {
                if (!myBluetooth.IsEnabled)
                {
                    //Ask to the user turn the bluetooth on 
                    Intent turnBTon = new Intent(BluetoothAdapter.ActionRequestEnable);
                    StartActivityForResult(turnBTon, 1);
                }
            }

            btnPaired.Click += BtnPaired_Click;
        }

        private void BtnPaired_Click(object sender, System.EventArgs e)
        {
            PairedDevicesList();
        }

        private void PairedDevicesList()
        {
            pairedDevices = myBluetooth.BondedDevices.ToList();
            ArrayList list = new ArrayList();

            if (pairedDevices.Count > 0)
            {
                foreach (BluetoothDevice bt in pairedDevices)
                {
                    list.Add(bt.Name + "\n" + bt.Address); //Get the device's name and the address
                }
            }
            else
            {
                Toast.MakeText(ApplicationContext, "No Paired Bluetooth Devices Found.", ToastLength.Long).Show();
            }

            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, list);
            devicelist.Adapter = adapter;
            devicelist.ItemClick += Devicelist_ItemClick; ; //Method called when the device from the list is clicked
        }

        private void Devicelist_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string info = ((TextView)e.View).Text;
            string address = info.Substring(info.Length - 17);
            // Make an intent to start next activity.
            Intent i = new Intent(this, typeof(RelayControl));
            //Change the activity.
            i.PutExtra("EXTRA_ADDRESS", address); //this will be received at ledControl (class) Activity
            StartActivity(i);
        }
    }
}

