using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_order : MonoBehaviour
{
    public GameObject[] customers;
    public Transform spawnPoint;
    private GameObject obj;

    public Text orderText;
    public Image textBox;
    public Button okButton;

    private void Start()
    {

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow)) //이제 위쪽 화살표 말고 게임 시작 | 앞 고객 완료 되면 객체 생성
        {
            //customer 랜덤뽑기 넣기
            obj = Instantiate(customers[0], spawnPoint.position, spawnPoint.rotation);
            orderText.text = obj.GetComponent<Customer>().comment[0];
            textBox.transform.position = new Vector2(textBox.transform.position.x, textBox.transform.position.y + 1000);
            okButton.transform.position = new Vector2(okButton.transform.position.x, okButton.transform.position.y + 1000);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) //아래쪽 화살표 말고 만족도가 -1이 아니면 리액션 후 객체 삭제
        {
            obj.GetComponent<Customer>().Reaction(10);
            orderText.text = obj.GetComponent<Customer>().getNowComment();
            Invoke("TextboxDown", 1.1f);
        }
    }

    void TextboxDown()
    {
        textBox.transform.position = new Vector2(textBox.transform.position.x, textBox.transform.position.y - 1000);
        okButton.transform.position = new Vector2(okButton.transform.position.x, okButton.transform.position.y - 1000);
    }


}