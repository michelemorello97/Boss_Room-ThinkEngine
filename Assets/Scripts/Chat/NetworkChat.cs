using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Utils;
using Unity.Collections;
using Unity.BossRoom.Gameplay.GameplayObjects;
using UnityEngine.Assertions;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class NetworkChat : NetworkBehaviour
    {

        [SerializeField] ClientPlayerAvatarRuntimeCollection m_PlayerAvatars;
        [SerializeField] private TMP_Text textArea;
        [SerializeField] private TMP_InputField textInput;
        ServerCharacter m_OwnedServerCharacter;

        ClientPlayerAvatar m_OwnedPlayerAvatar;

        string m_name;
        // Start is called before the first frame update
        void Start()
        {
            textArea.SetText("");
        }

        void Awake()
        {
            m_PlayerAvatars.ItemAdded += PlayerAvatarAdded;
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
        }

        string GetPlayerName(Component component)
        {
            var networkName = component.GetComponent<NetworkNameState>();
            return networkName.Name.Value;
        }

        public void SendMessage()
        {
            if (textInput.text.Equals("") == false)
            {
                
                AddTextServerRPC(m_name+ ":- " + textInput.text);
                textInput.SetTextWithoutNotify("");
            }
        }

        [ServerRpc(RequireOwnership = false)]
        void AddTextServerRPC(string text)
        {
            AddTextClientRPC(text);
        }

        [ClientRpc]
        void AddTextClientRPC(string text)
        {
            AddText(text);
        }

        void AddText(string chat)
        {
            string lastText = textArea.text;
            textArea.SetText(lastText + "\n" + chat);
        }

        public override void OnNetworkDespawn()
        {
            textArea.SetText("");
            base.OnNetworkDespawn();
        }
    }
}
