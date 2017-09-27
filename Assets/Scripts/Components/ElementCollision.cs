using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollision : MonoBehaviour {

    public PlayerLives PlayerLifeReference;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var myType = GetComponent<ElementType>();
        var theirType = collision.gameObject.GetComponent<ElementType>();

        //Both colliders belong to players
		if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Player"))
		{
			PlayerCollission(ref myType, ref theirType);
			return;
		}

        if (myType != null && theirType != null)
        {
            //Either the first or second types are not equal
            if (myType.CurrentTypes[0] != theirType.CurrentTypes[0] || myType.CurrentTypes[1] != theirType.CurrentTypes[1])
            {
                Debug.Log(string.Format("My Type(s):D {0},{1}  |  Their Type(s):D{2},{3}", 
                                        myType.CurrentTypes[0]
                                        ,myType.CurrentTypes[1]
                                        ,theirType.CurrentTypes[0]
                                        ,theirType.CurrentTypes[1]));
                
                if (PlayerLifeReference.Lives > 0)
				{
					EventSystem.instance.Dispatch(new GameEvents.ElementCollisionEvent(false));
                    Debug.LogWarning("<color=blue>DIFF TYPES!!!!</color>");
					PlayerLifeReference.Lives--;
				}
            }
            //Both types are the same
            else
			{
				EventSystem.instance.Dispatch(new GameEvents.ElementCollisionEvent(true));
				Debug.LogWarning("<color=green>SAME TYPES!!!!</color>");
			}
            Destroy(collision.gameObject);
        }
    }

    private void PlayerCollission(ref ElementType player1,ref ElementType player2)
	{
        if(player1.CurrentTypes[0] == player1.CurrentTypes[1])
		{
			player1.CurrentTypes[1] = player2.CurrentTypes[0];
			player2.CurrentTypes[1] = player1.CurrentTypes[0];
        }
    }
}
