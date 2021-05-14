using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;
using LootSaber.UI.ViewControllers;
namespace LootSaber.UI
{
    internal class LootSaberFlowCoordinator : FlowCoordinator
    {
        private MainViewController _mainViewController;

        public void Awake()
        {
            if (!_mainViewController)
                _mainViewController = BeatSaberUI.CreateViewController<MainViewController>();
        }


        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("Lootboxes");
                    showBackButton = true;
                    ProvideInitialViewControllers(_mainViewController);
                }
            }
            catch (Exception e)
            {
                Plugin.Log.Error(e);
            }
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
