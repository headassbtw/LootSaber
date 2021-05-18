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
        static Vector3 left = new Vector3(-0.67f, 0.87f, 3.5f);
        static Vector3 middle = new Vector3(0, 0.87f, 3.5f);
        static Vector3 right = new Vector3(0.49f, 0.87f, 3.5f);
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
            nnote.transform.localScale *= 0.4f;
            switch (position)
            {
                case 1:
                    nnote.gameObject.transform.SetPositionAndRotation(left, Quaternion.Euler(0, 270, 0));
                    break;
                case 2:
                    nnote.gameObject.transform.SetPositionAndRotation(middle, Quaternion.Euler(0, 270, 0));
                    break;
                case 3:
                    nnote.gameObject.transform.SetPositionAndRotation(right, Quaternion.Euler(0, 270, 0));
                    break;
            }
            return nnote;
        }

        internal static GameObject ShowSaber(DownloadRequestResponse downloadRequest, int position)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(downloadRequest.filePath);
            var Sabers = ab.LoadAsset<GameObject>("_CustomSaber");
            ab.Unload(false);
            GameObject saber = GameObject.Instantiate(Sabers);

            saber.name = "LootSaberPreviewSaber";
            GameObject.Find("LootSaberPreviewSaber/RightSaber").SetActive(false);
            switch (position)
            {
                case 1:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(left, Quaternion.Euler(270, 0, 0));
                    break;
                case 2:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(middle, Quaternion.Euler(270, 0, 0));
                    break;
                case 3:
                    GameObject.Find("LootSaberPreviewSaber/LeftSaber").gameObject.transform.SetPositionAndRotation(right, Quaternion.Euler(270, 0, 0));
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
                    break;
                case 2:
                    textObj.transform.position = middle;
                    break;
                case 3:
                    textObj.transform.position = right;
                    break;
            }
            textObj.SetActive(true);
            
            textObj.transform.SetParent(XP.XPScreen.Instance.transform);
            return textObj;
        }
        internal static void yeetem()
        {
            foreach(GameObject asset in previewObjects)
            {
                GameObject.Destroy(asset);
            }
        }
    }
}
