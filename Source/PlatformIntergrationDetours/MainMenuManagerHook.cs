using DwarfHeim.PlatformUserIntegration;
using DwarfHeim.SaveGame;
using DwarfHeim.UI.MainMenu;
using HarmonyLib;
using PineCone.Networking.Client;

namespace ProjectRebellion.PlatformIntergrationDetours
{
	//[HarmonyPatch]
    public static class MainMenuManagerHook
    {
		//[HarmonyPrefix]
        //[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.ChangeReadyStatus))]
        public static bool ChangeReadyStatusDetoured(bool ready, MainMenuManager __instance)
        {
			Plugin.LogMessage("Step1");
			if (!ready && LoadGame.Instance != null)
			{
				LoadGame.Instance.CancelLoad();
			}
			Plugin.LogMessage("Step2");
			__instance.Navigation.ToggleTab(MenuNavigation.State.Play);
			Plugin.LogMessage("Step3");
			if(__instance.Lobby == null)
			{
				var reflection = typeof(PlayerProfile).GetField("_profileData", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
				var data = new DwarfHeim.PlatformUserIntegration.ProfileData()
				{
					id = "",
					PlayerName = "Test",
					NameID = 0,
					PlatformIDs = new UserPlatformIDs()
					{
						sSteamID = "0",
						SteamID = 0,
					},
					AchievementIDs = new string[0],
					PublicRoles = new string[0],
					MatchInfo = new UserMatchInfo(),
					TimeInfo = new UserTimeInfo(),
					ShopInfo = new UserShopInfo(),
					CustomizationInfo = new UserCustomizationInfo(),
					ProgressionInfo = new UserProgressionInfo(),
					display_name = "Test"
				};
				reflection.SetValue(null, data);
				Plugin.LogMessage("Setup player");
				GameLobbyHandler.Instance.InitializeLobby();

			}

			if (__instance.Lobby.MyPlayer.IsReady == ready)
			{
				return false;
			}
			Plugin.LogMessage("Step4");
			if (ClientConnectionManager.Instance != null && ClientConnectionManager.Instance.IsDestroying)
			{
				return false;
			}
			Plugin.LogMessage("Step5");
			__instance.Lobby.SendReady(ready);
			return false;
        }
	}
}
