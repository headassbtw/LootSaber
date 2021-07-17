using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LootSaber.Files;
using TMPro;
using UnityEngine;
using static LootSaber.CustomTypes;

namespace LootSaber.UI.Asset_Viewing
{
    class AssetInstantiatePreviewing
    {
        internal static Vector3 left = new Vector3(-0.65f, 1.5f, 4.0f);
        internal static Vector3 middle = new Vector3(0, 1.5f, 4.0f);
        internal static Vector3 right = new Vector3(0.65f, 1.5f, 4.0f);
        static List<GameObject> previewObjects = new List<GameObject>();

        internal static void ShowPreviewAsset(ModelSaber.ModelSaberEntry asset, int position)
        {
            var prv = Downloading.DownloadModelAsPreview(asset);
            while (!prv.IsCompleted)
                Thread.Sleep(10);
            switch (asset.Type.ToLower())
            {
                
                case "saber":
                    previewObjects.Add(ShowSaber(prv.Result, position));
                    break;
                case "bloq":
                    previewObjects.Add(ShowNote(prv.Result, position));
                    break;
            }
        }

        internal static GameObject ShowNote(AssetBundle bundle, int position)
        {
            AssetBundle ab = bundle;
            GameObject note = ab.LoadAsset<GameObject>("assets/_customnote.prefab");
            ab.Unload(false);
            var NoteRight = note.transform.Find("NoteRight").gameObject;
            GameObject nnote = GameObject.Instantiate(NoteRight);
            nnote.name = "LootSaberNotePreview";
            nnote.transform.localScale *= 0.4f;
            switch (position)
            {
                case 1:
                    nnote.gameObject.transform.SetPositionAndRotation(left, Quaternion.Euler(0, 270, 0));
                    nnote.transform.SetParent(FloatingUI.PLS.transform);
                    break;
                case 2:
                    nnote.gameObject.transform.SetPositionAndRotation(middle, Quaternion.Euler(0, 270, 0));
                    nnote.transform.SetParent(FloatingUI.PMS.transform);
                    break;
                case 3:
                    nnote.gameObject.transform.SetPositionAndRotation(right, Quaternion.Euler(0, 270, 0));
                    nnote.transform.SetParent(FloatingUI.PRS.transform);
                    break;
            }
            return nnote;
        }

        internal static GameObject ShowSaber(AssetBundle bundle, int position)
        {
            AssetBundle ab = null;
            GameObject Sabers = null;
            try
            {
                ab = bundle;
                Sabers = ab.LoadAsset<GameObject>("_CustomSaber");
                //THIS IS HOW YOU DO SHIT WITH ASSETBUNDLES YOU FUCK
                ab.Unload(false);
                //HOW HARD WAS THAT? HUH? HUH?
            }
            catch (Exception)
            { //fuck you CustomSabers for not unloading your fucking assetbundles
            }
            GameObject saber = GameObject.Instantiate(Sabers);

            saber.name = "LootSaberPreviewSaber";
            GameObject.Destroy(saber.transform.Find("RightSaber").gameObject);
            switch (position)
            {
                case 1:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(left, Quaternion.Euler(270, 0, 0));
                    saber.gameObject.transform.SetParent(FloatingUI.PLS.transform);
                    break;
                case 2:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(middle, Quaternion.Euler(270, 0, 0));
                    saber.gameObject.transform.SetParent(FloatingUI.PMS.transform);
                    break;
                case 3:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(right, Quaternion.Euler(270, 0, 0));
                    saber.gameObject.transform.SetParent(FloatingUI.PRS.transform);
                    break;
            }
            
            
            saber.transform.SetParent(XP.XPScreen.Instance.transform);
            return saber;
        }

        
        internal static void yeetem()
        {
            try
            {
                //fuck you
                GameObject.Destroy(GameObject.Find("LootSaberPreviewSaber"));
            }
            catch (Exception e) { Plugin.Log.Critical(e.ToString()); }

            foreach(GameObject asset in previewObjects)
            {
                GameObject.Destroy(asset);
            }
        }
    }
}
