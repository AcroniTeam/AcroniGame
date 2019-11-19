using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static bool isSet = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSet)
            return;

        if (!collision.tag.Equals("Player"))
            return;

        isSet = true;

        Player.getInstance().TeleportToSpawn();

        if(OnStartScene.sceneType.Equals(SceneType.MEMORY_PUZZLE))
            FindObjectOfType<PuzzleTilemap>().Clear();

        Player.getInstance().TakeAHeart();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isSet = false;
    }
}