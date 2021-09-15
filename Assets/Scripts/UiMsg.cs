using UnityEngine;
using UnityEngine.UI;

public class UiMsg : MonoBehaviour
{
    [SerializeField] Snake snake;
    [SerializeField] RectTransform panel;
    [SerializeField] Text msg;
    void Update()
    {
        if(snake.win || !snake.isAlive){
            msg.text = snake.win ? "YOU WIN" : "YOU LOSE";
            panel.gameObject.SetActive(true);
        }else
          panel.gameObject.SetActive(false);  
    }
}
