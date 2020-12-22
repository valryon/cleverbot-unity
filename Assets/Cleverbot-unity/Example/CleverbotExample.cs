using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CleverbotUnity
{
    public class CleverbotExample : MonoBehaviour
    {
        [Header("Bindings")] public InputField apiKeyField;
        public InputField inputField;
        public Text conversation;

        private bool apiKeyChanged;
        private Cleverbot clerverbot;

        private void Awake()
        {
            apiKeyField.text = PlayerPrefs.GetString("Cleverbot_API_Key");
            apiKeyField.onValueChanged.AddListener((text) =>
            {
                PlayerPrefs.SetString("Cleverbot_API_Key", text);
                apiKeyChanged = true;
                Debug.Log("New API Key");
            });

            inputField.text = string.Empty;
            inputField.onEndEdit.AddListener((t) => SendInput());

            conversation.text = string.Empty;
        }

        public async void SendInput()
        {
            var s = inputField.text;
            inputField.text = string.Empty;
            await SendInput(s);
        }

        public async Task SendInput(string text)
        {
            if (clerverbot == null || apiKeyChanged)
            {
                try
                {
                    clerverbot = new Cleverbot(apiKeyField.text);
                }
                catch (Exception e)
                {
                    conversation.text +=
                        "\nAn error happened when establishing connection with the service.\n" + e.Message + "\n";
                    Debug.LogException(e);
                }
            }

            if (clerverbot == null) return;
            
            conversation.text += "> " + text + "\n";

            var r = await clerverbot.GetResponseAsync(text);

            conversation.text += "< " + r.Response + "\n";
        }
    }
}