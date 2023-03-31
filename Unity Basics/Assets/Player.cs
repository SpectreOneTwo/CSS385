using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private float speed = 7f;
    private Rigidbody2D rb2d;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletSpeed = 10f;
    private float cooldown = 0.5f;
    private float nextFire = 0f;
    private Vector3 mousePos;
    private Camera mainCam;

    void Start () {
      rb2d = GetComponent<Rigidbody2D> ();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
	
    void Update () {
      mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
      
      UpdateMotion();
      ProcessBulletSpwan();
    }

    private void UpdateMotion() {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
    
    private void ProcessBulletSpwan() {
      if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire) {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D re = bullet.GetComponent<Rigidbody2D>();
        re.velocity = firePoint.up * bulletSpeed;
        nextFire = Time.time + cooldown;
      }
    }
}
