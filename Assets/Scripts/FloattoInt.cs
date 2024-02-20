using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    
    public class FloattoInt : MonoBehaviour
    {
        public Transform player;
        public int x;
        public int y;
        public int z;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            float temp = player.position.x * 100;
            x = ((int)temp);
            temp = player.position.y * 100;
            y = ((int)temp);
            temp = player.position.z * 100;
            z = ((int)temp);

        }
    }
}
