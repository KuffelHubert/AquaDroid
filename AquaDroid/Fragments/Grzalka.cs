using Android;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.IO;

namespace AquaDroid.Fragments
{
    public class Grzalka : Fragment
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

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Intent newint = this.Activity.Intent;
            address = newint.GetStringExtra("EXTRA_ADDRESS");

            new ConnectBT().Execute();
        }

        public static Grzalka NewInstance()
        {
            var grzalka = new Grzalka { Arguments = new Bundle() };
            return grzalka;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.frag_Grzalka, null);
        }

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
                    Toast.MakeText(Activity.Application.ApplicationContext, "Error.", ToastLength.Long).Show();
                }
            }
            //Finish(); //return to the first layout
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