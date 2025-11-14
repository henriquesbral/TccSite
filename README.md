# TccSite - Sistema de Monitoramento de Nível de Rios

Este é um sistema completo para monitoramento do nível de rios utilizando **ESP32-CAM**, **Python + OpenCV**, **ASP.NET Core MVC** e **Highcharts**.

## 🚀 Começando

### Pré-requisitos

- .NET 6.0 ou superior
- Python 3.8+
- OpenCV para Python (`pip install opencv-python`)
- SQL Server
- ESP32-CAM

### Instalação

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd TccSite
```

2. **Configure o banco de dados**
- Atualize a connection string no `appsettings.json`
- Execute as migrações do Entity Framework

3. **Configure o Python**
```bash
cd Python
pip install -r requirements.txt
```

4. **Calibração do sistema (executar uma única vez)**
```bash
python calibrar.py
```
*Siga as instruções para selecionar a área do copo/régua na imagem*

5. **Execute a aplicação**
```bash
dotnet run
```

## 📋 Funcionalidades

### 🖥️ Painel Web (TccSite)
- **Dashboard interativo** com gráficos Highcharts
- **Monitoramento em tempo real** do nível do rio
- **Sistema de alertas** automáticos
- **Gestão de usuários** e permissões
- **Relatórios** históricos
- **Clima em tempo real** integrado

### 🔌 API Backend (TCCAPIESP32)
- **Recepção de imagens** do ESP32-CAM
- **Processamento automático** via Python/OpenCV
- **Armazenamento seguro** de dados e imagens
- **Geração de alertas** baseados em limites

### 🐍 Processamento de Imagem
- **Calibração precisa** usando régua de 10cm
- **Detecção robusta** da linha d'água
- **Processamento em tempo real**
- **Configuração flexível**

### 📷 ESP32-CAM
- **Captura periódica** automática
- **Envio seguro** para a API
- **Configuração via Wi-Fi**

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core MVC, Entity Framework Core
- **Frontend**: Bootstrap 5.3, jQuery, Highcharts, SweetAlert2
- **Processamento**: Python 3, OpenCV 4
- **Hardware**: ESP32-CAM
- **Banco de Dados**: SQL Server
- **APIs**: wttr.in (clima)

## 📁 Estrutura do Projeto

```
/TccSite
│ README.md
│ TccSite.sln
│
├─ /TccSite.Application
│ Serviços da aplicação Web
│
├─ /TccSite.Domain
│ Entidades, modelos e regras comuns
│
├─ /TccSite.Infrastructure
│ Acesso a dados, repositórios, EF Core
│
├─ /TccSite
│ Projeto Web MVC (Views, Controllers, JS, CSS)
│
├─ /TCCAPIESP32.Application
│ Serviços usados pela API
│
├─ /TCCAPIESP32.Domain
│ Entidades do armazenamento de imagens e alertas
│
├─ /TCCAPIESP32.Infrastructure
│ Migrações, DB Context, repositórios
│
├─ /Python
│ calibrar.py → calibração da área do copo (10cm)
│ processar.py → processamento do nível em cm
│ calibracao.json → arquivo gerado pelo calibrar.py
│
└─ /TccEsp32CamAPI
```

## 🔧 Configuração

### Configuração do Python
No `appsettings.json` da API:

```json
"PythonSettings": {
  "InterpreterPath": "C:/Python312/python.exe",
  "ProcessarScriptPath": "C:/caminho/para/processar.py"
}
```

### Configuração do ESP32-CAM
- Configure a rede Wi-Fi
- Defina a URL da API: `POST /api/Camera/Capturar`
- Ajuste o intervalo de captura

## 🧪 Testes

### Testes Recomendados
1. **Calibração**: Execute `calibrar.py` e valide a área selecionada
2. **Processamento**: Teste `processar.py` com imagens reais
3. **API**: Verifique recebimento de imagens do ESP32
4. **Dashboard**: Confirme atualização em tempo real
5. **Alertas**: Teste geração de alertas automáticos

### Teste Manual do Processamento
```bash
cd Python
python processar.py imagem_teste.jpg
```

## 📊 Uso do Sistema

1. **Acesso Web**: `https://localhost:5001`
2. **Login**: Use as credenciais administrativas
3. **Dashboard**: Visualize dados em tempo real
4. **Monitoramento**: O sistema processa automaticamente as imagens recebidas
5. **Alertas**: Configure limites no painel administrativo

## 🐛 Solução de Problemas

### Problemas Comuns

1. **Erro de calibração**
   - Verifique se a régua/copo está visível
   - Execute `calibrar.py` novamente

2. **API não recebe imagens**
   - Verifique a conexão do ESP32-CAM
   - Confirme a URL da API

3. **Processamento retorna erro**
   - Valide o caminho do Python no `appsettings.json`
   - Verifique permissões de arquivo

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request


## 👥 Autores

- **Carlos Sobral** - *Desenvolvimento do projeto e arquitetura* - [henriquesbral](https://github.com/henriquesbral)

## 🙏 Agradecimentos

- Equipe orientadora do TCC
- Contribuidores do OpenCV
- Comunidade .NET e Python
