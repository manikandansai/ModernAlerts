﻿using ModernAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Forms
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void DisplayAlert_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModernAlertsHelper.GetInstance().DisplayAlert(Color.Red, Color.White, "Modern Alerts Simple Alert", "Modern Alerts Simple Alert Body", "OK", null, (obj) =>
                {
                    //Logic
                });
            }
            catch(Exception ex)
            {

            }
        }
        private void DisplayConfirmation_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModernAlertsHelper.GetInstance().DisplayAlert(Color.Red, Color.White, "Modern Alert Confirmation Header", "Modern Alert Confirmation Body", "Yes", "No", (obj) =>
                {
                    if (obj == null) return;
                    //Logic
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void DisplayActionSheet_Clicked(object sender, EventArgs e)
        {
            try
            {
                string[] numbersarray = new string[] { "1", "2", "3", "4" };
                ModernAlertsHelper.GetInstance().ActionSheet(Color.Red, Color.White, "Modern Alerts ActionSheet", numbersarray, "Cancel", (obj) =>
                {
                    if (obj == null || (obj != null && obj == "Cancel")) return;
                    //Logic
                });
            }
            catch (Exception ex)
            {

            }
        }


    }
}