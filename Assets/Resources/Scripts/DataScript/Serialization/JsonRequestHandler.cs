using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using Newtonsoft.Json;



public class JsonRequestHandler
{
  private readonly string url;
  private string json;

  public JsonRequestHandler(string url)
  {
    this.url = url;
  }

  private string Result { get; set; }

  public JsonRequestHandler Feed(string _json)
  {
    this.json = _json;
    return this;
  }

  public JsonRequestHandler Fetch()
  {
    var httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
    httpWebRequest.ContentType = "application/json";
    httpWebRequest.Method = "POST";

    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    {
      streamWriter.Write(json);
    }

    var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
    using (var streamReader = new StreamReader(httpResponse.GetResponseStream() 
                                               ?? throw new Exception("JsonRequestHandler:: Something goes wrong while receiving response from server")))
    {
      Result = streamReader.ReadToEnd();
    }

    return this;
  }

  public string Get()
  {
    return Result;
  }
}
