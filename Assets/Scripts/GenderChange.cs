using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderChange : Subject
{
    public string gender;
    public void NotifySwitches()
    {
        Notify(gender, NotificationType.GenderChange);
    }
}
