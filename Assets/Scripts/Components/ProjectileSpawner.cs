using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileSpawner : MonoBehaviour {

	[Tooltip("Frequency in seconds of projectile spawn.")]
	public float Frequency = 1f;
	[Tooltip("Spacing between rows in percentage of screen")]
	public float RowSpacing = 0.25f;
    [Tooltip("Number of rows to spawn.")]
	public byte RowCount = 4;
    [Tooltip("Reference to PlayerType script to get types")]
	public ElementType PlayerTypeReference;
    [Tooltip("Prefab of the projectile (must own projectile script)")]
    public GameObject ProjectilePrefab;


    [Tooltip("The percentage to spawn a multi-type projectile")]
    [Range(0f, 1.0f)]
    public float MultiTypeChance = 0.5f;

    private float _timer = 0f;
    private Vector2 _origin;
    private List<int> _playerTypes = new List<int>();
	private readonly List<int> _spawnHistory = new List<int>();
	private readonly List<Sprite> _projectileHistory = new List<Sprite>();
    private List<Sprite> _spriteList = new List<Sprite>();
    private GameObject _projectileContainer;

    // Use this for initialization
    private void Start()
    {
        _origin = transform.position;

        if(PlayerTypeReference == null)
        {
            Debug.LogWarning("ProjectileSpawner requires a player type reference!");
        }
        _spriteList = PlayerTypeReference.Types;
        _projectileContainer = new GameObject("[Projectile_Container]");
        _projectileContainer.transform.parent = transform.parent;

        foreach(var playerType in FindObjectsOfType<ElementType>())
        {
            _playerTypes.Add(playerType.CurrentTypes[0]);
        }
        if (_playerTypes.Count == 0)
            this.enabled = false;
    }
	
	// Update is called once per frame
	private void Update () {

        _timer += Time.deltaTime;
        if (_timer >= Frequency)
        {
            _timer = 0f;
            SpawnProjectile();
        }
		
	}

    private void SpawnProjectile()
    {
        //Reset the spawn position history if it contains all the posible positions
        if (_spawnHistory.Count >= RowCount-1)
        {
            _spawnHistory.Clear();
        }

        //Find a random spawn position that is not within the history
        var SpawnIndex = Random.Range(-RowCount / 2, RowCount / 2+1);
        while(_spawnHistory.Contains(SpawnIndex))
        {
            SpawnIndex = Random.Range(-RowCount / 2, RowCount / 2+1);
        }
        _spawnHistory.Add(SpawnIndex);

        //Calculate the spawn position in screen space based on RowSpacing
        var spawnSpacing = Screen.height * RowSpacing * SpawnIndex;
        var spawnPosition = _origin;
        spawnPosition.y += spawnSpacing;

        //Reset the projectile sprite history if it contains all posible types
        if(_projectileHistory.Count >= _spriteList.Count-1)
        {
            _projectileHistory.Clear();
        }

        //Find a random projectile sprite that is not within the history
        var spriteIndex = _playerTypes[Random.Range(0, _playerTypes.Count)];
        _projectileHistory.Add(_spriteList[spriteIndex]);

        var otherSpriteIndex = _playerTypes[Random.Range(0, _playerTypes.Count)];

        var projectile = Instantiate(ProjectilePrefab, spawnPosition, new Quaternion(), _projectileContainer.transform);
		var projectileType = projectile.GetComponent<ElementType>();
        projectileType.Types = PlayerTypeReference.Types;
        projectileType.transform.localScale = Vector3.one;

        var randForMulti = Random.Range(0, 10);
        if((MultiTypeChance * 10f) > randForMulti)
		{
			projectileType.CurrentTypes[0] = spriteIndex;
			projectileType.CurrentTypes[1] = otherSpriteIndex;
        }
        else
		{
			projectileType.CurrentTypes[0] = spriteIndex;
			projectileType.CurrentTypes[1] = spriteIndex;
        }
        projectileType.UpdateSprites();
    }
}
