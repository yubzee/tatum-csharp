using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


/// <summary>
/// Summary description for IBitcoinCashClient
/// </summary>
namespace Tatum
{
    public interface IBitcoinCashClient
    {

        Wallets CreateWallet( string mnemonic, bool testnet);
        String GeneratePrivateKey(string mnemonic, int index, bool testnet);
        String GenerateAddress(string xPubString, int index, bool testnet);

        Task<BitcoinCash> GetBlockchainInfo();
        Task<BitcoinCash> GetBitcoinCashBlockHash(int i);
        Task<BitcoinCash> GetBitcoinCashBlockByHash(string hash);
        Task<BitcoinCash> GetBitcoinCashTransactionByHash(string hash);
        Task<List<BitcoinCash>> GetBitcoinCashTransactionByAddress(string address);

        
        Task<BitcoinCash> SendBitcoinCashTransactionFromUTXO(string txHash, int index, string privateKey, string toAddress, string value);
        Task<BitcoinCash> SendBitcoinCashTransactionFromUTXOKMS(string txHash, int index, string signatureId, string toAddress, string value);

        Task<BitcoinCash> BroadcastSignedBitcoinCashTransaction(string txData, string signatureId);

    }
}