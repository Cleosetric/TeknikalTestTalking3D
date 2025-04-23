using Neocortex.Data;
using UnityEngine;

namespace Neocortex.Samples
{
    public class AudioChatSample : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [Header("Neocortex Components")]
        [SerializeField]
        private AudioReceiver audioReceiver;

        [SerializeField]
        private NeocortexSmartAgent agent;

        [SerializeField]
        private NeocortexThinkingIndicator thinking;

        [SerializeField]
        private NeocortexChatPanel chatPanel;

        [SerializeField]
        private NeocortexAudioChatInput audioChatInput;

        [SerializeField]
        private AssistantAnimController animController;

        private void Start()
        {
            agent.OnTranscriptionReceived.AddListener(OnTranscriptionReceived);
            agent.OnChatResponseReceived.AddListener(OnChatResponseReceived);
            agent.OnAudioResponseReceived.AddListener(OnAudioResponseReceived);
            audioReceiver.OnAudioRecorded.AddListener(OnAudioRecorded);
        }

        private void StartMicrophone()
        {
            animController.SetListeningAnimation(false);
            animController.SetThinkingAnimation(false);
            animController.SetTalkingAnimation(false);
            audioReceiver.StartMicrophone();
        }

        private void OnAudioRecorded(AudioClip clip)
        {
            agent.AudioToAudio(clip);
            thinking.Display(true);
            audioChatInput.SetChatState(false);
        }

        private void OnTranscriptionReceived(string transcription)
        {
            chatPanel.AddMessage(transcription, true);
        }

        private void OnChatResponseReceived(ChatResponse response)
        {
            chatPanel.AddMessage(response.message, false);

            string action = response.action;
            if (!string.IsNullOrEmpty(action))
            {
                Debug.Log($"[ACTION] {action}");
            }
        }

        private void OnAudioResponseReceived(AudioClip audioClip)
        {
            animController.SetThinkingAnimation(false);
            animController.SetTalkingAnimation(true);
            audioSource.clip = audioClip;
            audioSource.Play();

            Invoke(nameof(StartMicrophone), audioClip.length);
            thinking.Display(false);
            audioChatInput.SetChatState(true);
        }
    }
}
