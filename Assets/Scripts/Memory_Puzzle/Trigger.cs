using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player"))
            return;

        Player.getInstance().TeleportToSpawn();

        if(OnStartScene.sceneType.Equals(SceneType.MEMORY_PUZZLE))
            FindObjectOfType<PuzzleTilemap>().Clear();
    }
}