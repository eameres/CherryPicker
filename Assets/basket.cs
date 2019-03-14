using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basket : MonoBehaviour
{
    void Update()
    {
        Vector3 rawMouse = Input.mousePosition;
        Vector3 convertedMouse = Camera.main.ScreenToWorldPoint(rawMouse);
        Vector3 pos = transform.position;
        pos.x = convertedMouse.x;
        transform.position = pos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cherry")
        {
            GameObject.Find("edges").GetComponent<master>().IncreaseScore();
            Destroy(collision.gameObject);
        }
    }
}