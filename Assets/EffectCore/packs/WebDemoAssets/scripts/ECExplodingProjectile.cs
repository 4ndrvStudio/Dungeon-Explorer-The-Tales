using UnityEngine;
using System.Collections;

/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project
public class ECExplodingProjectile : MonoBehaviour
{
    public GameObject impactPrefab;
    public GameObject impactNoDecalPrefab;
    public GameObject explosionPrefab;
    public float thrust;

    public Rigidbody thisRigidbody;

    public GameObject particleKillGroup;
    [SerializeField] private Collider thisCollider;

    public bool LookRotation = true;
    public bool Missile = false;
    public Transform missileTarget;
    public float projectileSpeed;
    public float projectileSpeedMultiplier;

    public bool ignorePrevRotation = false;

    public bool explodeOnTimer = false;
    public float explosionTimer;
    float timer;

    private Vector3 previousPosition;

    private bool _isUserOwner = false;
    

    // Use this for initialization
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        if (Missile)
        {
            missileTarget = GameObject.FindWithTag("Target").transform;
        }
        thisCollider = GetComponent<Collider>();
        previousPosition = transform.position;
        if (thisCollider != null) StartCoroutine(OnEnableCol());
    }

    // Update is called once per frame
    void Update()
    {
      
        timer += Time.deltaTime;
        if (timer >= explosionTimer && explodeOnTimer == true)
        {
            Explode();
        }

    }

    void FixedUpdate()
    {


        CheckCollision(previousPosition);

        previousPosition = transform.position;
    }

    void CheckCollision(Vector3 prevPos)
    {
        RaycastHit hit;
        Vector3 direction = transform.position - prevPos;
        Ray ray = new Ray(prevPos, direction);
        float dist = Vector3.Distance(transform.position, prevPos);
        if (Physics.Raycast(ray, out hit, dist))
        {
            string TargetTag = _isUserOwner? "Enemy" : "Player";
            
            if (hit.collider.tag == TargetTag)
            {
                transform.position = hit.point;
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                Vector3 pos = hit.point;
                Instantiate(impactNoDecalPrefab, pos, rot);
                if (!explodeOnTimer && Missile == false)
                {
                    Destroy(gameObject);
                }
                else if (Missile == true)
                {
                    thisCollider.enabled = false;
                    particleKillGroup.SetActive(false);
                    thisRigidbody.velocity = Vector3.zero;
                    Destroy(gameObject, 5);
                }
            }


        }
    }
    
    public void SetIsUserOwner() => _isUserOwner = true;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player" )
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            if (ignorePrevRotation)
            {
                rot = Quaternion.Euler(0, 0, 0);
            }
            Vector3 pos = contact.point;
            Instantiate(impactPrefab, pos, rot);
            if (!explodeOnTimer && Missile == false)
            {
                Destroy(gameObject);
            }
            else if (Missile == true)
            {

                thisCollider.enabled = false;
                particleKillGroup.SetActive(false);
                thisRigidbody.velocity = Vector3.zero;

                Destroy(gameObject, 5);

            }
        }
    }

    IEnumerator OnEnableCol()
    {
        thisCollider.enabled = false;
        yield return new WaitForSeconds(0.01f);
        thisCollider.enabled = true;
    }

    void Explode()
    {
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }

}