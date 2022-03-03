using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager Instance;

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
    #endregion Singleton

    public Item[] currentEquipment;

    public delegate void OnEquipmentChangedCallback();
    public OnEquipmentChangedCallback onEquipmentChangedCallback;

    //private void Start()
    //{
    //    int numSlots = System.Enum.GetNames(typeof(EquipType)).Length;
    //    currentEquipment = new BodyEquipment[numSlots];
    //}

    //public void Equip(BodyEquipment newItem)
    //{
    //    int equipSlot = (int)newItem.equipType;

    //    BodyEquipment oldItem = null;

    //    if (currentEquipment[equipSlot] != null)
    //    {
    //        oldItem = currentEquipment[equipSlot];
    //        Inventory.Instance.AddItem(oldItem);
    //    }

    //    currentEquipment[equipSlot] = newItem;

    //    StatusManager.Instance.UpdateCharacterStatus(newItem, oldItem);

    //    onEquipmentChangedCallback.Invoke();
    //}
}
