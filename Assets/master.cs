using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class master : MonoBehaviour
{
    private int score = 0;
    private int highScore = 1000;

    private List<GameObject> basketList;

    public GameObject basketPrefab;

    private AudioSource[] audioSrcs;

    // Start is called before the first frame update
    void Start()
    {
        float bottomEdge = transform.position.y + .5f;
        highScore = PlayerPrefs.GetInt("HS", 1000);
        GameObject.Find("HighScoreText").GetComponent<Text>().text = highScore.ToString();

        audioSrcs = GetComponents<AudioSource>();

        basketList = new List<GameObject>();

        for (int i = 0; i < 3; i++)
        {
            GameObject basket = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = basket.transform.position;
            pos.y = bottomEdge + (i * .50f);
            basket.transform.position = pos;
            basketList.Add(basket);
        }

    }

    public void IncreaseScore()
    {

        audioSrcs[0].Play();
        score += 100;

        GameObject.Find("ScoreText").GetComponent<Text>().text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            GameObject.Find("HighScoreText").GetComponent<Text>().text = highScore.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cherry") {
            audioSrcs[1].Play();

            GameObject[] cherryArray = GameObject.FindGameObjectsWithTag("cherry");

            foreach (GameObject cherry in cherryArray)
                Destroy(cherry);

            int topBasket = basketList.Count - 1;

            if (topBasket > -1)
            {
                Destroy(basketList[topBasket]);
                basketList.RemoveAt(topBasket);
            }

            if (basketList.Count <= 0)
            {
                Destroy(GameObject.Find("tree")); 
                PlayerPrefs.SetInt("HS", highScore);
                Invoke("Loader", .5f);
            }
        }
    }
    void Loader()
    {
        SceneManager.LoadScene("MainScene");
    }
}