﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NHSD.GPITF.BuyingCatalog.Datastore.CRM.Interfaces;
using RestSharp;
using System;
using System.Configuration;

namespace NHSD.GPITF.BuyingCatalog.Datastore.CRM
{
  public sealed class RestClientFactory : IRestClientFactory
  {
    private static readonly object _lockObject = new object();

    private readonly string ApiUri;
    private readonly string AccessTokenUri;

    private readonly string ClientId;
    private readonly string ClientSecret;

    private AccessToken _cachedAccessToken;

    public RestClientFactory(IConfiguration config)
    {
      // read out of user secret or environment
      ApiUri = Environment.GetEnvironmentVariable("CRM_APIURI") ?? config["CRM:ApiUri"];
      AccessTokenUri = Environment.GetEnvironmentVariable("CRM_ACCESSTOKENURI") ?? config["CRM:AccessTokenUri"];

      ClientId = Environment.GetEnvironmentVariable("CRM_CLIENTID") ?? config["CRM:ClientId"];
      ClientSecret = Environment.GetEnvironmentVariable("CRM_CLIENTSECRET") ?? config["CRM:ClientSecret"];

      if (string.IsNullOrWhiteSpace(ApiUri) ||
        string.IsNullOrWhiteSpace(AccessTokenUri) ||
        string.IsNullOrWhiteSpace(ClientId) ||
        string.IsNullOrWhiteSpace(ClientSecret)
        )
      {
        throw new ConfigurationErrorsException("Missing CRM configuration - check UserSecrets or environment variables");
      }

      _cachedAccessToken = GetAccessToken();
    }

    public IRestClient GetRestClient()
    {
      return new RestClient(ApiUri);
    }

    public AccessToken GetAccessToken()
    {
      lock (_lockObject)
      {
        _cachedAccessToken = _cachedAccessToken ?? CreateAccessToken();

        var expiry = _cachedAccessToken.CreatedOn.AddSeconds(_cachedAccessToken.expires_in);
        if (DateTime.UtcNow >= expiry)
        {
          _cachedAccessToken = CreateAccessToken();
        }

        return _cachedAccessToken;
      }
    }

    private AccessToken CreateAccessToken()
    {
      var restclient = new RestClient(AccessTokenUri);
      var request = new RestRequest() { Method = Method.POST };
      request.AddHeader("Accept", "application/json");
      request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
      request.AddParameter("client_id", ClientId);
      request.AddParameter("client_secret", ClientSecret);
      request.AddParameter("grant_type", "client_credentials");
      var resp = restclient.Execute(request);
      var responseJson = resp.Content;
      var token = JsonConvert.DeserializeObject<AccessToken>(responseJson);

      return token.access_token.Length > 0 ? token : null;
    }
  }
}
