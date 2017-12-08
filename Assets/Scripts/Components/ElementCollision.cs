using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCollision : MonoBehaviour {

    public PlayerLives PlayerLifeReference;
    public bool Enabled = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Enabled)
            return;
        
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
				EventSystem.instance.Dispatch(new GameEvents.ElementCollisionEvent(false));
                if (PlayerLifeReference != null && PlayerLifeReference.Lives > 0)
				{
					PlayerLifeReference.Lives--;
				}


                if(GetComponent<ElementCollection>() != null)
                {
                    GetComponent<ElementCollection>().RemoveElement();
                }
            }
            //Both types are the same
            else
			{
				EventSystem.instance.Dispatch(new GameEvents.ElementCollisionEvent(true));

                if(GetComponent<ElementCollection>() != null)
				{
					GetComponent<ElementCollection>().AddElement();
                }
			}
            Destroy(collision.gameObject);
        }
    }

    private void PlayerCollission(ref ElementType player1,ref ElementType player2)
	{
        if(player1.CurrentTypes[0] == player1.CurrentTypes[1])
		{
            var player1Combination = player1.gameObject.GetComponent<ElementCombination>();
            var player2Combination = player2.gameObject.GetComponent<ElementCombination>();
            if (!player1Combination.enabled || !player2Combination.enabled)
                return;
            if(player1Combination != null && player2Combination != null)
            {
                player1Combination.Combine(player2);
                player2Combination.Combine(player1);
            }
        }
    }
}
