using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollect : MonoBehaviour
{
    public GameObject scorePrefab;   // Префаб для отображения баллов
    public Sprite[] scoreSprites;    // Массив спрайтов баллов
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            collision.gameObject.SetActive(false);
            
            int randomScoreIndex = Random.Range(0, scoreSprites.Length);

            // Создаем объект для отображения баллов
            GameObject scoreObject = Instantiate(scorePrefab, collision.gameObject.transform.position, Quaternion.identity);
            scoreObject.GetComponent<SpriteRenderer>().sprite = scoreSprites[randomScoreIndex];

            StartCoroutine(HideAfterDuration(scoreObject, collision));
        }
    }
    
    IEnumerator HideAfterDuration(GameObject gameObject, Collider2D collision)
    {
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
        
        yield return new WaitForSeconds(1f);
        
        collision.gameObject.SetActive(true);
    }
}
