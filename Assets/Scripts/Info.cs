using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Info : MonoBehaviour
    {
        [SerializeField] ClientPlayerAvatarRuntimeCollection m_PlayerAvatars;

        ServerCharacter m_OwnedServerCharacter;

        ClientPlayerAvatar m_OwnedPlayerAvatar;

        public string m_name;

        public ulong m_id;
       

        void Awake()
        {
            m_PlayerAvatars.ItemAdded += PlayerAvatarAdded;
            //InvokeRepeating("stampa", 3.0f, 3.0f);
        }

        void stampa()
        {
            Debug.Log("nome = " + m_name + "ID = " + m_id);
        }

        void PlayerAvatarAdded(ClientPlayerAvatar clientPlayerAvatar)
        {
            if (clientPlayerAvatar.IsOwner)
            {
                SetHeroData(clientPlayerAvatar);
            }
        }

        void SetHeroData(ClientPlayerAvatar clientPlayerAvatar)
        {
            m_OwnedServerCharacter = clientPlayerAvatar.GetComponent<ServerCharacter>();

            Assert.IsTrue(m_OwnedServerCharacter, "ServerCharacter component not found on ClientPlayerAvatar");

            m_OwnedPlayerAvatar = clientPlayerAvatar;

            m_name = GetPlayerName(m_OwnedServerCharacter);

            m_id = m_OwnedPlayerAvatar.OwnerClientId;
            
        }

        string GetPlayerName(Component component)
        {
            var networkName = component.GetComponent<NetworkNameState>();
            return networkName.Name.Value;
        }

        public string getName()
        {
            return m_name;
        }

        public ulong getId()
        {
            return m_id;
        }
    }
}
