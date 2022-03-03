using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public CharacterStatus playerStatus;

    #region Singleton
    public static StatusManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    //public void UpdateCharacterStatus(BodyEquipment newItem, BodyEquipment oldItem)
    //{
    //    if (oldItem != null)
    //    {
    //        playerStatus.str -= oldItem.strModifier;
    //        playerStatus.hp -= oldItem.hpModifier;
    //    }
    //    playerStatus.str = playerStatus.baseStr + newItem.strModifier;
    //    playerStatus.hp = playerStatus.baseHp + newItem.hpModifier;
    //}
}
