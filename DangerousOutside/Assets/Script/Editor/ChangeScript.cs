using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(ChangeBtn))]
public class ChangeScript : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChangeBtn generator = (ChangeBtn)target;
        if (GUILayout.Button("Generate Cubes"))
        {
            Debug.LogWarning(generator.cubePrefab);

            SpriteRenderer sr = generator.cubePrefab.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                Image image = generator.cubePrefab.GetComponent<Image>();

                if (image == null)
                {
                    image = generator.cubePrefab.AddComponent<Image>();
                }
                
                image.raycastTarget = false;
                image.sprite = sr.sprite;
                image.SetNativeSize();
                Vector3 pos = image.rectTransform.anchoredPosition3D;
                pos.x *= 100;
                pos.y *= 100;

                image.rectTransform.anchoredPosition3D = pos;

                Destroy(sr);
            }

            
        }
    }
}
