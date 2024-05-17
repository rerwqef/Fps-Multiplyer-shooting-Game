using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerList : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI text;

    private Player player;
    public void setPlayerInfo(Player _player)
    {
        _player = player;
        text.text=player.NickName;
    }
}
