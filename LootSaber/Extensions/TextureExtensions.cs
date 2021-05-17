using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


namespace LootSaber.Extensions
{
    static internal class TextureExtensions
    {
        public static Sprite FromEmbedded(string filename)
        {
            byte[] imgBytes;

            try
            {
                //https://stackoverflow.com/questions/10412401/how-to-read-an-embedded-resource-as-array-of-bytes-without-writing-it-to-disk
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                using (Stream resFilestream = a.GetManifestResourceStream(filename))
                {
                    if (resFilestream == null) return null;
                    imgBytes = new byte[resFilestream.Length];
                    resFilestream.Read(imgBytes, 0, imgBytes.Length);
                }

                Texture2D Tex2D = new Texture2D(2, 2);
                try
                {
                    Tex2D.LoadImage(imgBytes);
                }
                catch (Exception e) { Plugin.Log.Notice("Failed to load bytes into texture:"); Plugin.Log.Critical(e.ToString()); }

                return Sprite.Create(Tex2D, new Rect(0, 0, Tex2D.width, Tex2D.height), new Vector2(0, 0), 100.0f);



            }
            catch(Exception e) { Plugin.Log.Notice("Failed to load image from embedded resource:"); Plugin.Log.Critical(e.ToString()); }

            return null;

        }
    }
}
