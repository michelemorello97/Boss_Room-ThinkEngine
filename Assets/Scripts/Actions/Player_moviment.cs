using System.Collections;
using System.Collections.Generic;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Player_moviment : Action
    {
        public int x { get; set; }
        public int z { get; set; }

        GameObject player; 
        public override void Do()
        {
            player = GameObject.Find("PlayerAvatar0");
            if (player != null)
            {
                ClientInputSender script2 = player.GetComponent<ClientInputSender>();

                // Assicurarsi che lo script2 sia presente
                if (script2 != null)
                {
                    float x_m = x / 100f;
                    float z_m = z / 100f;
                    // Chiamare la funzione desiderata in Script2
                    script2.TestWorld(x_m, z_m);
                }
                else
                {
                    Debug.LogError("ClientInputSender non trovato su PlayerAvatar.");
                }
            }
            else
            {
                Debug.LogError("PlayerAvatar non trovato.");
            }
           
        }

        public override State Done()
        {
            float x_m = x / 100f;
            float z_m = z / 100f;
            player = GameObject.Find("PlayerAvatar0");
             if (player != null)
             {
                 Transform pos = player.GetComponent<Transform>();
                
                 // Assicurarsi che lo script2 sia presente
                 if (pos != null && ((pos.position.x < (x_m-1f) || pos.position.x > x_m)  || (pos.position.z < (z_m-1f) || pos.position.z > z_m)))
                 {
                     return State.WAIT;
                 }
                 else
                 {

                     return State.READY;
                 }
             }
             else
             {

                 return State.READY;
             }
        }

        public override State Prerequisite()
        {
            float x_m = x / 100f;
            float z_m = z / 100f;

            player = GameObject.Find("PlayerAvatar0");
            if (player != null)
            {
                Transform pos = player.GetComponent<Transform>();
                
                // Assicurarsi che lo script2 sia presente
                if (pos != null && (pos.position.x != x_m || pos.position.z != z_m))
                {
                    return State.READY;
                }
                else
                {
                    
                    return State.ABORT;
                }
            }
            else
            {
                
                return State.ABORT;
            }

        
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
