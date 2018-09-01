using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;
using Java.Util;
using System.IO;
 
using Android.Content.PM; 
using Android.Support.V4.Widget; 
using AquaDroid.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget; 

namespace AquaDroid
{
    [Activity(Label = "RelayControl", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")] 
    public partial class RelayControl : AppCompatActivity
    {
        EditText grzalkaWlacz;
        EditText grzalkaWylacz;
        Spinner[] spinnerWlaczH = new Spinner[7];
        Spinner[] spinnerWlaczM = new Spinner[7];
        Spinner[] spinnerWylaczH = new Spinner[7];
        Spinner[] spinnerWylaczM = new Spinner[7];
        EditText[] editTextPrzej = new EditText[2];

        static String address = null;
        static BluetoothAdapter myBluetooth = null;
        static BluetoothSocket btSocket = null;
        private static bool isBtConnected = false;
        static readonly UUID myUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        IMenuItem previousItem;
         
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Intent newint = Intent;
            address = newint.GetStringExtra("EXTRA_ADDRESS");
            //  SetContentView(Resource.Layout.Relay);


            //Layout stuff
            SetContentView(Resource.Layout.main2);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                if (previousItem != null)
                    previousItem.SetChecked(false);

                navigationView.SetCheckedItem(e.MenuItem.ItemId);

                previousItem = e.MenuItem;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home_1:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_home_2:
                        ListItemClicked(1);
                        break;
                } 

                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                navigationView.SetCheckedItem(Resource.Id.nav_home_1);
                ListItemClicked(0);
            }

            //END Layout Stuff

            //InitButtons();
            
            //new ConnectBT().Execute();
            //

           //System.Threading.Thread listener = new System.Threading.Thread(ListenForData);
           //listener.Start();
        }

        //private bool _listenerInitialized = false;
        //private void ListenForData()
        //{
        //    if (!_listenerInitialized)
        //    {
        //        _listenerInitialized = true;
        //        while (true)
        //        {
        //            try
        //            {
        //                bool bytesAvailable = btSocket.InputStream.IsDataAvailable();
        //                if (bytesAvailable)
        //                {
        //                    byte[] bytes = new byte[200];
        //                    btSocket.InputStream.Read(bytes, 0, bytes.Length);
        //                    var data = Encoding.UTF8.GetString(bytes);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //            System.Threading.Thread.Sleep(100);
        //        }
        //    }
        //}

        private void Disconnect(object sender, EventArgs e)
        {
            if (btSocket != null) //If the btSocket is busy
            {
                try
                {
                    btSocket.Close(); //close connection
                }
                catch (IOException)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
            Finish(); //return to the first layout
        }

        private class ConnectBT : AsyncTask
        {
            private bool ConnectSuccess = false;

            protected override void OnPreExecute()
            {
                //progress = ProgressDialog.Show(Application.Context, "Connecting...", "Please wait!!!");
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                while (!ConnectSuccess)
                {
                    try
                    {
                        if (btSocket == null || !isBtConnected)
                        {
                            myBluetooth = BluetoothAdapter.DefaultAdapter;//get the mobile bluetooth device
                            BluetoothDevice dispositivo = myBluetooth.GetRemoteDevice(address);//connects to the device's address and checks if it's available
                            btSocket = dispositivo.CreateInsecureRfcommSocketToServiceRecord(myUUID);//create a RFCOMM (SPP) connection
                            BluetoothAdapter.DefaultAdapter.CancelDiscovery();
                            btSocket.Connect();//start connection
                            ConnectSuccess = true;
                        }
                    }
                    catch (Exception)
                    {
                        ConnectSuccess = false;//if the try failed, you can check the exception here
                    }
                }
                return null;
            }

            protected override void OnPostExecute(Java.Lang.Object result)
            {
                base.OnPostExecute(result);

                if (!ConnectSuccess)
                {
                    //       Toast.MakeText(Application.Context, "Connection Failed. Is it a SPP Bluetooth? Try again.", ToastLength.Long).Show();
                }
                else
                {
                    //      Toast.MakeText(Application.Context, "Connected.", ToastLength.Long).Show();
                    isBtConnected = true;

                }
                //progress.Dismiss();
            }
        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = Fragment1.NewInstance();
                    break;
                case 1:
                    fragment = Fragment2.NewInstance();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}