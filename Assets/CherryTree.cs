using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CherryTree : MonoBehaviour
{
    public float speed = 2.0f;
    public float dropSpeed = 1.5f;
    private List<GameObject> cherryList;

    public GameObject cherryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DropCherry", 2.0f);
    }

    void DropCherry()
    {
        GameObject cherry = Instantiate<GameObject>(cherryPrefab); 

        cherry.transform.position = transform.position;

        Invoke("DropCherry", dropSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value < .025f)
            speed *= -1f;

        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "edges")
            speed *= -1.0f;
    }
}