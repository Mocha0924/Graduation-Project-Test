using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D rb;
    [SerializeField] private GameObject Bullets;
    [SerializeField] private GameObject ShotPosition;
    [SerializeField] private GameObject AttackCollision;
    private int direction = 1;
    [SerializeField]private float MaxBulletTime;
    private float BulletTime = 0;
    [SerializeField]private Image BulletUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        BulletUI.fillAmount = (MaxBulletTime - BulletTime) / MaxBulletTime;
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(MoveSpeed,0,0)*Time.deltaTime;
            transform.localScale = Vector3.one;
            direction = 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(-1,1,1);
            direction = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0,jumpPower));
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(BulletTime <= 0)
            {
                GameObject bullets = Instantiate(Bullets, ShotPosition.transform.position, Quaternion.identity);
                Bullet bullet = bullets.GetComponent<Bullet>();
                bullet.PowerDirection = direction;
                BulletTime = MaxBulletTime;
            }
            
        }
        if(Input.GetMouseButtonDown(1))
        { 
            AttackCollision.gameObject.SetActive(true);
            Invoke("AttackFinish",0.3f);
        }
        if (!GetComponent<Renderer>().isVisible)
        {
            Vector3 pos = transform.position;
            if(pos.x <0)
                transform.position = new Vector3(9.11f, pos.y, pos.z);
            else
                transform.position =  new Vector3(-9.11f,pos.y,pos.z);
        }

        if(BulletTime >0)
            BulletTime-=Time.deltaTime;
    }

    private void AttackFinish()
    {
        AttackCollision.gameObject.SetActive(false);
    }
}
