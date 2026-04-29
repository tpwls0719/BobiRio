using UnityEngine;
using System.Collections.Generic;

public class GravityZone : MonoBehaviour
{
    private HashSet<PlayerController> inside = new HashSet<PlayerController>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController pc = col.GetComponent<PlayerController>();
        if (pc != null)
        {
            inside.Add(pc);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        PlayerController pc = col.GetComponent<PlayerController>();
        if (pc != null && inside.Contains(pc))
        {
            pc.ToggleGravity();
            inside.Remove(pc);
        }
    }
}