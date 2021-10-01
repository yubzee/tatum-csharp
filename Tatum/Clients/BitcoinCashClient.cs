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

/// <summary>
/// Summary description for BitcoinCashClient
/// </summary>
/// 
namespace Tatum
{
    public class BitcoinCashClient
    {
        private readonly string _privateKey;
        public BitcoinCashClient(string privateKey)
        {
            _privateKey = privateKey;
        }


        public async Task<BitcoinCash> GenerateBitcoinCashWallet(string mnemonic)
        {


            var stringResult = await GetSecureRequest($"wallet?mnemonic=" + mnemonic);

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }





        public async Task<BitcoinCash> GetBlockchainInfo()
        {


            var stringResult = await GetSecureRequest($"info");

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }

        public async Task<BitcoinCash> GetBitcoinCashBlockHash(int i)
        {


            var stringResult = await GetSecureRequest($"block/hash/{i}");

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<BitcoinCash> GetBitcoinCashBlockByHash(string hash)
        {


            var stringResult = await GetSecureRequest($"block/{hash}");

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<BitcoinCash> GetBitcoinCashTransactionByHash(string hash)
        {


            var stringResult = await GetSecureRequest($"transaction/{hash}");

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<List<BitcoinCash>> GetBitcoinCashTransactionByAddress(string address)
        {


            var stringResult = await GetSecureRequest($"transaction/{address}");

            var result = JsonConvert.DeserializeObject<List<BitcoinCash>>(stringResult);

            return result;
        }

        public async Task<BitcoinCash> GenerateBitcoinCashDepositAddressFromPublicKey(string xpub, int index)
        {


            var stringResult = await GetSecureRequest($"address/{xpub}/{index}");

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }

        public async Task<BitcoinCash> GenerateBitcoinCashPrivateKey(string index, int mnemonic)
        {

            string parameters = "{\"index\":" + "\"" + index + "" + "\",\"mnemonic\":" + "\"" + mnemonic + "" + "\"}";

            var stringResult = await PostSecureRequest($"wallet/priv", parameters);

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<BitcoinCash> SendBitcoinCashTransactionFromUTXO(string txHash, int index, string privateKey, string toAddress, string value)
        {

            string parameters = "{\"fromUTXO\":[{\"txHash\":" + "\"" + txHash + "" + "\",\"index\":" + "\"" + index + "" + "\",\"privateKey\":" + "\"" + privateKey + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";


            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<BitcoinCash> SendBitcoinCashTransactionFromUTXOKMS(string txHash, int index, string signatureId, string toAddress, string value)
        {

            string parameters = "{\"fromUTXO\":[{\"txHash\":" + "\"" + txHash + "" + "\",\"index\":" + "\"" + index + "" + "\",\"signatureId\":" + "\"" + signatureId + "" + "\"}],\"to\":[{\"address\":" + "\"" + toAddress + "" + "\",\"value\":" + "\"" + value + "" + "\"}]}";


            var stringResult = await PostSecureRequest($"transaction", parameters);

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }


        public async Task<BitcoinCash> BroadcastSignedBitcoinCashTransaction(string txData, string signatureId)
        {

            string parameters = "{\"txData\":" + "\"" + txData + "" + "\",\"signatureId\":" + "\"" + signatureId + "" + "\"}";


            var stringResult = await PostSecureRequest($"broadcast", parameters);

            var result = JsonConvert.DeserializeObject<BitcoinCash>(stringResult);

            return result;
        }





















        private async Task<string> GetSecureRequest(string path, Dictionary<string, string> paramaters = null)
        {
            var baseUrl = "https://api-eu1.tatum.io/v3/bcash";

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

            var baseUrl = "https://api-eu1.tatum.io/v3/bcash";

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

            var baseUrl = "https://api-eu1.tatum.io/v3/bcash";

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
            var baseUrl = "https://api-eu1.tatum.io/v3/bcash";

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