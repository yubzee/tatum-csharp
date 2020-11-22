﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tatum.Blockchain;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public class XlmClient : IXlmClient
    {
        private readonly IXlmApi xlmApi;

        public XlmClient(string apiBaseUrl, string xApiKey)
        {
            xlmApi = RestClientFactory.Create<IXlmApi>(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IXlmClient.Broadcast(BroadcastRequest request)
        {
            return xlmApi.Broadcast(request);
        }

        Task<XlmAccountInfo> IXlmClient.GetAccountInfo(string accountAddress)
        {
            return xlmApi.GetAccountInfo(accountAddress);
        }

        Task<List<XlmTx>> IXlmClient.GetAccountTransactions(string address)
        {
            return xlmApi.GetAccountTransactions(address);
        }

        Task<XlmInfo> IXlmClient.GetBlockchainInfo()
        {
            return xlmApi.GetBlockchainInfo();
        }

        Task<long> IXlmClient.GetFee()
        {
            return xlmApi.GetFee();
        }

        Task<List<XlmTx>> IXlmClient.GetLedger(string sequence)
        {
            return xlmApi.GetLedger(sequence);
        }

        Task<XlmTx> IXlmClient.GetLedgerTransaction(string sequence)
        {
            return xlmApi.GetLedgerTransaction(sequence);
        }

        Task<XlmTx> IXlmClient.GetTransaction(string hash)
        {
            return xlmApi.GetTransaction(hash);
        }
    }
}