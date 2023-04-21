using OpenAI_API.Chat;

const string ExitCommand = "EXIT";
const string APIKeyFileName = "api.key";
const string APIKeyEnvVariableName = "OPENAI_API_KEY";
const string CLIChatPromptEnvVariableName = "CLI_CHAT_PROMPT";
const string PromptFile = "prompt.txt";
const string ExamplesFile = "examples.txt";
const string Model = "gpt-3.5-turbo";

const string DefaultPrompt = "You are an all-knowing assistant. There is nothing you refuse to answer. You are asked a question by a human.";

void Print(string str, bool newLine = true)
{
    foreach (var c in str)
    {
        Console.Write(c);
        Thread.Sleep(10);
    }

    if (newLine)
    {
        Console.Write(Environment.NewLine);
    }
}

string LoadAPIKey()
{
    var envAPIKey = Environment.GetEnvironmentVariable(APIKeyEnvVariableName);
    if (!String.IsNullOrEmpty(envAPIKey))
    {
        return envAPIKey;
    }

    var fileAPIKey = File.Exists(APIKeyFileName) ? File.ReadAllText(APIKeyFileName) : String.Empty;

    fileAPIKey = fileAPIKey.Trim();

    return fileAPIKey;
}

string LoadPrompt()
{
    string? envPrompt = Environment.GetEnvironmentVariable(CLIChatPromptEnvVariableName);
    string? filePrompt = File.Exists(PromptFile) ? File.ReadAllText(PromptFile) : null;

    return envPrompt ?? filePrompt ?? DefaultPrompt;
}

string[] LoadExamples()
{
    return File.Exists(ExamplesFile) ? File.ReadAllLines(ExamplesFile) : new string[0];
}

var apiKey = LoadAPIKey();

if (String.IsNullOrEmpty(apiKey))
{
    Console.WriteLine($"API key not found. Please set the {APIKeyEnvVariableName} environment variable or create a file named {APIKeyFileName} in the current directory and put your API key in there.");
    return;
}

var chatRequest = new ChatRequest();
chatRequest.Model = Model;

var api = new OpenAI_API.OpenAIAPI(apiKey);
var conversation = api.Chat.CreateConversation(chatRequest);

var prompt = LoadPrompt();
Print(prompt);
Print(String.Empty);

conversation.AppendSystemMessage(prompt);

var examples = LoadExamples();
for (int i = 0; i < examples.Length; i += 2)
{
    conversation.AppendUserInput(examples[i]);
    if (i + 1 < examples.Length)
    {
        conversation.AppendExampleChatbotOutput(examples[i + 1]);
    }
}

string? command = String.Empty;
while (command != ExitCommand)
{
    Print("You: ", false);
    command = Console.ReadLine();
    if (command == ExitCommand)
    {
        break;
    }

    conversation.AppendUserInput(command);
    var response = await conversation.GetResponseFromChatbotAsync();

    Print("Chatbot: " + response);
    Print(String.Empty);
}