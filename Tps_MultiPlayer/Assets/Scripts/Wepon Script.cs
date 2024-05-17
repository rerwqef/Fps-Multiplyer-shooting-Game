using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class WeponScript : MonoBehaviourPunCallbacks
{


    public int damge;
    public float fireRate;
    private float NextFire;
    public Camera cam;
    public GameObject hitvfx;
    public int mag = 5;
    public int ammo=30;
    public int magammo = 30;
    public TextMeshProUGUI ammotext;
    public TextMeshProUGUI magtext;

    [Range(0f, 1f)]
    public float recoil = 0.3f;
    [Range(0, 2f)]
    public float recoverPercent = 0.7f;
    public float recoilUp = 1f;
    public float recoilback = 0f;

    private Vector3 orginalPosition;
    private Vector3 recoilVelocity=Vector3.zero;

    private float recoilLegth;
    private float recoverLegth;

    private bool recoiling;
    public bool recovering;
  public  bool shoot=false;
    public GameObject btn;


    public string enmyTag;
  
    private void Start()
    {
        if (photonView.IsMine)
        {
            magtext.text = mag.ToString();
            ammotext.text = ammo + "/" + magammo;
            orginalPosition = transform.localPosition;
            recoilLegth = 0;
            recoverLegth = 1 / fireRate * recoverPercent;

            btn.GetComponent<Button>().onClick.AddListener(fire1);
        }
     
    }
    public void fire1()
    {
        shoot = true;
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (NextFire > 0)
            {
                NextFire -= Time.deltaTime;
            }
            if ( shoot && NextFire <= 0&&ammo>0)
            {
                shoot = false;
                NextFire = 1 / fireRate;
                fire();
                ammo--;
                magtext.text = mag.ToString();
                ammotext.text = ammo + "/" + magammo;
            }
            if (Input.GetKeyDown(KeyCode.R)&&mag>0)
            {
                ReloadCaller();
            }

            if (recoiling)
            {
                Recoil();
            }
            if (recovering)
            {
                Recovering();
            }
       }
     
    }

    public void ReloadCaller()
    {
        Reload();
    }
    void Reload()
    {
        if (mag > 0)
        {
            mag--;
            ammo = magammo;
        }
        magtext.text = mag.ToString();
        ammotext.text = ammo + "/" + magammo;
    }
    void fire()
    {
        recoiling=true;
        recovering = false;
        Ray  ray=new Ray(cam.transform.position, cam.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin,ray.direction, out hit,100) )
        {
            PhotonNetwork.Instantiate(hitvfx.name,hit.point,Quaternion.identity);
            if (hit.transform.gameObject.CompareTag(enmyTag))
            {


                if (hit.transform.gameObject.GetComponent<PlayerHealth>())
                {

                    hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage" +
                        "" +
                        "", RpcTarget.All, damge);
                    if (hit.transform.gameObject.GetComponent<PlayerHealth>().health <= 0)
                    {
                        PhotonNetwork.LocalPlayer.AddScore(1);
                    }
                }
            }
        }
    }
    void Recoil()
    {
        Vector3 finalPosition = new Vector3(orginalPosition.x,orginalPosition.y+recoilUp,orginalPosition.z-recoilback);
        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity,recoilLegth);
  if(transform.localPosition==finalPosition )
        {
            recoiling = false;
            recovering = true;
        }
    }
    void Recovering()
    {
        Vector3 finalPosition = orginalPosition;
        transform.localPosition =
            Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLegth);
        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }
}
