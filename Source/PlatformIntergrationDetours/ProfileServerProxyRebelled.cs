using DwarfHeim.Managers;
using DwarfHeim.PlatformUserIntegration;
using DwarfHeim.Progression;
using DwarfHeim.UI;
using HarmonyLib;
using Newtonsoft.Json;
using PineCone;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Ucg.Matchmaking;

namespace ProjectRebellion.PlatformIntergrationDetours
{
	[HarmonyPatch]
	public static class ProfileServerProxyRebelledHook
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), "Awake")]
		public static bool Awake_Detoured(ProfileServerProxy __instance)
		{
			var go = __instance.gameObject;
			go.AddComponent<ProfileServerProxyRebelled>();
			MonoBehaviour.Destroy(__instance);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.ValidateSteamAuthTicket))]
		public static bool ValidateSteamAuthTicket_Detoured(ulong steamid, uint appID)
		{
			ProfileServerProxyRebelled.OtherInstance.ValidateSteamAuthTicketDetoured(steamid, appID);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.ValidateSteamAuthTicketOLD))]
		public static bool ValidateSteamAuthTicketOLD_Detoured(string username, uint appID)
		{
			ProfileServerProxyRebelled.OtherInstance.ValidateSteamAuthTicketOLDDetoured(username, appID);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.CheckUsernameValidity))]
		public static bool CheckUsernameValidity_Detoured(string username)
		{
			ProfileServerProxyRebelled.OtherInstance.CheckUsernameValidityDetoured(username);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.RegisterPlayer))]
		public static bool RegisterPlayer_Detoured(string id, string username, uint nameid)
		{
			ProfileServerProxyRebelled.OtherInstance.RegisterPlayerDetoured(id, username, nameid);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetPlayerData))]
		public static bool GetPlayerData_Detoured(string id)
		{
			ProfileServerProxyRebelled.OtherInstance.GetPlayerDataDetoured(id);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetFriendsData))]
		public static bool GetFriendsData_Detoured(List<ulong> steamIDs)
		{
			ProfileServerProxyRebelled.OtherInstance.GetFriendsDataDetoured(steamIDs);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetUserMatches))]
		public static bool GetUserMatches_Detoured(string id, int amount)
		{
			ProfileServerProxyRebelled.OtherInstance.GetUserMatchesDetoured(id, amount);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.OnGetUserFriends))]
		public static bool OnGetUserFriends_Detoured(ProfileResponseObject<ProfileData[]> response)
		{
			ProfileServerProxyRebelled.OtherInstance.OnGetUserFriendsDetoured(response);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SetUserUnlockRequest))]
		public static bool SetUserUnlockRequest_Detoured(string unlockId)
		{
			ProfileServerProxyRebelled.OtherInstance.SetUserUnlockRequestDetoured(unlockId);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SendBugReport))]
		public static bool SendBugReportDetoured_Detoured(BugReport bugReport)
		{
			ProfileServerProxyRebelled.OtherInstance.SendBugReportDetoured(bugReport);
			return false;
		}


		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SendBugReportBlob))]
		public static bool SendBugReportBlob_Detoured(string id, string filePostFix, FileStream blob)
		{
			ProfileServerProxyRebelled.OtherInstance.SendBugReportBlobDetoured(id, filePostFix, blob);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SendPlayerUpdate))]
		public static bool SendPlayerUpdate_Detoured(PlayerUpdateRequest updateRequest)
		{
			ProfileServerProxyRebelled.OtherInstance.SendPlayerUpdateDetoured(updateRequest);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetPlayerCosmetic))]
		public static bool GetPlayerCosmetic_Detoured(string id)
		{
			ProfileServerProxyRebelled.OtherInstance.GetPlayerCosmeticDetoured(id);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetPlayerSteam))]
		public static bool GetPlayerSteam_Detoured(ulong[] ids)
		{
			ProfileServerProxyRebelled.OtherInstance.GetPlayerSteamDetoured(ids);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SendPlayerNameTagged))]
		public static bool SendPlayerNameTagged_Detoured(string id)
		{
			ProfileServerProxyRebelled.OtherInstance.SendPlayerNameTaggedDetoured(id);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetCosmeticsByUnlockType))]
		public static bool GetCosmeticsByUnlockType_Detoured(string unlocktype)
		{
			ProfileServerProxyRebelled.OtherInstance.GetCosmeticsByUnlockTypeDetoured(unlocktype);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetExperienceLevelTable))]
		public static bool GetExperienceLevelTable_Detoured()
		{
			ProfileServerProxyRebelled.OtherInstance.GetExperienceLevelTableDetoured();
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.GetLeaderboard))]
		public static bool GetLeaderboard_Detoured(int amount)
		{
			ProfileServerProxyRebelled.OtherInstance.GetLeaderboardDetoured(amount);
			return false;
		}
	}


	public class ProfileServerProxyRebelled : ProfileServerProxy
	{
		public static string GET_FILEPATH_REBELLED_PROFILE() => Path.Combine(Application.persistentDataPath, $"PlayerData_{SteamClient.SteamId}.json");
		public static string GET_FILEPATH_REBELLED_PROFILE_BACKUP() => Path.Combine(Application.persistentDataPath, $"PlayerData_{SteamClient.SteamId}_{DateTime.Now.ToString().Replace(':', '_')}.json");


		public class ReflectionCache

		{
			//This is because of one of stupid edge cases, where you can't left assign in if a field is delcared as event, unless it belongs to declaring class
			public FieldInfo AuthResponse;
			public FieldInfo GetPlayerDataResponse;
			public FieldInfo GetFriendsDataResponse;
			public FieldInfo GetUserMatchesResponse;
			public FieldInfo GetLeaderboardResponse;
			public FieldInfo GetExperienceLevelTableResponse;
			public FieldInfo GetCosmeticsByUnlockTypeResponse;
			public FieldInfo GetGlobalChatResponse;
			public FieldInfo SendGlobalChatMessageResponse;
			public FieldInfo SendBugReportResponse;
			public FieldInfo PlayerUpdateResponse;
			public FieldInfo GetPlayerCosmeticsResponse;
			public FieldInfo GetPlayerSteamResponse;

			public ReflectionCache()
			{
				var reflectionSelf = typeof(ReflectionCache).GetFields(BindingFlags.Instance | BindingFlags.Public);

				foreach (var field in reflectionSelf)
				{
					var name = field.Name;
					var fieldToFind = typeof(ProfileServerProxy).GetField(name, BindingFlags.Static | BindingFlags.NonPublic); //C# is lying - these are private!
					if (fieldToFind == null)
					{
						Plugin.LogError($"Failed to get an event reflection {name} - not good!");
						continue;
					}

					field.SetValue(this, fieldToFind);
				}
				Plugin.Debug_LogMessage("Finished seting up reflections for events");

			}
		}

		public static ReflectionCache ReflectionCacheInstance = new ReflectionCache();

		//public static event Action<string> RegisterPlayerResponseDetoured;
		//public static event Action<MetaInfo, bool> ErrorResponseDetoured;
		public static ProfileServerProxyRebelled OtherInstance;

		private void Awake()
		{
			Plugin.LogMessage("Setting up rebelled profile server behaviour");
			if (ProfileServerProxy.Instance != null)
			{
				Destroy(this);
				return;
			}
			ProfileServerProxy.Instance = this;
			OtherInstance = this;
			SceneController.Instance.DontDestroyOnLoad(base.transform, Array.Empty<SceneController.PersistenceLevel>());
			MatchmakingClient.BASE_URL = "";
			Plugin.LogMessage("Set up!");
		}

		public void ValidateSteamAuthTicketDetoured(ulong steamid, uint appID)
		{
			Plugin.LogMessage("Running detoured ValidateSteamAuthTicketDetoured");
			this.ValidateSteamAuthTicketRequestDetoured(steamid, appID);
		}

		public void ValidateSteamAuthTicketOLDDetoured(string username, uint appID)
		{
			Plugin.LogMessage("Running detoured ValidateSteamAuthTicketOLDDetoured");
			base.StartCoroutine(this.ValidateSteamAuthTicketRequestOLD(username, appID));
		}

		public void CheckUsernameValidityDetoured(string username)
		{
			Plugin.LogMessage("Running detoured CheckUsernameValidityDetoured");
			this.CheckUsernameValidityRequest(username);
		}

		public void RegisterPlayerDetoured(string id, string username, uint nameid)
		{
			Plugin.LogMessage("Running detoured CheckUsernameValidityDetoured");
			this.RegisterPlayerRequest(id, username, nameid);
		}

		public void GetPlayerDataDetoured(string id)
		{
			this.GetPlayerDataRequest(id);
		}

		public void GetFriendsDataDetoured(List<ulong> steamIDs)
		{
			this.GetFriendsDataRequest(steamIDs);
		}

		public void GetUserMatchesDetoured(string id, int amount)
		{
			this.GetUserMatchesRequest(id, amount);
		}

		private void ValidateSteamAuthTicketRequestDetoured(ulong steamid, uint appID)
		{
			StartCoroutine(DelayAuthorization(new UserAuthorizationResponse()
			{
				HasRegistered = true,
				id = steamid.ToString(),
				Token = $"{steamid}/{appID}"
			}));
		}

		private IEnumerator DelayAuthorization(UserAuthorizationResponse userAuthorizationResponse)
		{
			//yield return new WaitForSeconds(5);
			yield return null;
			OnAuthResponse(userAuthorizationResponse);
		}

		private IEnumerator ValidateSteamAuthTicketRequestOLD(string username, uint appID)
		{
			yield return null;

			var steamID = SteamClient.SteamId;
			if (steamID == 0)
			{
				OnErrorResponse(new MetaInfo()
				{
					Code = 0,
					Message = "No Steam id",
					E = null
				});
			}
			else
			{
				OnAuthResponse(new UserAuthorizationResponse()
				{
					HasRegistered = true,
					id = steamID.Value.ToString(),
					Token = $"{steamID.Value}/{appID}"
				});
			}
		}

		private void CheckUsernameValidityRequest(string playerName)
		{
			OnCheckUsernameValidityResponse(new UserValidatePlayerNameResponse()
			{
				NameID = 0,
				PlayerName = playerName,
				PlayerNameUnique = false
			});
		}

		private void RegisterPlayerRequest(string id, string username, uint nameid)
		{
			Plugin.Debug_LogMessage("Register player data request... lmao");
		}

		private void GetPlayerDataRequest(string id)
		{
			Plugin.Debug_LogMessage($"Get player data request for {id}");
			if (id == SteamClient.SteamId.Value.ToString())
			{
				var path = GET_FILEPATH_REBELLED_PROFILE();
				ProfileData profileData = null;
				try
				{
					if (File.Exists(path))
					{
						Plugin.LogMessage($"Loading profile from JSON {path}!");

						string content = File.ReadAllText(path);
						profileData = JsonConvert.DeserializeObject<ProfileData>(content);
						profileData.TimeInfo.LastLogin = DateTime.UtcNow;
						Plugin.LogMessage($"Profile loaded successfully!");
					}
					else
					{
						profileData = CreateEmptyProfile(id);
						File.WriteAllText(path, JsonConvert.SerializeObject(profileData, Formatting.Indented));

						Plugin.LogMessage($"No profile file - creating new one!");
					}
				}
				catch (Exception ex)
				{
					Plugin.LogError($"Failed to load profile data: {ex}");
					if (File.Exists(path))
						File.Move(path, GET_FILEPATH_REBELLED_PROFILE_BACKUP());


					profileData = CreateEmptyProfile(id);
					File.WriteAllText(path, JsonConvert.SerializeObject(profileData, Formatting.Indented));
				}
				OnGetPlayerDataResponse(profileData);

				//OnGetPlayerDataResponse();
			}
			else
			{
				Plugin.Debug_LogError($"Not implemented or impossible to implement with Steamworks client limitations?");
				SteamId steamIdCasted = ulong.Parse(id);
				OnGetPlayerDataResponse(new ProfileData()
				{
					id = id,
					NameID = 0,
					PlayerName = new Friend(steamIdCasted).Name,
					display_name = new Friend(steamIdCasted).Name,
					ProgressionInfo = new UserProgressionInfo()
					{
						Experience = 0,
						Level = 1,
						Unlocks = new string[] { }
					}
				}); ;

			}

		}

		private ProfileData CreateEmptyProfile(string id)
		{
			return new ProfileData()
			{
				id = id,
				display_name = SteamClient.Name,
				NameID = 0,
				PlayerName = SteamClient.Name,
				MatchInfo = new UserMatchInfo()
				{
					ConquestPlayed = 0,
					ConquestWon = 0,
					CurrentElo = 0,
					CurrentRank = 0,
					RankCooldown = 0,
					MultipleRanks = new int[][]
					{
						new int[]
						{
							0
						}
					},
					MatchIDs = new string[] { },
					FPCounter = 0,
					MultipleElos = new int[][][]
					{
						new int[][]
						{
							new int[] { }
						}
					}
				},
				PlatformIDs = new UserPlatformIDs()
				{
					sSteamID = id,
					SteamID = ulong.Parse(id)
				},
				PublicRoles = new string[]
				{

				},
				TimeInfo = new UserTimeInfo()
				{
					DateJoined = DateTime.UtcNow,
					LastLogin = DateTime.UtcNow,
					TimePlayed = 0 //This should probably use Steam... but I don't know if it can
				},
				ShopInfo = new UserShopInfo()
				{
					IngameCurrency = 0,
					PremiumCurrency = 0,
				},
				CustomizationInfo = new UserCustomizationInfo()
				{
					AvatarID = 0,
					CosmeticIDs = new string[0],
					EquippedCosmeticIDs = new string[0],
					NameChangesRemaining = 0,
				},
				AchievementIDs = new string[] {
						//This might need to be loaded from some file instead or maybe steamworks?
						},
				ProgressionInfo = new UserProgressionInfo()
				{
					//Played unlocks is last in list
					Experience = 0,
					Level = 1,
					Unlocks = new string[] { JsonConvert.SerializeObject(new MyUnlocks()) }
				}
			};
		}

		private void StoreProfile()
		{
			if (PlayerProfile.ProfileData == null)
			{
				Plugin.LogError("Can't write a profile, cause it doesn't exist!");
				return;
			}

			try
			{
				var path = GET_FILEPATH_REBELLED_PROFILE();
				File.WriteAllText(path, JsonConvert.SerializeObject(PlayerProfile.ProfileData, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Plugin.Debug_LogError($"Something when wrong with unlock request: {ex}");
			}
		}

		private System.Collections.IEnumerator GetPlayerDataRequestOLD()
		{
			string text = ProfileServerProxy.BASE_URL + "accounts/profileapi/";
			using (UnityWebRequest www = UnityWebRequest.Get(text))
			{
				www.SetRequestHeader("Authorization", "Token " + PlayerProfile.AuthenticationService.AuthToken);
				www.timeout = 10;
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					string text2 = "[GetPlayerDataRequest] Error: " + www.error;
					this.OnErrorResponse(new MetaInfo
					{
						Code = www.responseCode,
						Message = text2,
						E = null
					}, false);
				}
				else
				{
					try
					{
						GetPlayerRequestData getPlayerRequestData = JsonUtility.FromJson<GetPlayerRequestData>(www.downloadHandler.text);
						if (!string.IsNullOrEmpty(getPlayerRequestData.profile_data.PlayerName))
						{
							this.OnGetPlayerDataResponse(getPlayerRequestData.profile_data);
						}
						else
						{
							string text3 = "[GetPlayerDataRequest] Response: " + getPlayerRequestData.status;
							this.OnErrorResponse(new MetaInfo
							{
								Code = www.responseCode,
								Message = text3,
								E = null
							}, false);
						}
					}
					catch (Exception ex)
					{
						string text4 = "[GetPlayerDataRequest] Server returned: " + www.downloadHandler.text;
						this.OnErrorResponse(new MetaInfo
						{
							Code = www.responseCode,
							Message = text4,
							E = ex
						}, false);
					}
				}
			}
		}

		private void GetFriendsDataRequest(List<ulong> steamIDs)
		{
			Plugin.LogMessage("Faking get friends data request");
			var profiles = new ProfileData[steamIDs.Count];
			for (int i = 0; i < profiles.Length; i++)
			{
				var profileSteamID = steamIDs[i];

				profiles[i] = new ProfileData()
				{
					AchievementIDs = new string[0],
					CustomizationInfo = new UserCustomizationInfo()
					{
						AvatarID = 0,
						CosmeticIDs = new string[0],
						EquippedCosmeticIDs = new string[0],
						NameChangesRemaining = 0,
					},
					display_name = new Friend(profileSteamID).Name,
					NameID = 0,
					PlayerName = new Friend(profileSteamID).Name,
					id = profileSteamID.ToString(),
					MatchInfo = new UserMatchInfo() { },
					PublicRoles = new string[0],
					PlatformIDs = new UserPlatformIDs()
					{
						sSteamID = profileSteamID.ToString(),
						SteamID = profileSteamID
					},
					ProgressionInfo = new UserProgressionInfo()
					{
						Experience = 0,
						Level = 1,
						Unlocks = new string[0]
					},
					ShopInfo = new UserShopInfo()
					{
						IngameCurrency = 0,
						PremiumCurrency = 0
					},
					TimeInfo = new UserTimeInfo()
					{
						DateJoined = DateTime.MinValue,
						LastLogin = DateTime.Now,
						TimePlayed = 0
					}
				};
			}

			var response = new ProfileResponseObject<ProfileData[]>()
			{
				IsSuccess = true,
				Meta = new MetaInfo()
				{
					Message = "",
					Code = 0,
					Error = ""
				},
				ProfileObject = profiles
			};
			OnGetUserFriends(response);
		}

		public void OnGetUserFriendsDetoured(ProfileResponseObject<ProfileData[]> response)
		{
			LogHandler.PrintDebug("Received user's friend profiles response from Profile Server", DebugCategory.ProfileServer, null);
			if (response.IsSuccess)
			{
				ProfileData[] profileObject = response.ProfileObject;
				List<GetFriendsRequestData> list = new List<GetFriendsRequestData>();
				foreach (ProfileData profileData in profileObject)
				{
					GetFriendsRequestData getFriendsRequestData = new GetFriendsRequestData
					{
						id = profileData.PlatformIDs.SteamID,
						ProfileData = profileData
					};
					list.Add(getFriendsRequestData);
				}

				EventHandler<List<GetFriendsRequestData>> getFriendsDataResponse = (EventHandler<List<GetFriendsRequestData>>)ReflectionCacheInstance.GetFriendsDataResponse.GetValue(this);
				if (getFriendsDataResponse == null)
				{
					return;
				}
				getFriendsDataResponse(this, list);
				return;
			}
			else
			{
				if ((int)response.Meta.Code != 404)
				{
					this.OnErrorResponse(response.Meta, false);
					return;
				}
				EventHandler<List<GetFriendsRequestData>> getFriendsDataResponse2 = (EventHandler<List<GetFriendsRequestData>>)ReflectionCacheInstance.GetFriendsDataResponse.GetValue(this);
				if (getFriendsDataResponse2 == null)
				{
					return;
				}
				getFriendsDataResponse2(this, new List<GetFriendsRequestData>());
				return;
			}
		}

		private void GetUserMatchesRequest(string id, int amount)
		{
			Plugin.Debug_LogMessage($"Trying to get user messages: {id}");

			if (id == SteamClient.SteamId.ToString())
			{
				//Store/Load from file?
				var matchProfile = new MatchProfile[]
				{
					new MatchProfile()
					{
						Date = DateTime.UtcNow,
						Difficulty = 0,
						EndInfo = new MatchEndInfo()
						{
							Finished = true,
							Surrender = false,
							TimePlayed = 0,
							WinningTeam = 0,
						},
						GameServerID = 0,
						id = "",
						MapID = "",
						ModeID = 0,
						Ranked = false,
						Teams = new MatchTeam[]
						{
							new MatchTeam()
							{
								players = new MatchPlayer[]
									{

									}
								}
							}
						}
				};

				OnGetUserMatches(new DwarfHeim.PlatformUserIntegration.ProfileResponseObject<MatchProfile[]>()
				{
					IsSuccess = true,
					Meta = new DwarfHeim.PlatformUserIntegration.MetaInfo()
					{
						Code = 0,
						Error = "",
						Message = "",
					},
					ProfileObject = matchProfile
				});
			}
			else
			{
				var matchProfile = new MatchProfile[]
				{
					new MatchProfile()
					{
						Date = DateTime.UtcNow,
						Difficulty = 0,
						EndInfo = new MatchEndInfo()
						{
							Finished = true,
							Surrender = false,
							TimePlayed = 0,
							WinningTeam = 0,
						},
						GameServerID = 0,
						id = "?",
						MapID = "",
						ModeID = 0,
						Ranked = false,
						Teams = new MatchTeam[]
						{
							new MatchTeam()
							{
								players = new MatchPlayer[]
									{

									}
								}
							}
						}
					};

				OnGetUserMatches(new DwarfHeim.PlatformUserIntegration.ProfileResponseObject<MatchProfile[]>()
				{
					IsSuccess = true,
					Meta = new DwarfHeim.PlatformUserIntegration.MetaInfo()
					{
						Code = 0,
						Error = "",
						Message = "",
					},
					ProfileObject = matchProfile
				});
			}
		}

		public void SetUserUnlockRequestDetoured(string unlockId)
		{
			Plugin.Debug_LogMessage("Faking unlocks by storing them in file");
			if (PlayerProfile.ProfileData == null)
			{
				Plugin.Debug_LogError("Player profile was null... now what?!");
				return;
			}

			PlayerProfile.ProfileData.ProgressionInfo.Unlocks[PlayerProfile.ProfileData.ProgressionInfo.Unlocks.Length - 1] = unlockId;

			StoreProfile();

		}

		public void SendBugReportDetoured(BugReport bugReport)
		{
			Plugin.LogError("Why the f*** would you even try sending a report for a bug report for dead game?!");
			return;
		}

		public void SendBugReportBlobDetoured(string id, string filePostFix, FileStream blob)
		{
			Plugin.LogError("Why the f*** would you even try sending a report for a bug report for dead game?!");
			return;
		}

		public void SendPlayerUpdateDetoured(PlayerUpdateRequest updateRequest)
		{
			Plugin.LogError("SendPlayerUpdate");

			Debug.Log("SendPlayerUpdate: Sending Player Update for id : " + updateRequest.id);
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "player/update";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders,
				Json = updateRequest.ToJson<PlayerUpdateRequest>()
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[SendPlayerUpdate] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			StartCoroutine(HTTPController.POST<ProfileResponseObject<string>>(httpRequest, new Action<ProfileResponseObject<string>>(this.OnPlayerUpdateResponse), httpErrorHandler));
		}

		public void GetPlayerCosmeticDetoured(string id)
		{
			Plugin.LogMessage($"Running detoured get player comsetics for id: {id}");

			if (id == SteamClient.SteamId.ToString())
			{
				var result = new PlayerCosmeticsResponse()
				{
					CosmeticIDs = RebelledData.GetAllCosmetics(),
					EquippedCosmeticIDs = new string[] { } //Might need to be send somehow?
				};
				OnGetPlayerCosmeticsResponse(new ProfileResponseObject<PlayerCosmeticsResponse>()
				{
					IsSuccess = true,
					Meta = new MetaInfo()
					{
						Code = 0,
						Error = "",
						Message = ""
					},
					ProfileObject = result
				});
			}
			else
			{
				var result = new PlayerCosmeticsResponse()
				{
					CosmeticIDs = RebelledData.GetAllCosmetics(),
					EquippedCosmeticIDs = new string[] { } //Might need to be send somehow?
				};
				OnGetPlayerCosmeticsResponse(new ProfileResponseObject<PlayerCosmeticsResponse>()
				{
					IsSuccess = true,
					Meta = new MetaInfo()
					{
						Code = 0,
						Error = "",
						Message = ""
					},
					ProfileObject = result
				});
			}
		}

		public void GetPlayerSteamDetoured(ulong[] ids)
		{
			Plugin.Debug_LogMessage($"Faking player steam data for {string.Join(",", ids)}");

			ProfileData[] temp = new ProfileData[ids.Length];
			for (int i = 0; i < temp.Length; i++)
			{
				temp[i] = new ProfileData()
				{
					AchievementIDs = new string[0],
					CustomizationInfo = new UserCustomizationInfo()
					{
						AvatarID = 0,
						CosmeticIDs = new string[0],
						EquippedCosmeticIDs = new string[0],
						NameChangesRemaining = 0
					},
					display_name = new Friend((SteamId)ids[i]).Name,
					id = ids[i].ToString(),
					NameID = 0,
					PlatformIDs = new UserPlatformIDs()
					{
						sSteamID = ids[i].ToString(),
						SteamID = ids[i]
					},
					PlayerName = new Friend((SteamId)ids[i]).Name,
					ProgressionInfo = new UserProgressionInfo()
					{
						Experience = 0,
						Level = 1,
						Unlocks = new string[0],
					},
					MatchInfo = new UserMatchInfo(),
					PublicRoles = new string[0],
					ShopInfo = new UserShopInfo()
					{
						IngameCurrency = 0,
						PremiumCurrency = 0
					},
					TimeInfo = new UserTimeInfo()
					{
						DateJoined = DateTime.MinValue,
						LastLogin = DateTime.UtcNow,
						TimePlayed = 0
					}
				};
			}

			ProfileResponseObject<ProfileData[]> response = new ProfileResponseObject<ProfileData[]>()
			{
				IsSuccess = true,
				Meta = new MetaInfo()
				{
					Code = 0,
					Error = "",
					Message = "",
				},
				ProfileObject = temp
			};
			OnGetPlayerSteamResponse(response);
		}

		public void SendPlayerNameTaggedDetoured(string id)
		{
			Debug.Log("SendPlayerNameTagged: setting nametagged for id: " + id);
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "player/nametag/" + id;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[SendPlayerNameTagged] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<string>>(httpRequest, new Action<ProfileResponseObject<string>>(this.OnSendNameTagged), httpErrorHandler, null));
		}

		public void SendGlobalChatMessageDetoured(string chat, string message, Action<string> errorCallback)
		{
			return;
		}

		public void GetGlobalChatDetoured(string chat, int amount = -1, Action<string> errorCallback = null)
		{
			return;
		}

		public void GetLeaderboardDetoured(int amount)
		{
			Plugin.LogMessage("Faking getting a leaderboard");

			var result = new ProfileProperty<int>[][]
			{
				new ProfileProperty<int>[]
				{
					new ProfileProperty<int>()
					{
						id = "F",
						Property = 0
					}
				}
			};
			OnGetLeaderboard(new ProfileResponseObject<ProfileProperty<int>[][]>()
			{
				IsSuccess = true,
				Meta = new MetaInfo()
				{
					Code = 0,
					Error = "",
					Message = "",
				},
				ProfileObject = result
			});
			this.GetExperienceLevelTableDetoured();
		}

		public void GetExperienceLevelTableDetoured()
		{
			Plugin.Debug_LogMessage("Faking get experience table");

			var response = new ProfileResponseObject<ExperienceLevelTable>()
			{
				IsSuccess = true,
				Meta = new MetaInfo()
				{
					Code = 0,
					Error = "",
					Message = ""
				},
				ProfileObject = new ExperienceLevelTable()
				{
					Table = new PseudoDictionary<uint, uint>()
					{
						new PseudoDictionaryItem<uint, uint>()
						{
							Key = 0, Value = 0
						},
						new PseudoDictionaryItem<uint, uint>()
						{
							Key = 1, Value = 1000
						}
						//Probably add stuff here... idk - nobody really cares about the game
					}
				}
			};
			OnGetExperienceLevelTableDetoured(response);
		}

		public void GetCosmeticsByUnlockTypeDetoured(string unlocktype)
		{
			Plugin.Debug_LogMessage($"Faking response for {unlocktype}");

			var response = new ProfileResponseObject<Cosmetic[]>()
			{
				IsSuccess = true,
				Meta = new MetaInfo()
				{
					Code = 0,
					Message = "",
					Error = "",
				},
				ProfileObject = new Cosmetic[]
				{

				}
			};
			OnCosmeticByUnlockTypeDetoured(response);
		}

		public void OnCosmeticByUnlockTypeDetoured(ProfileResponseObject<Cosmetic[]> response)
		{
			if (!response.IsSuccess)
			{
				this.OnErrorResponse(response.Meta, false);
				return;
			}
			LogHandler.PrintDebug("OnCosmeticByUnlockType: Result success! response=" + response.ToJson<ProfileResponseObject<Cosmetic[]>>(), DebugCategory.ProfileServer, null);
			EventHandler<Cosmetic[]> getCosmeticsByUnlockTypeResponse = (EventHandler<Cosmetic[]>)ReflectionCacheInstance.GetCosmeticsByUnlockTypeResponse.GetValue(this);
			if (getCosmeticsByUnlockTypeResponse == null)
			{
				return;
			}
			getCosmeticsByUnlockTypeResponse(this, response.ProfileObject);
		}

		public void OnGetExperienceLevelTableDetoured(ProfileResponseObject<ExperienceLevelTable> response)
		{
			if (!response.IsSuccess)
			{
				if (response.Meta.Code != 404L)
				{
					this.OnErrorResponse(response.Meta, false);
				}
				return;
			}
			LogHandler.PrintDebug("OnGetExperienceLevelTable: Result success! response=" + response.ToJson<ProfileResponseObject<ExperienceLevelTable>>(), DebugCategory.ProfileServer, null);
			EventHandler<ExperienceLevelTable> getExperienceLevelTableResponse = (EventHandler<ExperienceLevelTable>)ReflectionCacheInstance.GetExperienceLevelTableResponse.GetValue(this);
			if (getExperienceLevelTableResponse == null)
			{
				return;
			}
			getExperienceLevelTableResponse(this, response.ProfileObject);
		}

		public void GetPlayerPropertyDetoured<T, Y>(string propertyName, ulong platformID, Action<ProfileResponseObject<ProfileProperty<T, Y>>> callback)
		{
			LogHandler.PrintDebug(string.Format("GetPlayerProperty: Getting {0} for player {1}", propertyName, platformID), DebugCategory.ProfileServer, null);
			string text = string.Format("player/property/steam/{0}/", platformID) + propertyName;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetPlayerProperty] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<ProfileProperty<T, Y>>>(httpRequest, callback, httpErrorHandler, null));
		}

		public void OnGetLeaderboardDetoured(ProfileResponseObject<ProfileProperty<int>[][]> response)
		{
			LogHandler.PrintDebug("OnGetLeaderboard: Result success: " + response.IsSuccess.ToString() + ", response=" + response.ToJson<ProfileResponseObject<ProfileProperty<int>[][]>>(), DebugCategory.ProfileServer, null);
			EventHandler<ProfileProperty<int>[][]> getLeaderboardResponse = (EventHandler<ProfileProperty<int>[][]>)ReflectionCacheInstance.GetLeaderboardResponse.GetValue(this);
			if (getLeaderboardResponse == null)
			{
				return;
			}
			getLeaderboardResponse(this, response.ProfileObject);
		}

		public void OnGetGlobalChatDetoured(ProfileResponseObject<ChatResponse> response)
		{
			return;
			/*			EventHandler<ChatResponse> getGlobalChatResponse = (EventHandler<ChatResponse>)ReflectionCacheInstance.GetGlobalChatResponse.GetValue(this);
						if (getGlobalChatResponse == null)
						{
							return;
						}
						getGlobalChatResponse(this, response.ProfileObject);*/
		}

		public void OnSendGlobalChatMessageDetoured(ProfileResponseObject<string> response)
		{
			return;
			/*			if (!response.IsSuccess)
						{
							LogHandler.PrintError("OnSendGlobalChatMessage: Result success: " + response.IsSuccess.ToString() + ", response=" + response.ToJson<ProfileResponseObject<string>>(), null);
							EventHandler<string> sendGlobalChatMessageResponse = (EventHandler<string>)ReflectionCacheInstance.SendGlobalChatMessageResponse.GetValue(this);
							if (sendGlobalChatMessageResponse == null)
							{
								return;
							}
							sendGlobalChatMessageResponse(this, response.Meta.Message);
							return;
						}
						else
						{
							EventHandler<string> sendGlobalChatMessageResponse2 = (EventHandler<string>)ReflectionCacheInstance.SendGlobalChatMessageResponse.GetValue();
							if (sendGlobalChatMessageResponse2 == null)
							{
								return;
							}
							sendGlobalChatMessageResponse2(this, null);
							return;
						}*/
		}

		public void OnSendNameTaggedDetoured(ProfileResponseObject<string> response)
		{
			LogHandler.PrintDebug("OnSendNameTagged: Result success: " + response.IsSuccess.ToString() + ", response=" + response.ToJson<ProfileResponseObject<string>>(), DebugCategory.ProfileServer, null);
		}

		public void OnGetUserMatchesDetoured(ProfileResponseObject<MatchProfile[]> response)
		{
			return;
			/*			if (!response.IsSuccess)
						{
							if (response.Meta.Code != 404L)
							{
								this.OnErrorResponse(response.Meta, false);
							}
							return;
						}
						MatchProfile[] profileObject = response.ProfileObject;
						LogHandler.PrintDebug("OnGetUserMatches: Result success: " + profileObject.ToJson<MatchProfile[]>(), DebugCategory.ProfileServer, null);
						EventHandler<List<MatchProfile>> getUserMatchesResponse = ProfileServerProxyRebelled.GetUserMatchesResponse;
						if (getUserMatchesResponse == null)
						{
							return;
						}
						getUserMatchesResponse(this, profileObject.ToList<MatchProfile>());*/
		}



		protected override void OnAuthResponse(UserAuthorizationResponse authData)
		{
			Plugin.Debug_LogMessage($"Auth thing {authData.HasRegistered} / {authData.id} / {authData.Token}");

			EventHandler<UserAuthorizationResponse> authResponse = (EventHandler<UserAuthorizationResponse>)ReflectionCacheInstance.AuthResponse.GetValue(this);
			if (authResponse == null)
			{
				return;
			}
			authResponse(this, authData);
		}


		protected override void OnAuthResponse(ProfileResponseObject<UserAuthorizationResponse> response)
		{
			Plugin.Debug_LogMessage("Auth thing");
			if (!response.IsSuccess)
			{
				this.OnErrorResponse(response.Meta, true);
				return;
			}
			UserAuthorizationResponse profileObject = response.ProfileObject;
			LogHandler.PrintDebug("OnAuthResponse: Result success: " + profileObject.ToJson<UserAuthorizationResponse>(), DebugCategory.ProfileServer, null);
			if (string.IsNullOrEmpty(profileObject.Token))
			{
				this.OnErrorResponse(response.Meta, true);
				return;
			}
			EventHandler<UserAuthorizationResponse> authResponse = (EventHandler<UserAuthorizationResponse>)ReflectionCacheInstance.AuthResponse.GetValue(this);
			if (authResponse == null)
			{
				return;
			}
			authResponse(this, profileObject);
		}

	}
}
