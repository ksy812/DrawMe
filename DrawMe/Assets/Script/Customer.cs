using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;
    public Sprite[] sprite; 
    public string[] comment;
    
    private string now_comment;
    public static int satisfaction;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.up * 1600;
        satisfaction = -1;


    }
    private void Update()
    {
        if (transform.position.y > 500)
        {
            rigidbody.velocity = Vector2.zero;
        }

    }



    void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 100);
        rigidbody.velocity = Vector2.down * 1600;

    }

    public void Reaction(int satisfaction)
    {
        //1. 이미지 바뀌기 / 대사 바뀌기 (시간차)
        //2. 금액 지불하기
        //3. 밑으로 사라지기 (객체 삭제)

        if (satisfaction > 50)
        {
            spriteRenderer.sprite = sprite[1];  //만족
            now_comment = comment[1];
            //만족스러운 금액 지불
        }
        else
        {
            spriteRenderer.sprite = sprite[2]; //불만족
            now_comment = comment[2];
            //불만족스러운 금액 지불
        }
        Invoke("MoveDown", 1f);
        Destroy(this.gameObject, 1.5f);
    }


    public string getNowComment()
    {   
        return now_comment;
    }
}