using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableArea : MonoBehaviour
{

    private void OnMouseDown()
    {
        if (!Entity.IsPointerOverUIElement())
            Player.DamageCurrentEntity(Player.GetPlayerStats().baseDamage);
    }
}
