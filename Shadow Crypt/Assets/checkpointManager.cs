using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointManager : MonoBehaviour
{
    private Transform player;
    private static Transform currcp;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
       string checkpointName = PlayerPrefs.GetString("Checkpoint", "");
        if (!string.IsNullOrEmpty(checkpointName))
        {
            //currcp = transform.Find(checkpointName);
            Debug.Log(checkpointName);
            GameObject checkpoint = GameObject.Find(checkpointName);
            currcp = checkpoint.transform;
            if (currcp != null)
            {
                JumpToLatest();
            }
        }
        
    }

    public static void SetCheckPoint(Transform cp) {
        PlayerPrefs.SetString("Checkpoint",cp.name);
        PlayerPrefs.Save();
        currcp = cp;
    }

    public void JumpToLatest() {
        player.position=currcp.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
