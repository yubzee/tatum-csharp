﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Http;
using System.Security;
using NBitcoin;
using Tatum.Blockchain;

/// <summary>
/// Summary description for LitecoinClient
/// </summary>
/// 
namespace Tatum
{
    public class LitecoinClient:ILitecoinClient
    {
        private readonly string _privateKey;
        public LitecoinClient(string privateKey)
        {
            _privateKey = privateKey;
        }



        Wallets ILitecoinClient.CreateWallet(string mnemonic, bool testnet)
        {
            var xPub = new Mnemonic(mnemonic)
                .DeriveExtKey()
                .Derive(new KeyPath(testnet ? Constants.TestKeyDerivationPath : Constants.LtcKeyDerivationPath))
                .Neuter();

            return new Wallets
            {
                Mnemonic = mnemonic,
                XPub = xPub.ToString(testnet ? LitecoinTatum.Instance.Testnet : LitecoinTatum.Instance.Mainnet)
            };
        }

        string ILitecoinClient.GeneratePrivateKey(string mnemonic, int index, bool testnet)
        {
            return new Mnemonic(mnemonic)
                .DeriveExtKey()
                .Derive(new KeyPath(testnet ? Constants.TestKeyDerivationPath : Constants.LtcKeyDerivationPath))
                .Derive(Convert.ToUInt32(index))
                .PrivateKey
                .GetWif(testnet ? LitecoinTatum.Instance.Testnet : LitecoinTatum.Instance.Mainnet)
                .ToString();
        }

        string ILitecoinClient.GenerateAddress(string xPubString, int index, bool testnet)
        {
            return ExtPubKey.Parse(xPubString, testnet ? LitecoinTatum.Instance.Testnet : LitecoinTatum.Instance.Mainnet)
                .Derive(Convert.ToUInt32(index))
                .PubKey
                .GetAddress(ScriptPubKeyType.Legacy, testnet ? LitecoinTatum.Instance.Testnet : LitecoinTatum.Instance.Mainnet)
                .ToString();
        }



        public async Task<Litecoin> GetLitecoinBlockchainInfo()
        {


            var stringResult = await GetSecureRequest($"info");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }



        public async Task<Litecoin> GetLitecoinBlockHash(int i)
        {


            var stringResult = await GetSecureRequest($"block/hash/{i}");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }



        public async Task<Litecoin> GetLitecoinBlockByHash(string hash)
        {


            var stringResult = await GetSecureRequest($"block/{hash}");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }

        public async Task<Litecoin> GetLitecoinTransactionByHash(string hash)
        {


            var stringResult = await GetSecureRequest($"transaction/{hash}");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }


        public async Task<List<Litecoin>> GetMempoolTransactions()
        {


            var stringResult = await GetSecureRequest($"mempool");

            var result = JsonConvert.DeserializeObject<List<Litecoin>>(stringResult);

            return result;
        }

        public async Task<List<Litecoin>> GetLitecoinTransactionsByAddress(string address, int pageSize = 50, int offset = 0)
        {


            var stringResult = await GetSecureRequest($"transaction/address/{address}?pageSize=10&offset=0");

            var result = JsonConvert.DeserializeObject<List<Litecoin>>(stringResult);

            return result;
        }


        public async Task<Litecoin> GetLitecoinBalanceOfAddress(string address)
        {


            var stringResult = await GetSecureRequest($"address/balance/{address}");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }


        public async Task<Litecoin> GetLitecoinUTXOTransaction(string hash, int index)
        {


            var stringResult = await GetSecureRequest($"utxo/{hash}/{index}");

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }



      




        public async Task<Litecoin> SendLitecoinTransactionAddress(string fromaddress, string privateKey, string toAddress, string value)
        {

            string parameters = "{\"fromAddress\":[{\"address\":" + "\"" + fromaddress + "" + "\",\"privateKey\":" + "\"" + privateKey + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";

            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }


        public async Task<Litecoin> SendLitecoinTransactionAddressKMS(string fromaddress, string signatureId, string toAddress, string value)
        {

            string parameters = "{\"fromAddress\":[{\"address\":" + "\"" + fromaddress + "" + "\",\"signatureId\":" + "\"" + signatureId + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";

            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }



        public async Task<Litecoin> SendLitecoinTransactionUTXO(string txHash, int index, string privateKey, string toAddress, string value)
        {

            string parameters = "{\"fromUTXO\":[{\"txHash\":" + "\"" + txHash + "" + "\",\"index\":" + "\"" + index + "" + "\",\"privateKey\":" + "\"" + privateKey + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";

            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }




        public async Task<Litecoin> SendLitecoinTransactionUTXOKMS(string txHash, int index, string signatureId, string toAddress, string value)
        {

            string parameters = "{\"fromUTXO\":[{\"txHash\":" + "\"" + txHash + "" + "\",\"index\":" + "\"" + index + "" + "\",\"signatureId\":" + "\"" + signatureId + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";

            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }



        public async Task<Litecoin> BroadcastSignedLitecoinTransaction(string txData, string signatureId)
        {

            string parameters = "{\"txData\":" + "\"" + txData + "" + "\",\"signatureId\":" + "\"" + signatureId + "" + "\"}";

            var stringResult = await PostSecureRequest($"broadcast", parameters);

            var result = JsonConvert.DeserializeObject<Litecoin>(stringResult);

            return result;
        }















        private async Task<string> GetSecureRequest(string path, Dictionary<string, string> paramaters = null)
        {
            var baseUrl = "https://api-eu1.tatum.io/v3/litecoin";

            baseUrl = $"{baseUrl}/{path}";


            HttpWebRequest reqc = (HttpWebRequest)WebRequest.Create(baseUrl);

            reqc.Method = "GET";
            reqc.Headers.Add("x-api-key", _privateKey);
            reqc.Credentials = CredentialCache.DefaultCredentials;
            reqc.ContentType = "application/json";




            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)(await reqc.GetResponseAsync());
                string json;
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }

                return json;
            }
            catch (Exception ex)
            {
                throw new SecurityException("Bad credentials", ex);
            }
        }


        private async Task<string> PostSecureRequest(string path, string parameters)
        {

            var baseUrl = "https://api-eu1.tatum.io/v3/litecoin";

            baseUrl = $"{baseUrl}/{path}";


            HttpWebRequest reqc = (HttpWebRequest)WebRequest.Create(baseUrl);

            reqc.Method = "POST";
            reqc.Headers.Add("x-api-key", _privateKey);
            reqc.Credentials = CredentialCache.DefaultCredentials;
            reqc.ContentType = "multipart/form-data";

            using (var streamWriter = new StreamWriter(reqc.GetRequestStream()))
            {

                streamWriter.Write(parameters);
            }


            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)(await reqc.GetResponseAsync());
                string json;
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }

                return json;
            }
            catch (Exception ex)
            {
                throw new SecurityException("Bad credentials", ex);
            }

        }


        private async Task<string> PUTSecureRequest(string path, string parameters)
        {

            var baseUrl = "https://api-eu1.tatum.io/v3/litecoin";

            baseUrl = $"{baseUrl}/{path}";


            HttpWebRequest reqc = (HttpWebRequest)WebRequest.Create(baseUrl);

            reqc.Method = "PUT";
            reqc.Headers.Add("x-api-key", _privateKey);
            reqc.Credentials = CredentialCache.DefaultCredentials;
            reqc.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(reqc.GetRequestStream()))
            {

                streamWriter.Write(parameters);
            }


            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)(await reqc.GetResponseAsync());
                string json;
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }

                return json;
            }
            catch (Exception ex)
            {
                throw new SecurityException("Bad credentials", ex);
            }

        }

        private async Task<string> DeleteSecureRequest(string path, Dictionary<string, string> paramaters = null)
        {
            var baseUrl = "https://api-eu1.tatum.io/v3/litecoin";

            baseUrl = $"{baseUrl}/{path}";


            HttpWebRequest reqc = (HttpWebRequest)WebRequest.Create(baseUrl);

            reqc.Method = "DELETE";
            reqc.Headers.Add("x-api-key", _privateKey);
            reqc.Credentials = CredentialCache.DefaultCredentials;
            reqc.ContentType = "application/json";




            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)(await reqc.GetResponseAsync());
                string json;
                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    json = new StreamReader(responseStream).ReadToEnd();
                }

                return json;
            }
            catch (Exception ex)
            {
                throw new SecurityException("Bad credentials", ex);
            }
        }





















    }
}