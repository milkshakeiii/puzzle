using UnityEngine;
using System;
using Steamworks;
 
public class PlayerID : MonoBehaviour
{
    Callback<GetTicketForWebApiResponse_t> m_AuthTicketForWebApiResponseCallback;
    HAuthTicket m_AuthTicket;
    private string m_SessionTicket;
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            SignInWithSteam();        
        }
        void SignInWithSteam()
        {
            m_AuthTicketForWebApiResponseCallback = Callback<GetTicketForWebApiResponse_t>.Create(OnAuthCallback);
            m_AuthTicket = SteamUser.GetAuthTicketForWebApi("monstermakers");        
        }
        void OnAuthCallback(GetTicketForWebApiResponse_t callback)
        {
            m_SessionTicket = BitConverter.ToString(callback.m_rgubTicket).Replace("-", string.Empty);
            Debug.Log(m_SessionTicket);
        }
    }
}