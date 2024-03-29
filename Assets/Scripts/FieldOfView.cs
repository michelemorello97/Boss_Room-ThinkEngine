using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class FieldOfView : MonoBehaviour
    {
        public List<GameObject> view = new List<GameObject>();
        public List<Vector3Int> pos = new List<Vector3Int>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Imp") && !view.Contains(other.gameObject))
            {
                // Aggiunge l'oggetto alla lista solo se non è già presente
                
                view.Add(other.gameObject);

                Vector3Int tmp = new Vector3Int();
                float aux = other.transform.position.x * 100f;
                tmp.x = (int)aux;
                aux = other.transform.position.y * 100f;
                tmp.y = (int)aux;
                aux = other.transform.position.z * 100f;
                tmp.z = (int)aux;

                pos.Add(tmp);
                // Puoi eseguire altre azioni qui se necessario
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (view.Contains(other.gameObject))
            {
                // Rimuove l'oggetto dalla lista quando esce dal trigger
                int it = view.IndexOf(other.gameObject);

                view.Remove(other.gameObject);

                pos.RemoveAt(it);
                // Puoi eseguire altre azioni qui se necessario
            }
        }

        private void FixedUpdate()
        {
            if (view.Count != 0)
            {
                Vector3Int tmp = new Vector3Int();
                float aux;
                for (int it = 0; it < view.Count; it++)
                {
                    if (view[it] == null)
                    {
                        view.RemoveAt(it);
                        pos.RemoveAt(it);
                    }
                    else
                    {
                        aux = view[it].transform.position.x * 100f;
                        tmp.x = (int)aux;
                        aux = view[it].transform.position.y * 100f;
                        tmp.y = (int)aux;
                        aux = view[it].transform.position.z * 100f;
                        tmp.z = (int)aux;
                        pos[it] = tmp;
                    }
                }
            }
            GameObject player = GameObject.Find("PlayerAvatar0");

            if(player != null)
            {
                ClientInputSender script2 = player.GetComponent<ClientInputSender>();
                FieldOfView fov = player.GetComponentInChildren<FieldOfView>();
                if (fov != null)
                {
                    GameObject obj = fov.getAt(0);
                    if (obj != null)
                    {
                        ServerCharacter servercharacter = obj.GetComponent<ServerCharacter>();
                        if (servercharacter != null)
                            script2.setTarget(servercharacter);
                    }

                }
                else
                {
                    Debug.LogError("FieldOfView non trovato su PlayerAvatar.");
                }
            }
        }

        public GameObject getAt(int index)
        {
            if(index < view.Count)
                return view[index];
            return null;
        }
    }
}
