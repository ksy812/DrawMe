using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum COLOR : int
{
    BLACK = 0,
    RED = 1,
    GREEN = 2,
    BLUE = 3
};*/

public class Drawing : MonoBehaviour
{
    public Camera cam;
    [SerializeField]
    public Material defaultMaterial; //라인 렌더러의 Material -> 기본

    private List<LineRenderer> lines;
    private LineRenderer curLine; //지금 그리고있는 라인
    private int positionCount = 2; //처음 생성하는 라인 렌더러의 코너 점의 갯수를 지정. -> 여기서 코너 점이란 무엇인지???
    private Vector2 prevPos = Vector2.zero;

    //********************
    [HideInInspector]
    public Transform hitPage;
    bool d_enable = false;

    [SerializeField]
    private Color lineColor;

    private float lineThickness;

    void Start()
    {
        cam = Camera.main;
        SetColor(0);
        lineThickness = 0.1f;
        Debug.Log("생성");
    }

    void Update()
    {

            Draw();
    }

    void Draw()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Camera.main.transform.forward);
        if (hit.collider == null)
             return;

        if (Input.GetMouseButtonDown(0))
        {
            //if (CheckClick(mousePos))
                CreateLine(mousePos);
        }
        else if (Input.GetMouseButton(0))
        {
                ConnectLine(mousePos);
        }
        /*else if (Input.GetMouseButtonUp(0))
        {
            //lines.Add(curLine);
            //EndLine(gameObject);
        }*/
    }

    void CreateLine(Vector2 mousePos)
    {
        GameObject line = new GameObject("Line");
        //line.name = "Line";
        LineRenderer lineRend = line.AddComponent<LineRenderer>();
        positionCount = 2;

        line.transform.parent = cam.transform;
        line.transform.position = mousePos;

        lineRend.material = new Material(defaultMaterial);//defaultMaterial;

        //color error
        lineRend.material.color = lineColor;

        lineRend.startWidth = lineThickness;
        lineRend.endWidth = lineThickness;
        lineRend.numCornerVertices = 5;

        lineRend.SetPosition(0, mousePos);
        lineRend.SetPosition(1, mousePos);

        curLine = lineRend;
    }

    void ConnectLine(Vector2 mousePos)
    {
        //움직임이 있다면
        if (prevPos != null && Mathf.Abs(Vector2.Distance(prevPos, mousePos)) >= 0.001f)
        {
            prevPos = mousePos;
            positionCount++;
            curLine.positionCount = positionCount;
            curLine.SetPosition(positionCount - 1, mousePos);
        }
    }

    public void SetColor(int color)
    {
        Debug.Log("SetColor: " + color);
        switch (color)
        {
            case -1:
                lineColor = Color.white;
                break;
            case 0://(int)COLOR.BLACK:
                lineColor = Color.black;
                break;
            case 1://(int)COLOR.RED:
                lineColor = Color.red;
                break;
            case 2://(int)COLOR.GREEN:
                lineColor = Color.green;
                break;
            case 3://(int)COLOR.BLUE:
                lineColor = Color.blue;
                break;
            case 4:
                lineColor = Color.yellow;
                break;
        }
    }

/*    bool IsBoard()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hitPage != hit.transform)
            {
                hitPage = hit.transform;
            }
            return true;
        }
        return false;
    }*/

    public void SetClear()
    {
        //lines.Clear();
        Debug.Log("SetClear 호출");
        LineRenderer[] temm = Transform.FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer line in temm)
        {
            Destroy(line.gameObject);
        }
        //Destroy();
    }

    /// <summary>
    /// 클릭한 곳이 캔버스인지 확인
    /// </summary>
    /// <returns></returns>
    bool CheckClick(Vector2 mousePos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, cam.transform.forward);
        if (hit.collider != null)
        {
            GameObject obj = hit.transform.gameObject;
            Debug.Log(obj.name);
            return true;
        }
        Debug.Log("NULL");
        return false;


        /*Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            *//*if (hit.transform.tag == "Canvas")
            {
                Debug.Log("On canvas");
                return true;
            }*//*
            return false;
        }*/
        //return true;
    }


}