using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightButtons : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateKeys()
        UpdateMouse();
    }

    void UpdateKeys()
    {
        if(Input.GetAxisRaw("Vertical") > 0){
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(0).gameObject.SetActive(true);            
        }
        if(Input.GetAxisRaw("Vertical") < 0){
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(1).gameObject.SetActive(true);            
        }
        if(Input.GetAxisRaw("Horizontal") > 0){
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(3).gameObject.SetActive(true);            
        }
        if(Input.GetAxisRaw("Horizontal") < 0){
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            transform.GetChild(2).gameObject.SetActive(true);            
        }
    }

    void UpdateMouse()
    {
        if(Input.GetButton("Fire1"))
        {
            transform.GetChild(0).gameObject.SetActive(false);  
            transform.GetChild(1).gameObject.SetActive(true);  
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);  
            transform.GetChild(1).gameObject.SetActive(false); 
            
        }
    }
}
