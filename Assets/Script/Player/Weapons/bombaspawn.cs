using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class bombaspawn : MonoBehaviour
{
    public static bombaspawn Instance { get; private set; }
    public GameObject bomba;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Recharge();
    }
    public void Recharge()
    {
        bomba.SetActive(false);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);

        bomba.SetActive(true);
    }
}
