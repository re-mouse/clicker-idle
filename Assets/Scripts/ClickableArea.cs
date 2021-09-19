using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableArea : MonoBehaviour
{
    private void OnMouseDown()
    {
        Player.DamageCurrentEntity(Player.GetPlayerStats().baseDamage);
    }
}
