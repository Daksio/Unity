using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dog : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private Animator anim;
    private SpriteRenderer sprite;
    
    public GameManager gameManager;
    [SerializeField] private GameObject deathPanel;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            sprite.flipX = true;
            if (currentWaypointIndex >= waypoints.Length)
            {
                sprite.flipX = false;
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            collision.gameObject.SetActive(false);
            anim.SetBool("fight", true);
    
            StartCoroutine(RestartAfterDuration());
        }
    }
    
    IEnumerator RestartAfterDuration()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
        yield return new WaitForSeconds(2f);
        gameManager.RestartScene();
    }
}
