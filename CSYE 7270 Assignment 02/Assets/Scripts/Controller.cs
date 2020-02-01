using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    NORMAL_CELL,
    GENERAL_PATHOGENS,
    SPECIAL_VIRUS
}

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public NPCType type =NPCType.NORMAL_CELL;

    void Start()
    {
        if(type == NPCType.NORMAL_CELL)
            Destroy(gameObject, GameManager.Instance.normal_cell_lifetime);
        else if (type == NPCType.GENERAL_PATHOGENS)
            StartCoroutine("DestroyAndAttack", NPCType.GENERAL_PATHOGENS);
        else if (type == NPCType.SPECIAL_VIRUS)
            StartCoroutine("DestroyAndAttack", NPCType.SPECIAL_VIRUS);
    }


    private IEnumerator DestroyAndAttack(NPCType type)
    {
        float time = (type == NPCType.SPECIAL_VIRUS)
            ? GameManager.Instance.special_virus_lifetime
            : GameManager.Instance.general_pathogens_lifetime;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        if(type == NPCType.SPECIAL_VIRUS)
            GameManager.Instance.SpecialVirusAttack();
        if(type == NPCType.GENERAL_PATHOGENS)
            GameManager.Instance.GeneralPathogensAttack();
    }
}
