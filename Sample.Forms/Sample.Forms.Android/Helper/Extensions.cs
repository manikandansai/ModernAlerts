using System;
using Android.App;
using Android.Graphics;
using Android.Util;

namespace ModernAlerts.Droid.Helper
{


    public enum AndroidUUDMaskType
    {
        None = 1,
        Clear = 2,
        Black = 3
    }

    public static class Extensions
    {

        public static Color ToNative(this System.Drawing.Color This) => new Color(This.R, This.G, This.B, This.A);


        //public static void RequestMainThread(Action action)
        //{
        //    if (Application.SynchronizationContext == SynchronizationContext.Current)
        //        action();
        //    else
        //        Application.SynchronizationContext.Post(x => {
        //            try
        //            {
        //                action();
        //            }
        //            catch { }
        //        }, null);
        //}

        //static readonly Dictionary<string, int> drawableList;

        //static PlatformBitmapLoader()
        //{
        //    // NB: This is some hacky shit, but on MonoAndroid at the moment,
        //    // this is always the entry assembly.
        //    var assm = AppDomain.CurrentDomain.GetAssemblies()[1];

        //    var resources = assm.GetModules().SelectMany(x => x.GetTypes()).First(x => x.Name == "Resource");

        //    drawableList = resources.GetNestedType("Drawable").GetFields()
        //        .Where(x => x.FieldType == typeof(int))
        //.ToDictionary(k => k.Name, v => (int)v.GetRawConstantValue());
        public static Bitmap LoadBitmap(string resourceName)
        {
            //new DrawableBitmap(res.GetDrawable(id));
            return null;
        }

        public static void SafeRunOnUi(this Activity activity, Action action) => activity.RunOnUiThread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log.Error("", ex.ToString());
            }
        });


        public static AndroidUUDMaskType ToNative(this MaskType maskType)
        {
            switch (maskType)
            {
                case MaskType.Black:
                    return AndroidUUDMaskType.Black;

                case MaskType.Clear:
                    return AndroidUUDMaskType.Clear;

                case MaskType.Gradient:
                    Console.WriteLine("Warning - Gradient mask type is not supported on android");
                    return AndroidUUDMaskType.Black;

                case MaskType.None:
                    return AndroidUUDMaskType.None;

                default:
                    throw new ArgumentException("Invalid Mask Type");
            }
        }
    }
}