using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityZone : MonoBehaviour
{
    public string targetTag = "Bobi";

    private HashSet<PlayerController> cooldownPlayers = new HashSet<PlayerController>();

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag(targetTag))
            return;

        PlayerController pc = col.GetComponent<PlayerController>();

        if (pc == null)
            return;

        // 이미 처리중이면 무시
        if (cooldownPlayers.Contains(pc))
            return;

        pc.ToggleGravity();

        StartCoroutine(GravityCooldown(pc));
    }

    IEnumerator GravityCooldown(PlayerController pc)
    {
        cooldownPlayers.Add(pc);

        yield return new WaitForSeconds(0.5f);

        cooldownPlayers.Remove(pc);
    }
}