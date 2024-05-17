using Photon.Pun;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPun
{
    public PlayerMovement mov;
    public PlayerHealth health;
    public GameObject cam;
    public GameObject[] SOME;
   public Transform thiredPersonWeponHollder;

/*    public WeponScript[] weponscrript;*/

  
    public void IslocalPlayer()
    {

            mov.enabled = true;
          health.enabled = true;

            cam.SetActive(true);

            for (int I = 0; I < SOME.Length; I++)
            {
                SOME[I].SetActive(true);
            }

            thiredPersonWeponHollder.gameObject.SetActive(false);
        
      
    }
    [PunRPC]
    public void setTPWepon(int index)
    {
        
            //   if (!photonView.IsMine)
            //   {


            foreach (Transform wepomn in thiredPersonWeponHollder)
            {
                wepomn.gameObject.SetActive(false);
            }
            thiredPersonWeponHollder.GetChild(index).gameObject.SetActive(true);

        


    }


    public void shoot(int m)
    {
       
    }
}
