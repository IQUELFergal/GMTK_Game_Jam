using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Material material;
    public float respawnLag = 0.5f;
    public GameObject player;
    public GameObject start;

    float dissolveAmount;
    bool isDissolving;

    bool canReset = true;
    bool readyToRespawn = false;
    void Start()
    {
        dissolveAmount = 0;
        material.SetFloat("_DissolveAmount", dissolveAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }

    // reset position
    public void ResetPlayer()
    {
        if (canReset)
        {
            StartCoroutine(RespawnPlayer());
        }
    }


    public void ContinuousResetPlayerPosition()
    {
        if (canReset)
        {
            ResetPlayer();
            StartCoroutine(CancelResetPlayer(3.0f));
        }
    }

    IEnumerator CancelResetPlayer(float duration)
    {
        canReset = false;
        yield return new WaitForSeconds(duration);
        canReset = true;
    }

    IEnumerator RespawnPlayer()
    {
        StartCoroutine(Dissolve());
        while (!readyToRespawn)
        {
            yield return null;
        }
        player.transform.position = start.transform.position;
        StartCoroutine(Spawn());
    }


    IEnumerator Dissolve()
    {
        dissolveAmount = 0;
        while (dissolveAmount < 1)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
            yield return null;
        }
        dissolveAmount = 1;
        player.SetActive(false);
        readyToRespawn = true;
    }

    IEnumerator Spawn()
    {
        readyToRespawn = false;
        player.SetActive(true);
        dissolveAmount = 1;
        while (dissolveAmount > 0)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
            yield return null;
        }
        dissolveAmount = 0;
    }


}
