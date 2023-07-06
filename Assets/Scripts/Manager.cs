using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using JetBrains.Annotations;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI.Table;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject panelObject;
    [HideInInspector] public List<GameObject> panels = new List<GameObject>();
    [HideInInspector] private List<RenderTexture> renderTextures = new List<RenderTexture>();
    [HideInInspector] private List<Material> renderMaterials = new List<Material>();
    [SerializeField] private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerObject.name, new Vector3(0, 2, 0), Quaternion.identity);
        //AddPlayer(Camera.main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(Camera c)
    {
        RenderTexture r = new RenderTexture(1280, 720, 16, RenderTextureFormat.ARGB32);
        //assign c to the render texture
        c.targetTexture = r;
        renderTextures.Add(r);

        Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.mainTexture = r;
        renderMaterials.Add(m);

        for (int i = 0; i < 15; i++) {
        GameObject go = Instantiate(panelObject);
        go.GetComponent<MeshRenderer>().material = m;
        panels.Add(go);
        }

        RealignViews();

    }

    private void RealignViews()
    {
        int numViews = panels.Count;
        int rows = (int)Mathf.Ceil(Mathf.Sqrt(numViews));
        if (rows == 0) return;

        Vector3 initialScale = new Vector3(192, 1, 108);
        foreach (GameObject go in panels)
        {
            go.GetComponent<RectTransform>().localScale = initialScale / rows / 2;
        }

        for (int row = 0; row < rows - 1; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                int index = row * rows + col;
                if (index < numViews)
                {
                    panels[index].GetComponent<RectTransform>().localPosition = new Vector3(((col + 0.5f) / rows) * 1920 / 2, ((row + 0.5f) / rows) * -1080 / 2 + 1080 / 2, 0); ;
                }
            }
        }

        int remaining = numViews - (rows - 1) * rows;
        for (int i = 0; i < remaining; i++)
        {
            panels[numViews - 1 - i].GetComponent<RectTransform>().localPosition = new Vector3(((i + 0.5f) / rows) * 1920 / 2, ((rows - 0.5f) / rows) * -1080 / 2 + 1080 / 2, 0);
        }
    }
}
