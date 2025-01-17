﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


public class Bitcoin
{
    [JsonProperty("mnemonic")]
    public string mnemonic { get; set; }


    [JsonProperty("xpub")]
    public string xpub { get; set; }


    [JsonProperty("address")]
    public string address { get; set; }


    [JsonProperty("index")]
    public string index { get; set; }


    [JsonProperty("hash")]
    public string hash { get; set; }


    [JsonProperty("chain")]
    public string chain { get; set; }


    [JsonProperty("blocks")]
    public int blocks { get; set; }


    [JsonProperty("headers")]
    public int headers { get; set; }


    [JsonProperty("bestblockhash")]
    public string bestblockhash { get; set; }



    [JsonProperty("difficulty")]
    public float difficulty { get; set; }



    [JsonProperty("height")]
    public int height { get; set; }



    [JsonProperty("depth")]
    public int depth { get; set; }



    [JsonProperty("version")]
    public int version { get; set; }



    [JsonProperty("prevBlock")]
    public string prevBlock { get; set; }



    [JsonProperty("merkleroot")]
    public string merkleRoot { get; set; }



    [JsonProperty("time")]
    public int time { get; set; }



    [JsonProperty("bits")]
    public int bits { get; set; }



    [JsonProperty("nonce")]
    public int nonce { get; set; }



    [JsonProperty("txs")]
    public Tx[] txs { get; set; }


    [JsonProperty("witnessHash")]
    public string witnessHash { get; set; }



    [JsonProperty("fee")]
    public int fee { get; set; }



    [JsonProperty("rate")]
    public int rate { get; set; }



    [JsonProperty("mtime")]
    public int mtime { get; set; }



    [JsonProperty("block")]
    public string block { get; set; }



    [JsonProperty("inputs")]
    public Input[] inputs { get; set; }



    [JsonProperty("outputs")]
    public Output[] outputs { get; set; }



    [JsonProperty("locktime")]
    public int locktime { get; set; }



    [JsonProperty("incoming")]
    public string incoming { get; set; }



    [JsonProperty("outgoing")]
    public string outgoing { get; set; }



    [JsonProperty("value")]
    public int value { get; set; }



    [JsonProperty("script")]
    public string script { get; set; }


    [JsonProperty("coinbase")]
    public bool coinbase { get; set; }



    [JsonProperty("fromAddress")]
    public Fromaddress[] fromAddress { get; set; }



    [JsonProperty("to")]
    public To[] to { get; set; }



    [JsonProperty("txData")]
    public string txData { get; set; }



    [JsonProperty("signatureId")]
    public string signatureId { get; set; }  
  
    
   
}



public class Tx
{
}




public class Fromaddress
{
}

public class To
{
}



public class Input
{
}

public class Output
{
}


