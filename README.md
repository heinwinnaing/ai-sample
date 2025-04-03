# ai-sample

The Meta Llama 3.3 multilingual large language model (LLM) is a pretrained and instruction tuned generative model in 70B (text in/text out). The Llama 3.3 instruction tuned text only model is optimized for multilingual dialogue use cases and outperform many of the available open source and closed chat models on common industry benchmarks.

## CPU only
```cmd
docker run -d -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama:latest
```

## To run and chat with llama3
```cmd
docker exec -it ollama ollama run llama3
```
