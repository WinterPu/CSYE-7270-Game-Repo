using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public HealthController health;

    public float icon_offset;
    public Image icon_prefab;

    public Canvas canvas;

    public Stack<Image> stack;


    public Image Grenade;
    public TextMeshProUGUI txtNumGrenade;

    private int numGrenade = -1;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        stack = new Stack<Image>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        UpdateGrenadeUI();
    }

    void UpdateGUI()
    {
        int val_health = health.val;
        if(val_health != stack.Count)
        {
            if (val_health < stack.Count)
            {
                int amount = val_health <= 0 ? stack.Count : stack.Count - val_health;
                // decrease
                while (amount > 0)
                {
                    Image icon = stack.Pop();
                    Destroy(icon);
                    amount--;
                }
            }
            else
            {
                int amount = val_health - stack.Count;
                while (amount > 0)
                {
                    
                    // increase
                    Image icon = Instantiate(icon_prefab, Vector3.zero, Quaternion.identity);
                    icon.rectTransform.SetParent(canvas.transform, false);
                    
                    
                    

                    Vector2 postion = stack.Count>0 ? stack.Peek().rectTransform.anchoredPosition : Vector2.zero;
                    postion.x += icon_offset;
                    postion.y = -20;
                    icon.rectTransform.anchoredPosition = postion;
                   


                    stack.Push(icon);
                    amount--;
                }
            }
        }
    }


    private void UpdateGrenadeUI()
    {
        if (numGrenade != player.GetAvailableGrenadeNum())
        {
            txtNumGrenade.text = "x " + player.GetAvailableGrenadeNum();
        }
    }
}
