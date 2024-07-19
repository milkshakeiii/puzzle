using UnityEngine;
using System.Collections;
using Steamworks;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;

public class SteamScript : MonoBehaviour
{
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

    private void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
    }

    private void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);

        }
        else
        {
            Debug.Log("SteamMAnager.Initialized was false");
        }

        // Use ISteamUser::GetAuthTicketForWebApi
        // This will return a ticket that can be used to authenticate with a Web API
        HAuthTicket ticket = Steamworks.SteamUser.GetAuthTicketForWebApi("monstermakers");
        // the ticket will be returned in callback GetTicketForWebApiResponse_t
        
    }
}