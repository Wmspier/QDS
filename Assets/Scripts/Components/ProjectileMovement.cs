using UnityEngine;
using UnityEngine.UI;

public class ProjectileMovement : MonoBehaviour {

    public float Speed = 1.0f;
    public Vector2 Direction = new Vector2(0, 1);

    private float _spriteWidth;
    private float _speed;
    private Vector3 _killPosition;

    private void Start()
    {
        _spriteWidth = GetComponent<ElementType>().Types[0].rect.width * transform.lossyScale.x;
        _speed = Speed * _spriteWidth;
        _killPosition.x = Screen.width * 1.15f;
        _killPosition.y = Screen.height;

    }
	
	private void Update () {
        var newPosition = transform.position;
        newPosition.x += Time.deltaTime * Direction.x * _speed;
        newPosition.y += Time.deltaTime * Direction.y * _speed;
        transform.position = newPosition;

        if (newPosition.x > _killPosition.x || newPosition.x < -_killPosition.x / 10f)
        {
            Destroy(gameObject);
        }
	}
}
