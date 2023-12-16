using DwarfHeim.UI.MainMenu;
using HarmonyLib;

namespace ProjectRebellion.MainMenu
{
	[HarmonyPatch]
	public static class MenuNavigationHook
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(MenuNavigation), "Start")]
		public static void PostfixStart(MenuNavigation __instance)
		{
			if (__instance.gameObject.name == "Profile")
			{
				var find = __instance.transform.Find("Panel/NavBar/Leaderboard");
				if (find != null)
				{
					//We will never be able to recreate leaderboards, unless somehow we get access to Steamworks itself and active them there
					find.gameObject.SetActive(false);
				}
			}
		}
	}
}
