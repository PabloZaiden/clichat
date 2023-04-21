# CLIChat: OpenAI chatbot for the command line

CLIChat is a simple chatbot that runs in the command line. It uses the OpenAI API to generate responses to user input. It is a simple project that I made to learn more about the OpenAI API.

## TL;DR

### Run the published docker image with your own API key

```bash
OPENAI_API_KEY=<your api key>

docker run \
    --platform linux/x86_64 \
    --rm -ti \
    -e OPENAI_API_KEY=$OPENAI_API_KEY \
    ghcr.io/pablozaiden/clichat:latest
```

## Running locally

```bash
export OPENAI_API_KEY=<your api key>

# if you want to override the default prompt, change the prompt.txt file or set the CLIChatPromptEnvVariableName

# if you want to override the default conversation example, change the examples.txt file

dotnet run
```

## Building and running in Docker

```bash
./build-docker.sh

export OPENAI_API_KEY=<your api key>
./run-docker.sh
```

To finish the conversation, type `EXIT`.
