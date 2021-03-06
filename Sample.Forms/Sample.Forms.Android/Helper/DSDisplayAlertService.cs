﻿using System;
using System.Threading.Tasks;
using Android;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Telephony;
using Android.Text;
using Android.Views;
using Android.Widget;
using ModernAlerts;
using ModernAlerts.Droid;
using ModernAlerts.Droid.ProgressBar;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ModerAlertHelperService))]
namespace ModernAlerts.Droid
{
    public class ModerAlertHelperService : IShowDialogService
    {

        public ModerAlertHelperService()
        {
        }

        public async Task<bool> ActionSheet(Color backgroundcolor, Color fontColor, string title, string[] list, string cancelButton, Action<string> callback)
        {
            return await Task.Run(() => ShowActionSheet(backgroundcolor, fontColor, title, list, cancelButton, callback));
        }

        public async Task<bool> DisplayAlert(Color backgroundcolor, Color fontColor, string title, string content, string positiveButton, string negativeButton, string neutralbutton, Action<string> callback, bool isGetInput, InputConfig getinputConfig, bool iscontentleftalign)
        {
            return await Task.Run(() => Alert(backgroundcolor, fontColor, title, content, isGetInput, positiveButton, negativeButton, neutralbutton, callback, getinputConfig));
        }

        private bool ShowActionSheet(Color backgroundcolor, Color fontColor, string title, string[] content, string negativeButton, Action<string> callback)
        {
            bool alercon = false;
            if (ModernAlertsHelper.currentActivity == null)
            {
                throw new Exception("Call ModernAlertHelper.GetInstance().Init(this) in your MainActivity");
            }
            try
            {
                AlertDialog alertdialog = null;
                ModernAlertsHelper.currentActivity.RunOnUiThread(() =>
                {

                    AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(ModernAlertsHelper.currentActivity);
                    LayoutInflater inflater = ModernAlertsHelper.currentActivity.LayoutInflater;
                    var convertView = inflater.Inflate(Resource.Layout.customized_alertdialog, null);
                    dialogBuilder.SetView(convertView);
                    LinearLayout bv = (LinearLayout)convertView.FindViewById(Resource.Id.body_view);
                    LinearLayout listview_root = (LinearLayout)convertView.FindViewById(Resource.Id.listview_root);
                    listview_root.Visibility = ViewStates.Visible;
                    bv.Visibility = ViewStates.Gone;
                    Android.Widget.ListView lv = (Android.Widget.ListView)convertView.FindViewById(Resource.Id.listiview);
                    AlertDialogListViewAdapter adapter = new AlertDialogListViewAdapter(ModernAlertsHelper.currentActivity, content, backgroundcolor.ToAndroid(), fontColor.ToAndroid());
                    lv.Adapter = adapter;
                    lv.SetDrawSelectorOnTop(true);
                    lv.Divider = new ColorDrawable(Color.LightGray.ToAndroid());
                    lv.DividerHeight = 2;
                    var header = convertView.FindViewById<TextView>(Resource.Id.header);
                    var root = convertView.FindViewById<LinearLayout>(Resource.Id.root);
                    var lineheader = convertView.FindViewById(Resource.Id.lineheader);
                    lineheader.SetBackgroundColor(fontColor.ToAndroid());
                    var linebody = convertView.FindViewById(Resource.Id.linebody);
                    linebody.SetBackgroundColor(fontColor.ToAndroid());
                    //var leftSpacer = convertView.FindViewById<LinearLayout>(Resource.Id.leftSpacer);
                    //leftSpacer.Visibility = ViewStates.Visible;
                    var buttons = convertView.FindViewById<LinearLayout>(Resource.Id.buttons);
                    var button1 = convertView.FindViewById<Android.Widget.Button>(Resource.Id.positinvebutton);
                    header.SetTextColor(fontColor.ToAndroid());
                    header.Text = title;
                    buttons.SetBackgroundColor(backgroundcolor.ToAndroid());
                    root.SetBackgroundColor(backgroundcolor.ToAndroid());
                    button1.Visibility = ViewStates.Visible;
                    button1.Text = negativeButton;
                    button1.SetTextColor(fontColor.ToAndroid());
                    button1.Click += ((si, e) =>
                    {
                        callback(negativeButton);
                        alertdialog.Dismiss();
                    });
                    lv.ItemClick += ((sd, w) =>
                    {
                        callback(content[w.Position]);
                        alertdialog.Dismiss();
                    });
                    lv.ItemSelected += ((sd, w) =>
                    {
                        callback(content[w.Position]);
                        alertdialog.Dismiss();
                    });
                    alertdialog = dialogBuilder.Create();
                    alertdialog.Show();
                    ShowShadow(alertdialog);
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return alercon;
        }


        private bool Alert(Color backgroundcolor, Color fontColor, string title, string content, bool isGetInput, string positiveButton, string negativeButton, string neutralButton, Action<string> callback, InputConfig config)
        {
            bool alercon = false;
            if (ModernAlertsHelper.currentActivity == null)
            {
                throw new Exception("Call ModernAlertHelper.GetInstance().Init(this) in your MainActivity");
            }
            try
            {
                AlertDialog alertdialog = null;
                ModernAlertsHelper.currentActivity.RunOnUiThread(() =>
                {
                    AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(ModernAlertsHelper.currentActivity);

                    LayoutInflater inflater = ModernAlertsHelper.currentActivity.LayoutInflater;
                    var alertLayout = inflater.Inflate(Resource.Layout.customized_alertdialog, null);
                    dialogBuilder.SetView(alertLayout);
                    var header = alertLayout.FindViewById<TextView>(Resource.Id.header);
                    LinearLayout bv = (LinearLayout)alertLayout.FindViewById(Resource.Id.body_view);
                    var lineheader = alertLayout.FindViewById(Resource.Id.lineheader);
                    lineheader.SetBackgroundColor(fontColor.ToAndroid());
                    var linebody = alertLayout.FindViewById(Resource.Id.linebody);
                    linebody.SetBackgroundColor(fontColor.ToAndroid());
                    var root = alertLayout.FindViewById<LinearLayout>(Resource.Id.root);
                    var body = alertLayout.FindViewById<TextView>(Resource.Id.body);
                    var buttons = alertLayout.FindViewById<LinearLayout>(Resource.Id.buttons);
                    var button1 = alertLayout.FindViewById<Android.Widget.Button>(Resource.Id.positinvebutton);
                    var getinput_et = alertLayout.FindViewById<Android.Widget.EditText>(Resource.Id.getinput_et);

                    getinput_et.SetHintTextColor(Color.Gray.ToAndroid());

                    header.Text = title;
                    if (string.IsNullOrEmpty(content))
                    {
                        bv.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        body.Text = content;
                    }
                    header.SetTextColor(fontColor.ToAndroid());
                    body.SetTextColor(fontColor.ToAndroid());
                    buttons.SetBackgroundColor(backgroundcolor.ToAndroid());
                    root.SetBackgroundColor(backgroundcolor.ToAndroid());
                    button1.Visibility = ViewStates.Visible;
                    button1.Text = positiveButton;
                    button1.SetTextColor(fontColor.ToAndroid());
                    button1.Click += ((si, e) =>
                    {
                        if (isGetInput)
                        {
                            if (!string.IsNullOrEmpty(getinput_et.Text))
                            {
                                callback(getinput_et.Text);
                                alertdialog.Dismiss();
                            }
                        }
                        else
                        {
                            callback(positiveButton);
                            alertdialog.Dismiss();
                        }
                    });
                    if (isGetInput)
                    {
                        if (config != null && config.MinLength != 0)
                        {
                            //button1.Background.Alpha = 100;
                            button1.Enabled = false;
                            getinput_et.TextChanged += (w, me) =>
                            {
                                if (getinput_et.Text.Trim().Length >= config.MinLength)
                                {
                                    button1.Enabled = true;
                                    //button1.Background.Alpha = 0;
                                    button1.SetTextColor(fontColor.ToAndroid());
                                }
                                else
                                {
                                    //button1.Background.Alpha = 50;
                                    button1.SetTextColor(Color.Gray.ToAndroid());
                                    button1.Enabled = false;
                                }
                            };
                        }
                    }

                    if (isGetInput)
                    {
                        var getinputll = alertLayout.FindViewById<LinearLayout>(Resource.Id.get_input_view);

                        if (config != null)
                        {
                            getinput_et.SetBackgroundColor(config.BackgroundColor.ToAndroid());
                            getinput_et.SetTextColor(config.FontColor.ToAndroid());
                            getinput_et.Hint = config.placeholder; 
                            if (config.keyboard == Keyboard.Telephone)
                            {
                                getinput_et.InputType = InputTypes.ClassPhone;
                            }
                            else if (config.keyboard == Keyboard.Numeric)
                            {
                                getinput_et.InputType = InputTypes.ClassNumber;
                            }
                            else if (config.keyboard == Keyboard.Text)
                            {
                                if (config.isMultipleLine)
                                {
                                    setMultiLine(getinput_et);
                                }
                                else
                                {
                                    getinput_et.InputType = InputTypes.ClassText;
                                } 
                            }
                            else if (config.keyboard == Keyboard.Email)
                            {
                                getinput_et.InputType = InputTypes.TextVariationEmailAddress;
                            }  
                            if (config.MaxLength != 0)
                                getinput_et.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(config.MaxLength) });
                        }
                        getinputll.Visibility = ViewStates.Visible;
                        if(string.IsNullOrEmpty(content))
                        {
                            LinearLayout.LayoutParams ll = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
                            ll.SetMargins(0, 30, 0, 0);
                            getinputll.LayoutParameters=ll;
                        }
                    }
                    if (negativeButton != null)
                    {
                        var button2 = alertLayout.FindViewById<Android.Widget.Button>(Resource.Id.negativebutton);
                        button2.Visibility = ViewStates.Visible;
                        button2.Text = negativeButton;
                        button2.SetTextColor(fontColor.ToAndroid());
                        button2.Click += ((si, e) =>
                        {
                            callback(negativeButton);
                            alertdialog.Dismiss();
                        });

                        var second_separator_line = alertLayout.FindViewById(Resource.Id.second_separator_line);
                        second_separator_line.SetBackgroundColor(fontColor.ToAndroid());
                        second_separator_line.Visibility = ViewStates.Visible;
                    }
                    if (neutralButton != null)
                    {
                        var button3 = alertLayout.FindViewById<Android.Widget.Button>(Resource.Id.neutralbutton);
                        button3.Visibility = ViewStates.Visible;
                        button3.Text = neutralButton;
                        button3.SetTextColor(fontColor.ToAndroid());
                        button3.Click += ((si, e) =>
                        {
                            callback(neutralButton);
                            alertdialog.Dismiss();
                        });
                        var first_separator_line = alertLayout.FindViewById(Resource.Id.first_separator_line);
                        first_separator_line.SetBackgroundColor(fontColor.ToAndroid());
                        first_separator_line.Visibility = ViewStates.Visible;
                    }
                    alertdialog = dialogBuilder.Create();
                    if (isGetInput)
                    {
                        alertdialog.Window.SetSoftInputMode(SoftInput.StateAlwaysVisible);
                    }
                    alertdialog.Show();
                    if (config!=null&&!string.IsNullOrEmpty(config.DefaultValue))
                    {
                        getinput_et.Text = config.DefaultValue;
                        getinput_et.SetSelection(getinput_et.Text.Length);
                    }
                    ShowShadow(alertdialog);
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return alercon;
        }

        void setMultiLine(EditText getinput_et)
        {
            try
            {
                if (getinput_et == null) return;
                getinput_et.InputType = InputTypes.TextFlagMultiLine;
                getinput_et.SetHorizontallyScrolling(false); 
                getinput_et.SetLines(3);
            }
            catch(Exception e)
            {

            }
        }
        public ISpanned fromHtml(String source)
        {
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
            {
                return Html.FromHtml(source, FromHtmlOptions.ModeCompact);
            }
            else
            {
                return Html.FromHtml(source);
            }
        }
        public void ShowLoading(string title, bool show, string cancelText, MaskType? maskType, Action onCancel)
        {
            try
            {
                var config = new ProgressDialogConfig
                {
                    Title = title ?? ProgressDialogConfig.DefaultTitle,
                    AutoShow = show,
                    CancelText = cancelText ?? ProgressDialogConfig.DefaultCancelText,
                    MaskType = maskType ?? ProgressDialogConfig.DefaultMaskType,
                    IsDeterministic = true,
                    OnCancel = onCancel
                };

                ProgressDialog pr = new ProgressDialog(config, ModernAlertsHelper.currentActivity);
                pr.Show();
            }
            catch (Exception e)
            {

            }
        }

        public void ShowShadow(AlertDialog alertdialog)
        {
            try
            {
                var lp = alertdialog.Window.Attributes;
                lp.DimAmount = 0.8f;
                alertdialog.Window.Attributes = lp;
                alertdialog.Window.AddFlags(WindowManagerFlags.BlurBehind);
            }
            catch (Exception e)
            {

            }
        }

    }
}