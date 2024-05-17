using Photon.Pun;
using UnityEngine;

public class WeponSwicher : MonoBehaviourPun
{
    private int selectedWepon = 0;
      public PhotonView view;
    void Start()
    {
        if (photonView.IsMine)
        {

            SelectWepon();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {
            int previous = selectedWepon;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWepon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedWepon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedWepon = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selectedWepon = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                selectedWepon = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                selectedWepon = 5;
            }
            if (previous != selectedWepon)
            {
                SelectWepon();
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (selectedWepon >= transform.childCount - 1)
                {
                    selectedWepon = 0;
                }
                else
                {
                    selectedWepon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (selectedWepon <= transform.childCount - 1)
                {
                    selectedWepon = transform.childCount - 1;
                }
                else
                {
                    selectedWepon--;
                }
            }
        }
    }

    void SelectWepon()
    {
            photonView.RPC("setTPWepon", RpcTarget.All,selectedWepon); 

        if (photonView.IsMine)
        {
            if (selectedWepon >= transform.childCount)
            {
                selectedWepon = transform.childCount - 1;
            }
            int i = 0;
            foreach (Transform wepon in transform)
            {
                if (i == selectedWepon)
                {
                    wepon.gameObject.SetActive(true);
                }
                else
                {
                    wepon.gameObject.SetActive(false);
                }
                i++;
            }
        }

    }
}
