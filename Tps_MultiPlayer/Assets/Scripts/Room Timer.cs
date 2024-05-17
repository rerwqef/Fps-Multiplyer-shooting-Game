using Photon.Pun;

using UnityEngine;
using UnityEngine.UI;

public class RoomTimer : MonoBehaviourPunCallbacks
{


    public Text timerText;
    public float maxTime;
    private float startTime;

    private void Start()
    {
        // Request server time (you can implement this as an RPC)
        photonView.RPC("RequestServerTime", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void RequestServerTime()
    {
        // Get the server's current time
        startTime = PhotonNetwork.ServerTimestamp / 1000f; // Convert to seconds
    }

    private void Update()
    {
       /* if (!PhotonNetwork.IsMasterClient)
            return; */// Only the master client handles time synchronization

        // Calculate remaining time based on server time
        float currentTime = PhotonNetwork.ServerTimestamp / 1000f; // Convert to seconds
        float remainingTime = maxTime - (currentTime - startTime);

        // Update timer display
        UpdateTimerText(remainingTime);

        if (remainingTime <= 0f)
        {
            // Timer expired, handle game over logic
            // ...
        }
    }

    private void UpdateTimerText(float remainingTime)
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        string formattedTime = $"{minutes:D2}:{seconds:D2}";
        timerText.text = formattedTime;
    }
}

