using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateButton : MonoBehaviour
{

    public UltimateSkill ultimateSkill;

    public void ShotUltimateSkill()
    {
        UltimateSkill tempObj = ObjectPoolManager.GetInstance().ultimatePool.PopObject().GetComponent<UltimateSkill>();

        tempObj.Shot();
    }
}
