using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector3 Power;
    public float PowerDirection;
    void Update()
    {
        transform.position += new Vector3(Power.x*PowerDirection, Power.y, Power.z) * Time.deltaTime;

        if (!GetComponent<Renderer>().isVisible)
        {
            Vector3 pos = transform.position;
            if (pos.x < 0)
                transform.position = new Vector3(9.11f, pos.y, pos.z);
            else
                transform.position = new Vector3(-9.11f, pos.y, pos.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Debug.Log("a");
            PowerDirection *= -1.5f;

            if (Input.GetKey(KeyCode.W))
            {
                Power = new Vector3(Power.x, Power.y + 2, Power.z);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Power = new Vector3(Power.x, Power.y - 2, Power.z);
            }

        }
        else
            Destroy(this.gameObject);
    }
}
