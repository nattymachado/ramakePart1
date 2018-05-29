using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour {

    public GameObject prefab;
    public GameObject pill = null;
    private GameObject pillPart2 = null;
    private SpriteRenderer board = null;
    public float speed;

    Color[] colors = new Color[] { Color.red, Color.yellow, Color.cyan};

    // Use this for initialization
    public void Start () {
        board = GetComponent<SpriteRenderer>();
    }
    
	// Update is called once per frame
	public void Update () {
        
        if (pill == null)
        {
            createNewPill();

        } else
        {
            if (board.transform.localPosition.y > (pill.transform.localPosition.y - speed))
            {
                pill = null;
            }
            
        }
        
        

    }

    private void createNewPill()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        pill = Instantiate(prefab, pos, Quaternion.Euler(0, 0, 0));
        SpriteRenderer[] pillParts =  pill.GetComponentsInChildren<SpriteRenderer>();
        pillParts[0].color = colors[Random.Range(0, colors.Length)];
        pillParts[1].color = colors[Random.Range(0, colors.Length)];

        float initial_y = board.size.y;
        float initial_x = (board.size.x - (pillParts[0].size.x * 1.9f));
        Vector3 newPosition = new Vector3(initial_x, initial_y, 0);

        pill.transform.parent = board.transform;
        pill.transform.localPosition = newPosition;
        
    }
}
