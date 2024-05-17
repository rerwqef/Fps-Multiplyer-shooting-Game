
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
public class LeaderBord : MonoBehaviour
{
    public GameObject playerHolder;
    public float refrashRate = 1f;
    public GameObject[] sloats;
    public TextMeshProUGUI[] scoretext;
    public TextMeshProUGUI[] naemText;
  public  TeamScore score;

    public void Start()
    {

        InvokeRepeating(nameof(Refresh), 1f, refrashRate);
    }
    public void Refresh()
    {
        foreach (var slot in sloats)
        {
            slot.gameObject.SetActive(false);
        }
        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();
        int i = 0;
        foreach (var player in sortedPlayerList)
        {

            sloats[i].SetActive(true);
            if (player.NickName == "")
            {
                player.NickName = "Unnamed";
            }
            naemText[i].text = player.NickName;
            scoretext[i].text = player.GetScore().ToString();
        //    UpdateUiScoreHealth(player);
            i++;
        }
    }
    private void Update()
    {
        playerHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }

  /*  public void UpdateUiScoreHealth(Player player)
    {
        string localPlayerNickname = player.NickName;

        if (TeamManger.Instance.teamAPlayers.Exists(player => player.NickName == localPlayerNickname))
        {
            score.TeamAScore++;
            // photonView.RPC("UpdateUiScore", RpcTarget.All, 0, 1);
        }
        else
        {
           score.TeamBScore++;
            //photonView.RPC("UpdateUiScore", RpcTarget.All, 1, 0);
        }
        score.UpdateScoreDisplay();
    }*/
}
                                                                                                                                                                                                                                                                                                                                                                            