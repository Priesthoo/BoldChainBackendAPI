this is an experimental blockChain prototype for secure email verification . still under construction.
more features to be added soon 
# 🔐 BoldChain Backend API

**BoldChain** is a decentralized secure messaging and identity platform built with ASP.NET Core, Ethereum smart contracts (via Nethereum), and IPFS. It provides robust cryptographic communication, domain-based trust verification, blockchain-integrated identity registry, and real-time messaging via SignalR.

---

## 🚀 Features

- ✅ User registration with public/private key generation
- ✅ JWT & refresh token authentication
- ✅ Ethereum smart contract integration using Nethereum
- ✅ TrustStamp, Identity Registry, Domain Verifier services
- ✅ Message encryption, signature, and IPFS storage
- ✅ SignalR notifications for real-time message delivery
- ✅ Plugin architecture with support for enterprise integrations
- ✅ Swagger (OpenAPI) for full API documentation
- ✅ Modular service-based architecture with dependency injection
- ✅ Logging, validation, health checks, and telemetry support

---

## 🧱 Technologies

| Layer                  | Tech Stack                                      |
|------------------------|--------------------------------------------------|
| Backend Framework      | ASP.NET Core 8                                   |
| Blockchain Integration | Nethereum, Solidity, Ganache                     |
| Identity & Auth        | ASP.NET Identity, JWT, ECDSA wallet signatures   |
| Data Storage           | EF Core + SQL Server                             |
| Message Storage        | IPFS via Pinata                                  |
| Real-Time              | SignalR                                          |
| API Docs               | Swagger (Swashbuckle)                            |
| Key Management         | Azure Key Vault (RSA/ECDSA key storage)          |
| Logging                | Serilog + Seq                                    |

---

## ⚙️ Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Ganache](https://trufflesuite.com/ganache/) (or Infura RPC URL)
- [Node.js](https://nodejs.org/) (for frontend, if applicable)
- [IPFS/Pinata API keys](https://pinata.cloud/)
- [Azure Key Vault](https://learn.microsoft.com/en-us/azure/key-vault/general/basic-concepts)

### Local Setup

1. **Clone Repository**

```bash
git clone https://github.com/your-org/boldchain-backend.git
cd boldchain-backend
