# SortFiles
![status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)

**SortFiles** é um organizador de arquivos inteligente para Windows feito em **NET 8.0** que permite **mover, renomear e classificar arquivos** usando uma **linguagem de script intuitiva**, com suporte a **expressões regulares**, **datas** e **tamanho de arquivos**.

---

## Principais Recursos

- Linguagem de regras simples e poderosa
- Suporte a **regex** para filtrar por nome, extensão, etc.
- Manipulação baseada em **datas** 
- Regras baseadas em **tamanho de arquivo**
- Automatize a organização de pastas com lógica personalizada

---

## Parâmetros

- `-p` recebe o caminho da pasta a ser organizada, n nada for passado usará o diretória atual (opcional)
- `-s` recebe uma string contendo a receita para organizar o dirétório

## Sintaxe para o parâmetro S

a sintaxe para organizar um diretório segue a seguinte estrutura

`<funnel_bool><nome_da_pasta>[extensões, regex, datas, tamanhos...];`
### Alguns exemplos válidos a seguir:

suporte para extensões:
- move qualquer arquivo mp4, pdf ou exe para a pasta `teste`

```bash
teste=[.mp4, .pdf, .exe];
```

suporte para Regex:
- move qualquer arquivo que comece com my_file seguido de um número

```bash
teste=[\^my_file\d+\];
```

⚠️ Observação: regex deve estar entre duas `\`

---

suporte para datas:
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
para mais detalhes acesse o Tutorial.md

## Como Usar?

clone o repositório:

```bash
git clone https://github.com/Yuri-Kranholdt/BarChart.git
cd BarChart
```

compile o projeto e execute a aplicação

---
<br>

o script também pode ser adicionado na variavel `PATH` de ambiente do windows para ser chamado de qualquer pasta
