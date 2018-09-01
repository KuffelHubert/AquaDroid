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
        private ProgressDialog[] tonh_Dialog = new ProgressDialog[7];
        private ProgressDialog[] tonm_Dialog = new ProgressDialog[7];
        private ProgressDialog[] tofh_Dialog = new ProgressDialog[7];
        private ProgressDialog[] tofm_Dialog = new ProgressDialog[7];
        private ProgressDialog[] przejDialog = new ProgressDialog[2];
        private ProgressDialog grzalkaWlaczDialog;
        private ProgressDialog grzalkaWylaczDialog;
        private bool _grzlkaWlaczIgnoreEvent;
        private bool _grzlkaWylaczIgnoreEvent;
        private bool[] _przejIgnoreEvent = new bool[2];
        private bool[] _tonhIgnoreEvent = new bool[7];
        private bool[] _tonmIgnoreEvent = new bool[7];
        private bool[] _tofmIgnoreEvent = new bool[7];
        private bool[] _tofhIgnoreEvent = new bool[7];
        private void InitButtons()
        {
            if (!_initialized)
            {
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


                var minutesAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.minutes_array, Android.Resource.Layout.SimpleSpinnerItem);
                minutesAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                var hourAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.hours_array, Android.Resource.Layout.SimpleSpinnerItem);
                hourAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                foreach (var spr in spinnerWlaczH)
                {
                    spr.Adapter = hourAdapter;
                }

                foreach (var spr in spinnerWylaczH)
                {
                    spr.Adapter = hourAdapter;
                }

                foreach (var spr in spinnerWlaczM)
                {
                    spr.Adapter = minutesAdapter;
                }

                foreach (var spr in spinnerWylaczM)
                {
                    spr.Adapter = minutesAdapter;
                }
                for (int i = 0; i < spinnerWylaczM.Length; i++)
                {
                    int currentIndex = i;
                    spinnerWylaczM[i].ItemSelected += (obj, args) =>
                    {
                        if (_initialized && !_tofmIgnoreEvent[currentIndex])
                        {
                            tofm_Dialog[currentIndex] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                            var index = currentIndex + 1;
                            SendToArduino("tofm" + index.ToString() + spinnerWylaczM[index - 1].SelectedItem);
                        }
                        _tofmIgnoreEvent[currentIndex] = false;
                    };
                    spinnerWylaczH[i].ItemSelected += (obj, args) =>
                    {
                        if (_initialized && !_tofhIgnoreEvent[currentIndex])
                        {
                            tofh_Dialog[currentIndex] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                            var index = currentIndex + 1;
                            SendToArduino("tofh" + index.ToString() + spinnerWylaczH[index - 1].SelectedItem);
                        }
                        _tofhIgnoreEvent[currentIndex] = false;
                    };
                    spinnerWlaczM[i].ItemSelected += (obj, args) =>
                    {
                        if (_initialized && !_tonmIgnoreEvent[currentIndex])
                        {
                            tonm_Dialog[currentIndex] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                            var index = currentIndex + 1;
                            SendToArduino("tonm" + index.ToString() + spinnerWlaczM[index - 1].SelectedItem);
                        }
                        _tonmIgnoreEvent[currentIndex] = false;
                    };
                    spinnerWlaczH[i].ItemSelected += (obj, args) =>
                    {
                        if (_initialized && !_tonhIgnoreEvent[currentIndex])
                        {
                            tonh_Dialog[currentIndex] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                            var index = currentIndex + 1;
                            SendToArduino("tonh" + index.ToString() + spinnerWlaczH[index - 1].SelectedItem);
                        }
                        _tonhIgnoreEvent[currentIndex] = false;
                    };
                }

                editTextPrzej[0] = (Spinner)FindViewById(Resource.Id.etprz2);
                editTextPrzej[1] = (Spinner)FindViewById(Resource.Id.etprz3);
                grzalkaWlacz = (Spinner)FindViewById(Resource.Id.wlgrzalka);
                grzalkaWylacz = (Spinner)FindViewById(Resource.Id.wylgrzalka);

                editTextPrzej[0].Adapter = minutesAdapter;
                editTextPrzej[1].Adapter = minutesAdapter;
                grzalkaWlacz.Adapter = minutesAdapter;
                grzalkaWylacz.Adapter = minutesAdapter;

                editTextPrzej[0].ItemSelected += RelayControl_ItemSelected; ;
                editTextPrzej[1].ItemSelected += RelayControl_ItemSelected1;

                grzalkaWlacz.ItemSelected += GrzalkaWlacz_ItemSelected; ;
                grzalkaWylacz.ItemSelected += GrzalkaWylacz_ItemSelected; ;
            }
        }

        private void GrzalkaWylacz_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (_initialized && !_grzlkaWylaczIgnoreEvent)
            {
                grzalkaWylaczDialog = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                var index = 0;
                SendToArduino("temf" + index.ToString() + grzalkaWylacz.SelectedItem);
            };
            _grzlkaWylaczIgnoreEvent = false;
        }

        private void GrzalkaWlacz_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (_initialized && !_grzlkaWlaczIgnoreEvent)
            {
                grzalkaWlaczDialog = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                var index = 0;
                SendToArduino("temo" + index.ToString() + grzalkaWlacz.SelectedItem);
            };
            _grzlkaWlaczIgnoreEvent = false;
        }

        private void RelayControl_ItemSelected1(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (_initialized && !_przejIgnoreEvent[1])
            {
                przejDialog[1] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                var index = 1 + 1;
                SendToArduino("tran" + index.ToString() + editTextPrzej[1].SelectedItem);
            };
            _przejIgnoreEvent[1] = false;
        }

        private void RelayControl_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (_initialized && !_przejIgnoreEvent[0])
            {
                przejDialog[0] = ProgressDialog.Show(this, "Zapisywanie, proszę czekać", "Czekaj!");
                var index = 0 + 1;
                string data = "tran" + index.ToString() + editTextPrzej[0].SelectedItem;
                SendToArduino(data);
            };
            _przejIgnoreEvent[0] = false;
        }
    }

}