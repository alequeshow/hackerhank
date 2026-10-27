# GanhoCapital - Calculadora de Impostos sobre Capital

## Sobre o Projeto

O GanhoCapital é uma aplicação em C# (.NET 8.0) que calcula impostos sobre ganhos de capital em operações de compra e venda de ações. A aplicação processa eventos de mercado em formato JSON e retorna os impostos devidos para cada operação.

### Arquitetura da Aplicação

A aplicação foi estruturada seguindo princípios de **Clean Architecture** e **SOLID**, com separação clara de responsabilidades:

#### 1. **Camada de Domínio (`Domain/`)**
- **`MarketEvent`**: Representa um evento de mercado (compra/venda)
- **`OperationResult`**: Representa o resultado de uma operação (imposto calculado)
- **`OperationGain`**: Classe principal que contém a lógica de negócio para cálculo de impostos
- **`OperationType`**: Enum que define os tipos de operação (Buy/Sell)

#### 2. **Camada de Serviços (`Services/`)**
- **`IOperationHandlerService`**: Interface que define o contrato para processamento de operações
- **`OperationHandlerService`**: Implementação do serviço que orquestra o processamento

#### 3. **Camada de Aplicação**
- **`Program.cs`**: Ponto de entrada da aplicação, responsável por:
  - Leitura de entrada JSON via `Console.In`
  - Processamento assíncrono dos dados
  - Tratamento de exceções
  - Serialização/deserialização JSON

### Decisões Técnicas Importantes

- Escolha 100% de bibliotecas nativas da plataforma netcore.
- Utilização do `Microsoft.Extensions.DependencyInjection` para gerenciamento de dependências
  - Uso de scopes para garantir o ciclo de vida correto dos serviços 
  - Abordagem cumpre o requisito de negócio de que as transações não devem guardar estado ao serem configuradas Transients.
- Uso do `System.Text.Json` para serialização/deserialização

#### Lógica de Negócio
A classe `OperationGain` implementa as regras de negócio:

- **Preço Médio Ponderado**: Calcula automaticamente o preço médio das ações conforme novas compras
- **Threshold de R$ 20.000**: Operações abaixo deste valor são isentas de imposto
- **Compensação de Perdas**: Perdas anteriores são deduzidas de ganhos futuros
- **Taxa de 20%**: Aplicada sobre ganhos líquidos acima do threshold

#### Padrão Iterator
- Uso de `yield return` para processamento eficiente de grandes volumes de dados
- Implementação lazy evaluation que processa operações sob demanda

#### Tratamento de Casos Extremos
- Proteção contra divisão por zero
- Validação de quantidades negativas ou zero
- Reinicialização correta do preço médio quando todas as ações são vendidas

## Como Compilar e Executar o Projeto

### Pré-requisitos

**Para Execução Local:**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- Sistema operacional compatível (Windows, Linux, macOS)

**Para Execução em Container:**
- [Docker](https://www.docker.com/get-started) instalado e configurado

### Execução Local

#### **1. Compilação**

```bash
# Clone o repositório (se ainda não tiver)
git clone <url-do-repositorio>
cd GanhoCapital

# Navegue até o diretório do projeto
cd src/GanhoCapital

# Restaure as dependências
dotnet restore

# Compile o projeto
dotnet build
```

#### **2. Execução Direta**
```bash
cd src/GanhoCapital
dotnet run
```

#### **3. Execução com Arquivo Executável**
```bash
# Publicar o projeto
dotnet publish -c Release -o ./publish

# Executar o binário
./publish/GanhoCapital
```

### Execução em Container (Docker)

A aplicação pode ser executada em container Docker usando o arquivo `Dockerfile` incluído no projeto.

#### **1. Build da Imagem Docker**

```bash
# Navegue até o diretório que contém o Dockerfile
cd src/GanhoCapital

# Construa a imagem Docker
docker build -t ganho-capital .
```

#### **2. Execução do Container**

**Execução com entrada via pipe:**
```bash
echo '[{"operation":"buy", "unit-cost":10.00, "quantity": 100}, {"operation":"sell", "unit-cost":15.00, "quantity": 50}]' | docker run --rm -i ganho-capital
```

**Execução interativa:**
```bash
docker run --rm -it ganho-capital
```

**Execução com arquivo de entrada:**
```bash
cat input.json | docker run --rm -i ganho-capital
```

### Como Usar

A aplicação lê dados JSON da entrada padrão (`stdin`) e escreve os resultados na saída padrão (`stdout`).

**Formato de Entrada:**
```json
[{"operation":"buy", "unit-cost":10.00, "quantity": 100}, {"operation":"sell", "unit-cost":15.00, "quantity": 50}]
```

**Exemplo de Uso (Local):**
```bash
echo '[{"operation":"buy", "unit-cost":10.00, "quantity": 100}, {"operation":"sell", "unit-cost":15.00, "quantity": 50}]' | dotnet run
```

**Formato de Saída:**
```json
[{"tax": 0.0},{"tax": 0.0}]
```

## Como Executar os Testes

### Execução Local

#### **Executar Todos os Testes**

```bash
cd tests/GanhoCapital.Tests
dotnet test
```

#### **Executar Testes com Saída Detalhada**

```bash
dotnet test --verbosity normal
```

### Execução em Container (Docker)

Para executar todos os testes em um ambiente containerizado isolado:

#### **1. Build da Imagem de Testes**

```bash
# Navegue até o diretório raiz do projeto
cd /caminho/para/GanhoCapital

# Construa a imagem de testes
docker build -t ganho-capital-tests -f tests/Dockerfile .
```

#### **2. Executar Todos os Testes**

```bash
docker run --rm ganho-capital-tests
```

## Exemplo Prático

**Execução Local:**
```bash
# Entrada
echo '[
  {"operation":"buy", "unit-cost":10.00, "quantity": 1000},
  {"operation":"sell", "unit-cost":15.00, "quantity": 500},
  {"operation":"sell", "unit-cost":25.00, "quantity": 500}
]' | dotnet run

# Saída esperada
[{"tax":0.00},{"tax":0.00},{"tax":1500.00}]
```

**Execução em Container:**
```bash
# Entrada
echo '[
  {"operation":"buy", "unit-cost":10.00, "quantity": 1000},
  {"operation":"sell", "unit-cost":15.00, "quantity": 500},
  {"operation":"sell", "unit-cost":25.00, "quantity": 500}
]' | docker run --rm -i ganho-capital

# Saída esperada
[{"tax": 0.0},{"tax": 0.0},{"tax": 1500.0}]
```

**Explicação:**
1. Compra 1000 ações a R$ 10,00 → Tax: R$ 0,00 (operação de compra)
2. Vende 500 ações a R$ 15,00 (R$ 7.500 total) → Tax: R$ 0,00 (abaixo de R$ 20.000)
3. Vende 500 ações a R$ 25,00 (R$ 12.500 total) → Tax: R$ 1.500,00 (ganho de R$ 7.500 × 20%)

## Estrutura do Projeto

```
GanhoCapital/
├── src/GanhoCapital/                    # Código fonte principal
│   ├── Domain/                          # Modelos de domínio
│   ├── Services/                        # Serviços da aplicação
│   ├── JsonHelpers/                     # Utilitários JSON  
│   ├── Program.cs                       # Ponto de entrada
│   ├── ServiceProviderFactory.cs        # Configuração DI
│   ├── Dockerfile                       # Configuração Docker para aplicação
│   └── GanhoCapital.csproj              # Arquivo de projeto
├── tests/                               # Projetos de teste
│   ├── Dockerfile                       # Configuração Docker para testes
│   ├── GanhoCapital.Tests/              # Testes unitários
│   │   └── OperationGainTests.cs        # Testes da lógica de negócio
│   └── GanhoCapital.Integration.Tests/  # Testes de integração
│       └── IntegrationTests.cs          # Testes end-to-end
├── Docs/                                # Documentação
└── README.md                            # Este arquivo
```