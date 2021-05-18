using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        internal static void ShowPreviewAsset(DownloadRequestResponse downloadRequest, int position)
        {
            switch (downloadRequest.assetType)
            {
                case "Menu Text Font":
                    previewObjects.Add(ShowMenuFont(downloadRequest, position));
                    break;
                case "Saber":
                    previewObjects.Add(ShowSaber(downloadRequest, position));
                    break;
                case "Note":
                    previewObjects.Add(ShowNote(downloadRequest, position));
                    break;
            }
        }

        internal static GameObject ShowNote(DownloadRequestResponse downloadRequest, int position)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(downloadRequest.filePath);
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

        internal static GameObject ShowSaber(DownloadRequestResponse downloadRequest, int position)
        {
            AssetBundle ab = null;
            GameObject Sabers = null;
            try
            {
                ab = AssetBundle.LoadFromFile(downloadRequest.filePath);
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

        internal static GameObject ShowMenuFont(DownloadRequestResponse downloadRequest, int position)
        {
            AssetBundle fontBundle = AssetBundle.LoadFromFile(downloadRequest.filePath);
            GameObject textPrefab = fontBundle.LoadAsset<GameObject>("Text");
            GameObject textObj = GameObject.Instantiate(textPrefab);
            fontBundle.Unload(false);
            textObj.name = "LootSaberPreviewFont";
            textObj.SetActive(false);
            var Text = textObj.GetComponent<TextMeshPro>();
            Text.alignment = TextAlignmentOptions.Center;
            Text.fontSize = 2;
            Text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
            Text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
            Text.richText = true;
            textObj.transform.localScale *= 1.0f;
            Text.overflowMode = TextOverflowModes.Overflow;
            Text.enableWordWrapping = false;
            Text.text = "ABG";
            switch (position)
            {
                case 1:
                    textObj.transform.position = left;
                    textObj.gameObject.transform.SetParent(FloatingUI.PLS.transform);
                    break;
                case 2:
                    textObj.transform.position = middle;
                    textObj.gameObject.transform.SetParent(FloatingUI.PMS.transform);
                    break;
                case 3:
                    textObj.transform.position = right;
                    textObj.gameObject.transform.SetParent(FloatingUI.PRS.transform);
                    break;
            }
            textObj.SetActive(true);
            
            textObj.transform.SetParent(XP.XPScreen.Instance.transform);
            return textObj;
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
