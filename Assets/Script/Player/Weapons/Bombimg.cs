using Fusion;
using System;
using UnityEngine;

public class Bombimg : NetworkBehaviour
{
    public int countBomb;
    [SerializeField] private GameObject[] _bombPlaces;
    public GameObject bomba;
    public Transform bomSalida;

    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;
    }

    public void AddBomb()
    {
        countBomb++;
        CountBombs(countBomb);
    }

    public void SetBomb()
    {
        if(countBomb >= 1)
        {
            Instantiate(bomba, bomSalida.position, bomSalida.rotation);
            countBomb = countBomb - 1;
            CountBombs(countBomb);
        }
    }

    void CountBombs(int b)
    {
        switch(b)
        {
            case 1:
            _bombPlaces[0].SetActive(true);
            _bombPlaces[1].SetActive(false);
            _bombPlaces[2].SetActive(false);
            break;
        case 2:
            _bombPlaces[1].SetActive(true);
            _bombPlaces[2].SetActive(false);
            break;
        case 3:
            _bombPlaces[2].SetActive(true);
            break;
        default:
            _bombPlaces[0].SetActive(false);
            _bombPlaces[1].SetActive(false);
            _bombPlaces[2].SetActive(false);
            break;
        }
    }
}
