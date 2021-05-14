using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.FloatingScreen;

namespace LootSaber.UI
{
    internal class UICreator
    {
        public static LootSaberFlowCoordinator lootsaberFlowCoordinator;
        public static bool Created;

        public static void CreateMenu()
        {
            if (!Created)
            {
                MenuButton menuButton = new MenuButton("Lootboxes", "Manage lootboxes", ShowFlow);
                MenuButtons.instance.RegisterButton(menuButton);
                Created = true;
            }
        }


        public static void ShowFlow()
        {
            if (lootsaberFlowCoordinator == null)
                lootsaberFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<LootSaberFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(lootsaberFlowCoordinator);
        }
    }
}
