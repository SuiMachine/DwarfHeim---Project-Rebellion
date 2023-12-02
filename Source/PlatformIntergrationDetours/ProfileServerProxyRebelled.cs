﻿using DwarfHeim.Managers;
using DwarfHeim.PlatformUserIntegration;
using DwarfHeim.UI;
using HarmonyLib;
using Newtonsoft.Json;
using PineCone;
using System;
using System.Collections.Generic;
using System.IO;
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
		public static bool ValidateSteamAuthTicketOLD_Detoured(string username, uint appIDD)
		{
			ProfileServerProxyRebelled.OtherInstance.ValidateSteamAuthTicketOLDDetoured(username, appIDD);
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
		[HarmonyPatch(typeof(ProfileServerProxy), nameof(ProfileServerProxy.SendGlobalChatMessage))]
		public static bool SendGlobalChatMessage_Detoured(string chat, string message, Action<string> errorCallback)
		{
			ProfileServerProxyRebelled.OtherInstance.SendGlobalChatMessageDetoured(chat, message, errorCallback);
			return false;
		}

	}


	public class ProfileServerProxyRebelled : ProfileServerProxy
	{
		public static new event EventHandler<UserAuthorizationResponse> AuthResponse;

		public static new event EventHandler<UserValidatePlayerNameResponse> CheckUsernameValidityResponse;
		public static new event Action<string> RegisterPlayerResponse;
		public static new event EventHandler<ProfileData> GetPlayerDataResponse;
		public static new event EventHandler<List<GetFriendsRequestData>> GetFriendsDataResponse;
		public static new event EventHandler<List<MatchProfile>> GetUserMatchesResponse;
		public static new event EventHandler<ProfileProperty<int>[][]> GetLeaderboardResponse;
		public static new event EventHandler<ExperienceLevelTable> GetExperienceLevelTableResponse;
		public static new event EventHandler<Cosmetic[]> GetCosmeticsByUnlockTypeResponse;
		public static new event EventHandler<ChatResponse> GetGlobalChatResponse;
		public static new event EventHandler<string> SendGlobalChatMessageResponse;
		public static new event EventHandler<bool> SendBugReportResponse;
		public static new event EventHandler<bool> PlayerUpdateResponse;
		public static new event EventHandler<PlayerCosmeticsResponse> GetPlayerCosmeticsResponse;
		public static new event EventHandler<ProfileData[]> GetPlayerSteamResponse;
		public static new event Action<MetaInfo, bool> ErrorResponse;
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
			string text = new UserSteamAuthorizationRequest
			{
				SteamID = steamid,
				AppID = appID,
				TicketData = PlayerProfile.AuthenticationService.PlatformAuthTicket
			}.ToJson();

			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};

			string text2 = "player/auth/client/steam";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text2,
				Headers = httpHeaders,
				Json = text
			};

			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[ValidateSteamAuthTicketRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse),
				Authentication = true
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<UserAuthorizationResponse>>(httpRequest, new Action<ProfileResponseObject<UserAuthorizationResponse>>(this.OnAuthResponse), httpErrorHandler));
		}

		private System.Collections.IEnumerator ValidateSteamAuthTicketRequestOLD(string username, uint appID)
		{
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("authTicket", PlayerProfile.AuthenticationService.PlatformAuthTicket);
			wwwform.AddField("username", username);
			wwwform.AddField("appid", appID.ToString());
			string text = ProfileServerProxy.BASE_URL;
			text += "accounts/steamticket/";
			using (UnityWebRequest www = UnityWebRequest.Post(text, wwwform))
			{
				www.timeout = 30;
				yield return www.SendWebRequest();
				if (www.isNetworkError || www.isHttpError)
				{
					string text2 = "[ValidateSteamAuthTicketRequest] Error: " + www.error;
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
						UserAuthorizationResponse userAuthorizationResponse = www.downloadHandler.text.JsonToObj<UserAuthorizationResponse>();
						Debug.Log(JsonUtility.ToJson(userAuthorizationResponse));
						if (!string.IsNullOrEmpty(userAuthorizationResponse.Token))
						{
							LogHandler.PrintDebug("Received Auth response from Profile Server", DebugCategory.ProfileServer, null);
							this.OnAuthResponse(userAuthorizationResponse);
						}
						else
						{
							string text3 = "[ValidateSteamAuthTicketRequest] Server returned: " + www.downloadHandler.text;
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
						string text4 = "[ValidateSteamAuthTicketRequest] Server returned: " + www.downloadHandler.text;
						this.OnErrorResponse(new MetaInfo
						{
							Code = www.responseCode,
							Message = text4,
							E = ex
						}, false);
					}
				}
			}
			yield break;
		}

		private void CheckUsernameValidityRequest(string playerName)
		{
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "userprofile/checkname/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text + playerName,
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[CheckUsernameValidityRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<UserValidatePlayerNameResponse>>(httpRequest, new Action<ProfileResponseObject<UserValidatePlayerNameResponse>>(this.OnCheckUsernameValidityResponse), httpErrorHandler, null));
		}

		private void RegisterPlayerRequest(string id, string username, uint nameid)
		{
			string text = new UserRegisterRequest
			{
				id = id,
				PlayerName = username,
				NameID = nameid
			}.ToJson<UserRegisterRequest>();
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text2 = "userprofile/create/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text2,
				Headers = httpHeaders,
				Json = text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[RegisterPlayerRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<UserRegisterResponse>>(httpRequest, new Action<ProfileResponseObject<UserRegisterResponse>>(this.OnRegisterPlayerResponse), httpErrorHandler));
		}

		private void GetPlayerDataRequest(string id)
		{
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "userprofile/id/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text + id,
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetPlayerDataRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<ProfileData>>(httpRequest, new Action<ProfileResponseObject<ProfileData>>(this.OnGetPlayerDataResponse), httpErrorHandler, null));
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
			string text = steamIDs.ToArray().ToJson<ulong[]>();
			Debug.Log("Sending friends ids: " + text);
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text2 = "userprofile/id/steam/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text2,
				Headers = httpHeaders,
				Json = text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetUserFriendsRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<ProfileData[]>>(httpRequest, new Action<ProfileResponseObject<ProfileData[]>>(this.OnGetUserFriends), httpErrorHandler));
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

				EventHandler<List<GetFriendsRequestData>> getFriendsDataResponse = ProfileServerProxyRebelled.GetFriendsDataResponse;
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
				EventHandler<List<GetFriendsRequestData>> getFriendsDataResponse2 = ProfileServerProxyRebelled.GetFriendsDataResponse;
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
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "userprofile/match/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = string.Concat(new object[]
				{
					ProfileServerProxy.BASE_URL,
					text,
					id,
					"/",
					amount
				}),
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetUserMatchesRequest] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<MatchProfile[]>>(httpRequest, new Action<ProfileResponseObject<MatchProfile[]>>(this.OnGetUserMatches), httpErrorHandler, null));
		}

		public void SetUserUnlockRequestDetoured(string unlockId)
		{
			ProfileProperty<string> profileProperty = default(ProfileProperty<string>);
			profileProperty.id = PlayerProfile.ProfileData.id;
			profileProperty.Property = unlockId;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + "player/unlock",
				Json = JsonConvert.SerializeObject(profileProperty),
				Headers = new HttpHeaders
				{
					Authorization = PlayerProfile.AuthenticationService.AuthToken
				}
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler();
			httpErrorHandler.BaseMessage = "[Sending Unlock Request] ";
			httpErrorHandler.Callback = delegate (MetaInfo meta, bool auth)
			{
				LogHandler.PrintDebug(string.Format("{0}: {1}", meta.Code, meta.Message), DebugCategory.Network, null);
			};
			HttpErrorHandler httpErrorHandler2 = httpErrorHandler;
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<string>>(httpRequest, delegate (ProfileResponseObject<string> res)
			{
				LogHandler.PrintDebug(string.Format("Unlock request success: {0}, {1}", res.IsSuccess, res.Meta.ToString()), DebugCategory.ProfileServer, null);
			}, httpErrorHandler2));
		}

		public void SendBugReportDetoured(BugReport bugReport)
		{
			Plugin.LogError("Why the f*** would you even try sending a report for a bug report for dead game?!");
			return;
/*			string text = bugReport.ToJson<BugReport>();
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text2 = "bugreport/create/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text2,
				Headers = httpHeaders,
				Json = text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[SendBugReport] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<string>>(httpRequest, new Action<ProfileResponseObject<string>>(this.OnSendBugReportResponse), httpErrorHandler));*/
		}

		public void SendBugReportBlobDetoured(string id, string filePostFix, FileStream blob)
		{
			Plugin.LogError("Why the f*** would you even try sending a report for a bug report for dead game?!");
			return;
/*			Debug.Log("SendBugReportBlob: Sending bug report blob for id: " + id);
			HttpBinaryHeaders httpBinaryHeaders = new HttpBinaryHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken,
				RequestId = id,
				FilePostFix = filePostFix,
				ContentType = HttpContentType.ApplicationOctetStream
			};
			string text = "bugreport/createblobbinary/";
			HttpBinaryRequest httpBinaryRequest = new HttpBinaryRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpBinaryHeaders,
				Content = blob
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[SendBugReportBlob] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.POST_BINARY<ProfileResponseObject<string>>(httpBinaryRequest, new Action<ProfileResponseObject<string>>(this.OnSendBugReportBlobResponse), httpErrorHandler));*/
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
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<string>>(httpRequest, new Action<ProfileResponseObject<string>>(this.OnPlayerUpdateResponse), httpErrorHandler));
		}

		public void GetPlayerCosmeticDetoured(string id)
		{
			Debug.Log("GetPlayerCosmetic: Getting cosmetics for id : " + id.ToJson<string>());
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "player/cosmetics/" + id;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetPlayerCosmetics] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<PlayerCosmeticsResponse>>(httpRequest, new Action<ProfileResponseObject<PlayerCosmeticsResponse>>(this.OnGetPlayerCosmeticsResponse), httpErrorHandler, null));
		}

		public void GetPlayerSteamDetoured(ulong[] ids)
		{
			Debug.Log("GetPlayerStean: Getting cosmetics for steam id : " + ids.ToJson<ulong[]>());
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "player/id/steam/";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders,
				Json = ids.ToJson<ulong[]>()
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetPlayerSteam] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<ProfileData[]>>(httpRequest, new Action<ProfileResponseObject<ProfileData[]>>(this.OnGetPlayerSteamResponse), httpErrorHandler));
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
/*			Debug.Log("SendChatMessage: Sending message=" + message + " to chat=" + chat);
			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "chat/send/";
			ChatMessage chatMessage = new ChatMessage
			{
				ChatID = chat,
				SenderID = PlayerProfile.ProfileData.id,
				Message = message
			};
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders,
				Json = chatMessage.ToJson<ChatMessage>()
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "",
				Callback = delegate (MetaInfo meta, bool auth)
				{
					Action<string> errorCallback2 = errorCallback;
					if (errorCallback2 == null)
					{
						return;
					}
					errorCallback2(meta.Message);
				}
			};
			base.StartCoroutine(HTTPController.POST<ProfileResponseObject<string>>(httpRequest, new Action<ProfileResponseObject<string>>(this.OnSendGlobalChatMessage), httpErrorHandler));*/
		}

		public void GetGlobalChatDetoured(string chat, int amount = -1, Action<string> errorCallback = null)
		{
			return;
/*			HttpHeaders httpHeaders = new HttpHeaders
			{
				Authorization = PlayerProfile.AuthenticationService.AuthToken
			};
			string text = "chat/id/" + chat + ((amount < 0) ? "" : string.Format("/{0}", amount));
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text,
				Headers = httpHeaders
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[SendGlobalChatMessage] ",
				Callback = delegate (MetaInfo meta, bool auth)
				{
					Action<string> errorCallback2 = errorCallback;
					if (errorCallback2 == null)
					{
						return;
					}
					errorCallback2(meta.Message);
				}
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<ChatResponse>>(httpRequest, new Action<ProfileResponseObject<ChatResponse>>(this.OnGetGlobalChat), httpErrorHandler, null));*/
		}

		public void GetLeaderboardDetoured(int amount)
		{
			Debug.Log("GetLeaderboard: Getting amount=" + amount);
			string text = "player/leaderboard/" + amount;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetLeaderboard] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<ProfileProperty<int>[][]>>(httpRequest, new Action<ProfileResponseObject<ProfileProperty<int>[][]>>(this.OnGetLeaderboard), httpErrorHandler, null));
			this.GetExperienceLevelTable();
		}

		public void GetExperienceLevelTableDetoured()
		{
			Debug.Log("GetExperienceLevelTable");
			string text = "player/expleveltable";
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetExperienceLevelTable] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<ExperienceLevelTable>>(httpRequest, new Action<ProfileResponseObject<ExperienceLevelTable>>(this.OnGetExperienceLevelTable), httpErrorHandler, null));
		}

		public void GetCosmeticsByUnlockTypeDetoured(string unlocktype)
		{
			Debug.Log("GetCosmeticsByUnlockType: " + unlocktype);
			string text = "cosmetic/unlock/" + unlocktype;
			HttpRequest httpRequest = new HttpRequest
			{
				Url = ProfileServerProxy.BASE_URL + text
			};
			HttpErrorHandler httpErrorHandler = new HttpErrorHandler
			{
				BaseMessage = "[GetCosmeticsByUnlockType] ",
				Callback = new Action<MetaInfo, bool>(this.OnErrorResponse)
			};
			base.StartCoroutine(HTTPController.GET<ProfileResponseObject<Cosmetic[]>>(httpRequest, new Action<ProfileResponseObject<Cosmetic[]>>(this.OnCosmeticByUnlockType), httpErrorHandler, new CosmeticConverter()));
		}

		public void OnCosmeticByUnlockTypeDetoured(ProfileResponseObject<Cosmetic[]> response)
		{
			if (!response.IsSuccess)
			{
				this.OnErrorResponse(response.Meta, false);
				return;
			}
			LogHandler.PrintDebug("OnCosmeticByUnlockType: Result success! response=" + response.ToJson<ProfileResponseObject<Cosmetic[]>>(), DebugCategory.ProfileServer, null);
			EventHandler<Cosmetic[]> getCosmeticsByUnlockTypeResponse = ProfileServerProxyRebelled.GetCosmeticsByUnlockTypeResponse;
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
			EventHandler<ExperienceLevelTable> getExperienceLevelTableResponse = ProfileServerProxyRebelled.GetExperienceLevelTableResponse;
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
			EventHandler<ProfileProperty<int>[][]> getLeaderboardResponse = ProfileServerProxyRebelled.GetLeaderboardResponse;
			if (getLeaderboardResponse == null)
			{
				return;
			}
			getLeaderboardResponse(this, response.ProfileObject);
		}

		public void OnGetGlobalChatDetoured(ProfileResponseObject<ChatResponse> response)
		{
			EventHandler<ChatResponse> getGlobalChatResponse = ProfileServerProxyRebelled.GetGlobalChatResponse;
			if (getGlobalChatResponse == null)
			{
				return;
			}
			getGlobalChatResponse(this, response.ProfileObject);
		}

		public void OnSendGlobalChatMessageDetoured(ProfileResponseObject<string> response)
		{
			if (!response.IsSuccess)
			{
				LogHandler.PrintError("OnSendGlobalChatMessage: Result success: " + response.IsSuccess.ToString() + ", response=" + response.ToJson<ProfileResponseObject<string>>(), null);
				EventHandler<string> sendGlobalChatMessageResponse = ProfileServerProxyRebelled.SendGlobalChatMessageResponse;
				if (sendGlobalChatMessageResponse == null)
				{
					return;
				}
				sendGlobalChatMessageResponse(this, response.Meta.Message);
				return;
			}
			else
			{
				EventHandler<string> sendGlobalChatMessageResponse2 = ProfileServerProxyRebelled.SendGlobalChatMessageResponse;
				if (sendGlobalChatMessageResponse2 == null)
				{
					return;
				}
				sendGlobalChatMessageResponse2(this, null);
				return;
			}
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
	}
}