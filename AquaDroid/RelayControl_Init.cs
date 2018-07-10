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

namespace AquaDroid
{
    public partial class RelayControl
    {
        private bool _initialized;
        private void InitButtons()
        {
            if (!_initialized)
            {
                _initialized = true;
                toggleButtonsSter[0] = (ToggleButton)FindViewById(Resource.Id.ster1);
                toggleButtonsSter[1] = (ToggleButton)FindViewById(Resource.Id.ster2);
                toggleButtonsSter[2] = (ToggleButton)FindViewById(Resource.Id.ster3);
                toggleButtonsSter[3] = (ToggleButton)FindViewById(Resource.Id.ster4);
                toggleButtonsSter[4] = (ToggleButton)FindViewById(Resource.Id.ster5);
                toggleButtonsSter[5] = (ToggleButton)FindViewById(Resource.Id.ster6);
                toggleButtonsSter[6] = (ToggleButton)FindViewById(Resource.Id.ster7);
                toggleButtonsSter[7] = (ToggleButton)FindViewById(Resource.Id.ster8);

                toggleButtonsStan[0] = (ToggleButton)FindViewById(Resource.Id.stan1);
                toggleButtonsStan[1] = (ToggleButton)FindViewById(Resource.Id.stan2);
                toggleButtonsStan[2] = (ToggleButton)FindViewById(Resource.Id.stan3);
                toggleButtonsStan[3] = (ToggleButton)FindViewById(Resource.Id.stan4);
                toggleButtonsStan[4] = (ToggleButton)FindViewById(Resource.Id.stan5);
                toggleButtonsStan[5] = (ToggleButton)FindViewById(Resource.Id.stan6);
                toggleButtonsStan[6] = (ToggleButton)FindViewById(Resource.Id.stan7);
                toggleButtonsStan[7] = (ToggleButton)FindViewById(Resource.Id.stan8);

                spinnerWlaczH[0] = (Spinner)FindViewById(Resource.Id.sprwlh2);
                spinnerWlaczH[1] = (Spinner)FindViewById(Resource.Id.sprwlh3);
                spinnerWlaczH[2] = (Spinner)FindViewById(Resource.Id.sprwlh4);
                spinnerWlaczH[3] = (Spinner)FindViewById(Resource.Id.sprwlh5);
                spinnerWlaczH[4] = (Spinner)FindViewById(Resource.Id.sprwlh6);
                spinnerWlaczH[5] = (Spinner)FindViewById(Resource.Id.sprwlh7);
                spinnerWlaczH[6] = (Spinner)FindViewById(Resource.Id.sprwlh8);

                spinnerWlaczM[0] = (Spinner)FindViewById(Resource.Id.sprwlm2);
                spinnerWlaczM[1] = (Spinner)FindViewById(Resource.Id.sprwlm3);
                spinnerWlaczM[2] = (Spinner)FindViewById(Resource.Id.sprwlm4);
                spinnerWlaczM[3] = (Spinner)FindViewById(Resource.Id.sprwlm5);
                spinnerWlaczM[4] = (Spinner)FindViewById(Resource.Id.sprwlm6);
                spinnerWlaczM[5] = (Spinner)FindViewById(Resource.Id.sprwlm7);
                spinnerWlaczM[6] = (Spinner)FindViewById(Resource.Id.sprwlm8);

                spinnerWylaczH[0] = (Spinner)FindViewById(Resource.Id.sprwylh2);
                spinnerWylaczH[1] = (Spinner)FindViewById(Resource.Id.sprwylh3);
                spinnerWylaczH[2] = (Spinner)FindViewById(Resource.Id.sprwylh4);
                spinnerWylaczH[3] = (Spinner)FindViewById(Resource.Id.sprwylh5);
                spinnerWylaczH[4] = (Spinner)FindViewById(Resource.Id.sprwylh6);
                spinnerWylaczH[5] = (Spinner)FindViewById(Resource.Id.sprwylh7);
                spinnerWylaczH[6] = (Spinner)FindViewById(Resource.Id.sprwylh8);

                spinnerWylaczM[0] = (Spinner)FindViewById(Resource.Id.sprwylm2);
                spinnerWylaczM[1] = (Spinner)FindViewById(Resource.Id.sprwylm3);
                spinnerWylaczM[2] = (Spinner)FindViewById(Resource.Id.sprwylm4);
                spinnerWylaczM[3] = (Spinner)FindViewById(Resource.Id.sprwylm5);
                spinnerWylaczM[4] = (Spinner)FindViewById(Resource.Id.sprwylm6);
                spinnerWylaczM[5] = (Spinner)FindViewById(Resource.Id.sprwylm7);
                spinnerWylaczM[6] = (Spinner)FindViewById(Resource.Id.sprwylm8);

                editTextPrzej[0] = (EditText)FindViewById(Resource.Id.etprz2);
                editTextPrzej[1] = (EditText)FindViewById(Resource.Id.etprz3);
                grzalkaWlacz = (EditText)FindViewById(Resource.Id.wlgrzalka);
                grzalkaWylacz = (EditText)FindViewById(Resource.Id.wylgrzalka);

                toggleButtonsStan[0].CheckedChange += RelayControl_CheckedChange1;
                toggleButtonsStan[1].CheckedChange += RelayControl_CheckedChange2;
                toggleButtonsStan[2].CheckedChange += RelayControl_CheckedChange3;
                toggleButtonsStan[3].CheckedChange += RelayControl_CheckedChange4;
                toggleButtonsStan[4].CheckedChange += RelayControl_CheckedChange5;
                toggleButtonsStan[5].CheckedChange += RelayControl_CheckedChange6;
                toggleButtonsStan[6].CheckedChange += RelayControl_CheckedChange7;
                toggleButtonsStan[7].CheckedChange += RelayControl_CheckedChange8;

                toggleButtonsSter[0].CheckedChange += RelayControl_CheckedChange9;
                toggleButtonsSter[1].CheckedChange += RelayControl_CheckedChange10;
                toggleButtonsSter[2].CheckedChange += RelayControl_CheckedChange11;
                toggleButtonsSter[3].CheckedChange += RelayControl_CheckedChange12;
                toggleButtonsSter[4].CheckedChange += RelayControl_CheckedChange13;
                toggleButtonsSter[5].CheckedChange += RelayControl_CheckedChange14;
                toggleButtonsSter[6].CheckedChange += RelayControl_CheckedChange15;
                toggleButtonsSter[7].CheckedChange += RelayControl_CheckedChange16;

                spinnerWlaczH[0].ItemSelected += RelayControl_ItemSelected;
                spinnerWlaczH[1].ItemSelected += RelayControl_ItemSelected1;
                spinnerWlaczH[2].ItemSelected += RelayControl_ItemSelected2;
                spinnerWlaczH[3].ItemSelected += RelayControl_ItemSelected3;
                spinnerWlaczH[4].ItemSelected += RelayControl_ItemSelected4;
                spinnerWlaczH[5].ItemSelected += RelayControl_ItemSelected5;
                spinnerWlaczH[6].ItemSelected += RelayControl_ItemSelected6;

                spinnerWlaczM[0].ItemSelected += RelayControl_ItemSelected7;
                spinnerWlaczM[1].ItemSelected += RelayControl_ItemSelected8;
                spinnerWlaczM[2].ItemSelected += RelayControl_ItemSelected9;
                spinnerWlaczM[3].ItemSelected += RelayControl_ItemSelected10;
                spinnerWlaczM[4].ItemSelected += RelayControl_ItemSelected11;
                spinnerWlaczM[5].ItemSelected += RelayControl_ItemSelected12;
                spinnerWlaczM[6].ItemSelected += RelayControl_ItemSelected13;

                spinnerWylaczH[0].ItemSelected += RelayControl_ItemSelected14;
                spinnerWylaczH[1].ItemSelected += RelayControl_ItemSelected15;
                spinnerWylaczH[2].ItemSelected += RelayControl_ItemSelected16;
                spinnerWylaczH[3].ItemSelected += RelayControl_ItemSelected17;
                spinnerWylaczH[4].ItemSelected += RelayControl_ItemSelected18;
                spinnerWylaczH[5].ItemSelected += RelayControl_ItemSelected19;
                spinnerWylaczH[6].ItemSelected += RelayControl_ItemSelected20;

                spinnerWylaczM[0].ItemSelected += RelayControl_ItemSelected21;
                spinnerWylaczM[1].ItemSelected += RelayControl_ItemSelected22;
                spinnerWylaczM[2].ItemSelected += RelayControl_ItemSelected23;
                spinnerWylaczM[3].ItemSelected += RelayControl_ItemSelected24;
                spinnerWylaczM[4].ItemSelected += RelayControl_ItemSelected25;
                spinnerWylaczM[5].ItemSelected += RelayControl_ItemSelected26;
                spinnerWylaczM[6].ItemSelected += RelayControl_ItemSelected27;

                editTextPrzej[0].TextChanged += RelayControl_TextChanged;
                editTextPrzej[1].TextChanged += RelayControl_TextChanged1;


                foreach (var spr in spinnerWlaczH)
                {
                    ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.hours_array, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spr.Adapter = adapter;
                }

                foreach (var spr in spinnerWylaczH)
                {
                    ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.hours_array, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spr.Adapter = adapter;
                }

                foreach (var spr in spinnerWlaczM)
                {
                    ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.minutes_array, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spr.Adapter = adapter;
                }

                foreach (var spr in spinnerWylaczM)
                {
                    ArrayAdapter adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.minutes_array, Android.Resource.Layout.SimpleSpinnerItem);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spr.Adapter = adapter;
                }
            }
        }
        private void RelayControl_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 0 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tran" + index.ToString() + editTextPrzej[0].Text);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_TextChanged1(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 1 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tran" + index.ToString() + editTextPrzej[1].Text);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected21(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 0 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected27(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 6 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected22(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 1 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected23(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 2 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected24(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 3 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected25(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 4 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected26(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 5 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected14(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 0 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected15(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 1 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected16(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 2 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected17(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 3 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected18(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 4 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected19(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 5 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected20(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 6 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected7(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 0 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected8(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 1 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected9(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 2 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected10(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 3 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected11(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 4 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected12(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 5 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected13(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 6 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 0 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected1(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 1 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 2 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected3(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 3 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected4(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 4 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected5(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 5 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_ItemSelected6(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var index = 6 + 1;
                    var bytes = Encoding.UTF8.GetBytes("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange9(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 0);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange10(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 1);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange11(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 2);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange12(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 3);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange13(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 4);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange14(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 5);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange15(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 6);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange16(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes("ster" + 7);
                    btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange1(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan01");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan00");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange2(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan11");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan10");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange3(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan21");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan20");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange4(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan31");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan30");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange5(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan41");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan40");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange6(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan51");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan50");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange7(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan61");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan60");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }

        private void RelayControl_CheckedChange8(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (btSocket != null && isBtConnected == true)
            {
                try
                {
                    if (e.IsChecked)
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan71");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = Encoding.UTF8.GetBytes("stan70");
                        btSocket.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Error.", ToastLength.Long).Show();
                }
            }
        }
    }

}