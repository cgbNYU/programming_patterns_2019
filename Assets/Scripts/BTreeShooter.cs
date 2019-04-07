using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.Networking;

public class BTreeShooter : MonoBehaviour
{

    private Tree<BTreeShooter> _tree; //holds the tree for enemy behavior

    [SerializeField] private GameObject _player;

    [SerializeField] private float _speed;

    [SerializeField] private float _burstRange;

    public int Score;
    public GameObject bullet;
    public float ShotForce;

    private const float MaxHealth = 10;

    [SerializeField] private float _health = MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        _tree = new Tree<BTreeShooter>(new Selector<BTreeShooter>(
            //Enemy prioritizes shooting a burst at player if it is close enough and health is low
            new Sequence<BTreeShooter>(
                new IsVeryDamaged(),
                new IsPlayerInRange(),
                new Burst()
            ),

            //Enemy chases if health is low, but it is not close enough
            new Sequence<BTreeShooter>(
                new IsVeryDamaged(),
                new Chase()
            ),

            //Enemy shoots if health is not low
            new Shoot()
        ));
    }

    // Update is called once per frame
    void Update()
    {
        _tree.Update(this); //go through the tree
    }
    
    #region Functions

    private void ShootAtPlayer()
    {
        Vector3 targetDir = _player.transform.position - gameObject.transform.position;
        GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().AddForce(targetDir * ShotForce);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 targetDir = _player.transform.position - gameObject.transform.position;
        transform.position += targetDir * _speed * Time.deltaTime;
    }

    private void BurstShot()
    {
        Vector3 shotDir = Vector3.zero;
        float angle = 0;
        for (int i = 0; i < 8; i++)
        {
            shotDir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().AddForce(shotDir * ShotForce);
            angle += (2 * Mathf.PI) / 8;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _health--;
            if (_health <= 0)
            {
                EventManager.Instance.Fire(new Events.EnemyDied(Score, gameObject));
            }
        }
    }

    #endregion
    
    #region Nodes
    //Conditions
    private class IsVeryDamaged : Node<BTreeShooter>
    {
        public override bool Update(BTreeShooter enemy)
        {
            return enemy._health < MaxHealth / 2;
        }
    }

    private class IsPlayerInRange : Node<BTreeShooter>
    {
        public override bool Update(BTreeShooter enemy)
        {
            var playerPos = enemy._player.transform.position;
            var enemyPos = enemy.transform.position;
            return Vector3.Distance(playerPos, enemyPos) < enemy._burstRange;
        }
    }
    
    //Actions
    private class Shoot : Node<BTreeShooter>
    {
        public override bool Update(BTreeShooter enemy)
        {
            enemy.ShootAtPlayer();
            return true;
        }
    }

    private class Chase : Node<BTreeShooter>
    {
        public override bool Update(BTreeShooter enemy)
        {
            enemy.MoveTowardsPlayer();
            return true;
        }
    }

    private class Burst : Node<BTreeShooter>
    {
        public override bool Update(BTreeShooter enemy)
        {
            enemy.BurstShot();
            return true;
        }
    }
    #endregion
}
