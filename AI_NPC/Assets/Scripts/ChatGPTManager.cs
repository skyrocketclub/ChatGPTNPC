using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;


public class ChatGPTManager : MonoBehaviour
{
    [TextArea(5,20)]
    public string personality;
    [TextArea(5, 20)]
    public string scene;
    public int maxResponseWordsLimit = 15;

    //triggering specific actions from the NPC...
    public List<NPCAction> actions;
    [System.Serializable]
    public struct NPCAction
    {
        public string actionKeyword;
        [TextArea(5, 20)]
        public string actionDescription;

        public UnityEvent actionEvent;
    }

    public OnResponseEvent OnResponse;

    //Custom unity event that takes the string response of chatgpt as a parameter
    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }


    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage> ();

    public string GetInstructions()
    {
        string instructions = "You are a video game character and will answer to the message the player ask you. \n" +
            "You must reply to the player message only using the information from your personality and the scene that are provided afterwards. \n" +
            "Do not invent or create response that are not mentioned in those information \n" +
            "Do not break character or mention you are an AI or a video game character. \n" +
            "You must answer in less than " + maxResponseWordsLimit + " words. \n" +
            "Here is the information about your personality: \n" + personality + "\n" +
            "here is the information about the Scene around you: \n"+
            BuildActionInstructions() + 
            "here is the message of the player: \n";
        return instructions;
    }

    public string BuildActionInstructions()
    {
        string instructions = " ";
        foreach(var item in actions)
        {
            instructions += "if I imply that I want you to do the following: "+ item.actionDescription + ". You must add to your answer the following keyword: "+
                item.actionKeyword + "\n";
        }

        return instructions;
    }

    public async void AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = GetInstructions() + newText;
        newMessage.Role = "user";
        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
           
            foreach(var item in actions)
            {
                if (chatResponse.Content.Contains(item.actionKeyword))
                {
                    string textNoKeyword = chatResponse.Content.Replace(item.actionKeyword, "");
                    chatResponse.Content = textNoKeyword;
                    item.actionEvent.Invoke();
                }
            }

            messages.Add(chatResponse);
            Debug.Log(chatResponse.Content);

            OnResponse.Invoke(chatResponse.Content);
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
