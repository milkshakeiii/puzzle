using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDisplayer : MonoBehaviour
{
    public GameObject linkPrefab;
    public GameObject treeHead;

    // Start is called before the first frame update
    void Start()
    {
        DisplayLinks(treeHead);
    }

    private void DisplayLinks(GameObject parent)
    {
        // recursively instantiate links and place them between parent and each of its children
        foreach (Transform child in parent.transform)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            // set the position of the link to the middle point between parent and child
            link.transform.position = (parent.transform.position + child.position) / 2;
            // rotate the link so that it points from parent to child
            link.transform.up = child.position - parent.transform.position;

            // recursively display the links of the child
            DisplayLinks(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
