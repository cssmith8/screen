using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using JetBrains.Annotations;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UI;

public class Manager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject panelObject;
    [HideInInspector] public List<GameObject> panels = new List<GameObject>();
    [HideInInspector] private List<RenderTexture> renderTextures = new List<RenderTexture>();
    [HideInInspector] private List<Photon.Realtime.Player> players = new List<Photon.Realtime.Player>();
    //[HideInInspector] private List<Material> renderMaterials = new List<Material>();
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

    public void AddPlayer(Camera c, Photon.Realtime.Player p)
    {
        RenderTexture r = new RenderTexture(1280, 720, 16, RenderTextureFormat.ARGB32);
        //assign c to the render texture
        c.targetTexture = r;
        renderTextures.Add(r);

        //Material m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //m.mainTexture = r;
        //renderMaterials.Add(m);

        players.Add(p);
        
        GameObject go = Instantiate(panelObject);
        go.transform.SetParent(transform.GetChild(0));
        go.GetComponent<RawImage>().texture = r;
        panels.Add(go);

        RealignViews();

    }

    private void RealignViews()
    {
        int numViews = panels.Count;
        int rows = (int)Mathf.Ceil(Mathf.Sqrt(numViews));
        if (rows == 0) return;

        Vector3 initialScale = new Vector3(19.2f, 10.8f, 1);
        foreach (GameObject go in panels)
        {
            go.GetComponent<RectTransform>().localScale = initialScale / rows;
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < rows; col++)
            {
                int index = row * rows + col;
                if (index < numViews)
                {
                    panels[index].GetComponent<RectTransform>().localPosition = new Vector3(((col + 0.5f) / rows) * 1920 - 960, ((row + 0.5f) / rows) * -1080 + 540, 0); ;
                }
            }
        }

        /*
        int remaining = numViews - (rows - 1) * rows;
        for (int i = 0; i < remaining; i++)
        {
            panels[numViews - 1 - i].GetComponent<RectTransform>().localPosition = new Vector3(((i + 0.5f) / rows) * 1920 - 960, ((rows - 0.5f) / rows) * -1080 + 540, 0);
        }
        //*/

        int emptyRows = ((int)Mathf.Pow((float)rows, 2f) - numViews) / rows;
        foreach(GameObject go in panels)
        {
            go.GetComponent<RectTransform>().localPosition += new Vector3(0, -1080 / rows / 2 * emptyRows, 0);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player p)
    {
        for (int i = 0; i < players.Count; i++) {
            if (players[i] == p)
            {
                Destroy(panels[i]);
                panels.RemoveAt(i);
                renderTextures.RemoveAt(i);
                players.RemoveAt(i);
                RealignViews();
                i--;
            }
        }
    }
}
