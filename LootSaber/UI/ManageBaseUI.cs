using HMUI;
using IPA;
using IPA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//i stole most of this from SaberFactory https://github.com/ToniMacaroni/SaberFactoryV2/blob/main/SaberFactory/UI/BaseGameUiHandler.cs
namespace LootSaber.UI
{
    internal class BaseGameUiHandler
    {
        private readonly HierarchyManager _hierarchyManager;
        private readonly ScreenSystem _screenSystem;
        internal static BaseGameUiHandler Instance;
        private readonly List<GameObject> _deactivatedScreens = new List<GameObject>();

        internal BaseGameUiHandler(HierarchyManager hierarchyManager)
        {
            Instance = this;
            _hierarchyManager = hierarchyManager;
            _screenSystem = hierarchyManager.gameObject.GetComponent<ScreenSystem>();
        }

        public void DismissGameUI()
        {
            _deactivatedScreens.Clear();
            DeactivateScreen(_screenSystem.leftScreen);
            DeactivateScreen(_screenSystem.mainScreen);
            DeactivateScreen(_screenSystem.rightScreen);
            DeactivateScreen(_screenSystem.bottomScreen);
            DeactivateScreen(_screenSystem.topScreen);

            //var main = GetViewController(_screenSystem.mainScreen);

            //_childViewControllers.Clear();
            //_childViewControllers.Add(GetViewController(_screenSystem.leftScreen));
            //_childViewControllers.Add(GetViewController(_screenSystem.rightScreen));
            //_childViewControllers.Add(main);
            //_childViewControllers.Add(GetViewController(_screenSystem.bottomScreen));
            //_childViewControllers.Add(GetViewController(_screenSystem.topScreen));
            //GetChildViewControllers(main, _childViewControllers);

            //HideViewControllers(_childViewControllers);
        }

        public void PresentGameUI()
        {
            foreach (var screenObj in _deactivatedScreens)
            {
                screenObj.SetActive(true);
            }
        }

        public Transform GetUIParent()
        {
            return _hierarchyManager.transform;
        }

        private void DeactivateScreen(HMUI.Screen screen)
        {
            var go = screen.gameObject;
            if (go.activeSelf)
            {
                _deactivatedScreens.Add(go);
                go.SetActive(false);
            }
        }

        private void HideViewControllers(IEnumerable<ViewController> vcs)
        {
            var cgs = vcs.NonNull().Select(x => x.GetComponent<CanvasGroup>());
            foreach (var cg in cgs)
            {
                cg.gameObject.SetActive(false);
            }
        }

        private void ShowViewControllers(IEnumerable<ViewController> vcs)
        {
            var cgs = vcs.NonNull().Select(x => x.GetComponent<CanvasGroup>());
            foreach (var cg in cgs)
            {
                cg.gameObject.SetActive(true);
            }
        }

        private void GetChildViewControllers(ViewController vc, List<ViewController> list)
        {
            if (vc.childViewController != null)
            {
                list.Add(vc.childViewController);
                GetChildViewControllers(vc.childViewController, list);
            }
        }

        private ViewController GetViewController(HMUI.Screen screen)
        {
            return screen.GetField<ViewController, HMUI.Screen>("_rootViewController");
        }
    }
}
