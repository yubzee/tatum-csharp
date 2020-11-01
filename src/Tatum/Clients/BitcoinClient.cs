﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tatum.Blockchain;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public partial class BitcoinClient : IBitcoinClient
    {
        private readonly IBitcoinApi bitcoinApi;

        public BitcoinClient(string apiBaseUrl, string xApiKey)
        {
            bitcoinApi = RestClientFactory.Create<IBitcoinApi>(apiBaseUrl, xApiKey);
        }

        Task<BitcoinInfo> IBitcoinClient.GetBlockchainInfo()
        {
            return bitcoinApi.GetBlockchainInfo();
        }

        Task<BitcoinBlock> IBitcoinClient.GetBlock(string hash)
        {
            return bitcoinApi.GetBlock(hash);
        }

        Task<BlockHash> IBitcoinClient.GetBlockHash(int blockHeight)
        {
            return bitcoinApi.GetBlockHash(blockHeight);
        }

        Task<BitcoinUtxo> IBitcoinClient.GetUtxo(string txHash, int txOutputIndex)
        {
            return bitcoinApi.GetUtxo(txHash, txOutputIndex);
        }

        Task<List<BitcoinTx>> IBitcoinClient.GetTxForAccount(string address, int pageSize, int offset)
        {
            return bitcoinApi.GetTxForAccount(address, pageSize, offset);
        }

        Task<BitcoinTx> IBitcoinClient.GetTransaction(string hash)
        {
            return bitcoinApi.GetTransaction(hash);
        }
    }
}