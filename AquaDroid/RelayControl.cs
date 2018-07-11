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

namespace AquaDroid
{
    [Activity(Label = "RelayControl", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public partial class RelayControl : Activity
    {
        ToggleButton[] toggleButtonsSter = new ToggleButton[8];
        EditText grzalkaWlacz;
        EditText grzalkaWylacz;
        Spinner[] spinnerWlaczH = new Spinner[7];
        Spinner[] spinnerWlaczM = new Spinner[7];
        Spinner[] spinnerWylaczH = new Spinner[7];
        Spinner[] spinnerWylaczM = new Spinner[7];
        EditText[] editTextPrzej = new EditText[2];
        ToggleButton[] toggleButtonsStan = new ToggleButton[8];

        static String address = null;
        static BluetoothAdapter myBluetooth = null;
        static BluetoothSocket btSocket = null;
        private static bool isBtConnected = false;
        static readonly UUID myUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Intent newint = Intent;
            address = newint.GetStringExtra("EXTRA_ADDRESS");
            SetContentView(Resource.Layout.Relay);
            InitButtons();
            
            new ConnectBT().Execute();

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
    }
}