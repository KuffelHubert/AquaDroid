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
        Spinner grzalkaWlacz;
        Spinner grzalkaWylacz;
        Spinner[] spinnerWlaczH = new Spinner[7];
        Spinner[] spinnerWlaczM = new Spinner[7];
        Spinner[] spinnerWylaczH = new Spinner[7];
        Spinner[] spinnerWylaczM = new Spinner[7];
        Spinner[] editTextPrzej = new Spinner[2];
        ProgressDialog progress;

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

            var connector = new ConnectBT();
            connector.StartedExecution += Connector_StartedExecution;
            connector.EndedExecution += Connector_EndedExecution;
            connector.Execute();

            System.Threading.Thread listener = new System.Threading.Thread(ListenForData);
            listener.Start();
        }

        private void Connector_EndedExecution()
        {
            SendToArduino("data111");
        }

        private void Connector_StartedExecution()
        {
            progress = ProgressDialog.Show(this, "Connecting...", "Please wait!!!");
        }

        private bool _listenerInitialized = false;
        private void ListenForData()
        {
            if (!_listenerInitialized)
            {
                _listenerInitialized = true;
                while (true)
                {
                    try
                    {
                        if (btSocket != null)
                        {
                            bool bytesAvailable = btSocket.InputStream.IsDataAvailable();
                            if (bytesAvailable)
                            {
                                SystemClock.Sleep(100);
                                byte[] bytes = new byte[2048];
                                btSocket.InputStream.Read(bytes, 0, bytes.Length);
                                var data = Encoding.UTF8.GetString(bytes);
                                RunOnUiThread(() => { ProcessReceivedData(data); });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RunOnUiThread(() => { Toast.MakeText(Application.Context, ex.Message, ToastLength.Short).Show(); });
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void SendToArduino(string data)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes(data);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Short).Show();
                }
            }
        }

        private void ProcessReceivedData(string data)
        {
            string action = data.Substring(0, 4);
            data = data.Substring(4);
            int index = 0;
            string val = "";
            switch (action)
            {
                case "data":
                    var values = data.Split(';');
                    Toast.MakeText(Application.Context, "Godzina: " + values[DataCursor], ToastLength.Long).Show();
                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 0)
                        {
                            val = values[DataCursor];
                            if ((string)grzalkaWlacz.SelectedItem != val)
                            {
                                _grzlkaWlaczIgnoreEvent = true;
                            }
                            grzalkaWlacz.SetSelection(GetIndex(grzalkaWlacz, val),false);
                            val = values[DataCursor];
                            if ((string)grzalkaWylacz.SelectedItem != val)
                            {
                                _grzlkaWylaczIgnoreEvent = true;
                            }
                            grzalkaWylacz.SetSelection(GetIndex(grzalkaWylacz, val), false);
                        }
                        else
                        {
                            val = values[DataCursor];
                            if ((string)spinnerWlaczH[i - 1].SelectedItem != val)
                            {
                                _tonhIgnoreEvent[i - 1] = true;
                            }
                            spinnerWlaczH[i - 1].SetSelection(GetIndex(spinnerWlaczH[i - 1], val), false);

                            val = values[DataCursor];
                            if ((string)spinnerWlaczM[i - 1].SelectedItem != val)
                            {
                                _tonmIgnoreEvent[i - 1] = true;
                            }
                            spinnerWlaczM[i - 1].SetSelection(GetIndex(spinnerWlaczM[i - 1], val), false);

                            val = values[DataCursor];
                            if ((string)spinnerWylaczH[i - 1].SelectedItem != val)
                            {
                                _tofhIgnoreEvent[i - 1] = true;
                            }
                            spinnerWylaczH[i - 1].SetSelection(GetIndex(spinnerWylaczH[i - 1], val), false);

                            val = values[DataCursor];
                            if ((string)spinnerWylaczM[i - 1].SelectedItem != val)
                            {
                                _tofmIgnoreEvent[i - 1] = true;
                            }

                            spinnerWylaczM[i - 1].SetSelection(GetIndex(spinnerWylaczM[i - 1], val), false);
                            if (i == 1 || i == 2)
                            {
                                val = values[DataCursor];
                                val = ((int.Parse(val) * 180 / 60) - 1).ToString();
                                if ((string)editTextPrzej[i - 1].SelectedItem != val)
                                {
                                    _przejIgnoreEvent[i - 1] = true;
                                }
                                editTextPrzej[i - 1].SetSelection(GetIndex(editTextPrzej[i-1], val),false);
                            }
                        }
                    }
                    progress.Dismiss();
                    _initialized = true;
                    break;
                case "tonh":
                    index = int.Parse(data.Substring(0, 1));
                    val = int.Parse(data.Substring(1)).ToString();
                    if ((string)spinnerWlaczH[index - 1].SelectedItem != val)
                    {
                        _tonhIgnoreEvent[index - 1] = true;
                    }
                    spinnerWlaczH[index - 1].SetSelection(GetIndex(spinnerWlaczH[index - 1], val), false);
                    tonh_Dialog[index - 1].Dismiss();
                    break;
                case "tonm":
                    index = int.Parse(data.Substring(0, 1));
                    val = int.Parse(data.Substring(1)).ToString();
                    if ((string)spinnerWlaczM[index - 1].SelectedItem != val)
                    {
                        _tonmIgnoreEvent[index - 1] = true;
                    }
                    spinnerWlaczM[index - 1].SetSelection(GetIndex(spinnerWlaczM[index - 1], val), false);
                    tonm_Dialog[index - 1].Dismiss();
                    break;
                case "tofh":
                    index = int.Parse(data.Substring(0, 1));
                    val = int.Parse(data.Substring(1)).ToString();
                    if ((string)spinnerWylaczH[index - 1].SelectedItem != val)
                    {
                        _tofhIgnoreEvent[index - 1] = true;
                    }
                    spinnerWylaczH[index - 1].SetSelection(GetIndex(spinnerWylaczH[index - 1], val), false);
                    tofh_Dialog[index - 1].Dismiss();
                    break;
                case "tofm":
                    index = int.Parse(data.Substring(0, 1));
                    val = int.Parse(data.Substring(1)).ToString();
                    if ((string)spinnerWylaczM[index - 1].SelectedItem != val)
                    {
                        _tofmIgnoreEvent[index - 1] = true;
                    }
                    spinnerWylaczM[index - 1].SetSelection(GetIndex(spinnerWylaczM[index - 1], val),false);
                    tofm_Dialog[index - 1].Dismiss();
                    break;
                case "temo":
                    val = int.Parse(data).ToString();
                    if ((string)grzalkaWlacz.SelectedItem != val)
                    {
                        _grzlkaWlaczIgnoreEvent = true;
                    }
                    grzalkaWlacz.SetSelection(GetIndex(grzalkaWlacz, val), false);
                    grzalkaWlaczDialog.Dismiss();
                    break;
                case "temf":
                    val = int.Parse(data).ToString();
                    if ((string)grzalkaWylacz.SelectedItem != val)
                    {
                        _grzlkaWylaczIgnoreEvent = true;
                    }
                    grzalkaWylacz.SetSelection(GetIndex(grzalkaWylacz, val), false);
                    grzalkaWylaczDialog.Dismiss();
                    break;
                case "tran":
                    var maxPVM = 180;
                    index = int.Parse(data.Substring(0, 1));
                    val = ((int.Parse(data.Substring(1)) * maxPVM / 60) - 1).ToString();
                    if ((string)editTextPrzej[index - 1].SelectedItem != val)
                    {
                        _przejIgnoreEvent[index - 1] = true;
                    }
                    editTextPrzej[index - 1].SetSelection(GetIndex(editTextPrzej[index - 1], val), false);
                    przejDialog[index - 1].Dismiss();
                    break;
                default:
                    break;
            }
        }

        private int GetIndex(Spinner spinner, String myString)
        {
            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(myString, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return 0;
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
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
            Finish(); //return to the first layout
        }

        private int _dataCursor;
        private int DataCursor
        {
            get
            {
                return _dataCursor++;
            }
            set
            {
                DataCursor = value;
            }
        }

        private class ConnectBT : AsyncTask
        {
            private bool ConnectSuccess = false;

            protected override void OnPreExecute()
            {
                OnStatedExecution();
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
                    Toast.MakeText(Application.Context, "Connection Failed. Is it a SPP Bluetooth? Try again.", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(Application.Context, "Connected.", ToastLength.Long).Show();
                    isBtConnected = true;

                }
                OnEndedExecution();
            }

            public event Action StartedExecution;
            public void OnStatedExecution()
            {
                StartedExecution?.Invoke();
            }

            public event Action EndedExecution;
            public void OnEndedExecution()
            {
                EndedExecution?.Invoke();
            }
        }
    }
}