# Tutorial
---

## Parâmetros

- `-p` recebe o caminho da pasta a ser organizada, n nada for passado usará o diretória atual (opcional)
- `-s` recebe uma string contendo a receita para organizar o dirétório

## Sintaxe para o parâmetro S

a sintaxe para organizar um diretório segue a seguinte estrutura

`<funnel_bool><nome_da_pasta>[extensões, regex, datas, tamanhos...];`
# Alguns exemplos válidos a seguir:

suporte para extensões:
- move qualquer arquivo mp4, pdf ou exe para a pasta `teste`

```bash
teste=[.mp4, .pdf, .exe];
```

### suporte para Regex:
- move qualquer arquivo que comece com my_file seguido de um número

```bash
teste=[\^my_file\d+\];
```

⚠️ Observação: regex deve estar entre duas `\`

---

### suporte para datas:
- move qualquer arquivo criado em 11/01/2025 para a pasta teste

```bash
teste=[11/01/2025];
```

- move qualquer arquivo q tenha sido criado dps de 11/01/2025 através do sinal `+` na frente

```bash
teste=[+11/01/2025];
```
- move qualquer arquivo q tenha sido criado antes de 11/01/2025 através do sinal `-` na frente

```bash
teste=[-11/01/2025];
```
---

### Suporte para tamanho de arquivos(K, M, G, B):

Sendo K -> KiloBytes, M -> Megabytes, G -> Gigabytes, B -> bytes

- Move arquivos com 513 kiloBytes 
```bash
teste=[513K];
```
- Move arquivos com menos de 1 Gigabyte
```bash
teste=[-1G];
```

- Move arquivos com mais de 513 Megabytes 
```bash
teste=[+513M];
```

---

### Fúnil de arquivos

colocando o `-` na frente do nome da pasta, o programa irá procurar qualquer arquivo que tenha compatibilidade com todas as condições informadas, tornando a pesquisa mais específica


Exemplo:
- move arquivos que sejam menores que 513 e maiores que 111 kilobytes
```bash
-teste=[-513K, +111k];
```
- move arquivos que sejam menores que 513, maiores que 111 kilobytes, que sua data de criação seja menor que 27/06/2025, maior que 31/03/2025 e que seja um mp4
```bash
-teste=[-513K, +111k, -27/06/2025, +31/03/2025, .mp4];
```

---

### Alias

internamente o programa possui algumas whitelists que armazenam varias extensões de arquivos agrupadas de acordo com seu tipo ex:(videos)

assim ao invés de ter q digitar diversos tipos diferentes de extensões

```bash
videos=[.mp4, .wav, .mov, .webm, .mpg, .mpeg, .mkv, .wmv, .avi ....];
```

vc escreveria apenas


```bash
videos=[videos];
```
