using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;

namespace ProjectRebellion;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static Harmony HarmonyInstance { get; private set; }
    public static BepInEx.Logging.ManualLogSource LoggerInstance { get; private set; }

	private void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
		LoggerInstance = Logger;
		HarmonyInstance = new Harmony("local.rebellion.project");
		HarmonyInstance.PatchAll();
		Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is hooked!");
	}

	public static void LogMessage(object message) => LoggerInstance.LogMessage(message);
	public static void Debug_LogMessage(object message)
	{
#if DEBUG
		LoggerInstance.LogMessage(message);
#endif
	} 

	public static void LogError(object message) => LoggerInstance.LogError(message);
	public static void Debug_LogError(object message)
	{
#if DEBUG
		LoggerInstance.LogError(message);
#endif
	}

	public static void LogWarning(object message) => LoggerInstance.LogWarning(message);
	public static void Debug_LogWarning(object message)
	{
#if DEBUG
		LoggerInstance.LogWarning(message);
#endif
	}
}
