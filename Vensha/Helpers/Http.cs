/*
                                      /;    ;\
                                  __  \\____//
                                 /{_\_/   `'\____
                                 \___   (o)  (o  }
      _____________________________/          :--'
  ,-,'`@@@@@@@@       @@@@@@         \_    `__\
 ;:(  @@@@@@@@@        @@@             \___(o'o)
 :: )  @@@@          @@@@@@        ,'@@(  `===='
 :: : @@@@@:          @@@@         `@@@:
 :: \  @@@@@:       @@@@@@@)    (  '@@@'
 ;; /\      /`,    @@@@@@@@@\   :@@@@@)
 ::/  )    {_----------------:  :~`,~~;
;;'`; :   )                  :  / `; ;
;;;; : :   ;                  :  ;  ; :
`'`' / :  :                   :  :  : :
   )_ \__;      ";"          :_ ;  \_\       `,','
   :__\  \    * `,'*         \  \  :  \   *  8`;'*
       `^'     \ :/           `^'  `-^-'   \v/ :   
*/


using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vensha.Helpers.Http
{
    public static class Http
    {
        private static HttpClient _Client = new HttpClient();

        public static async Task<HttpResponse> Get(string url)
        {
            HttpResponse output = null;
            HttpResponseMessage result = null;
            try
            {
                result = await _Client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                output = new HttpResponse(url, result.Content, result.IsSuccessStatusCode, (int)result.StatusCode);
            }
            catch (Exception error)
            {
                output = new HttpResponse(url, result.Content, false, (int)result.StatusCode, error.Message);
            }
            return output;
        }

        public static async Task<HttpResponse> Post(string url, Dictionary<string, string> values)
        {
            HttpResponse output = null;
            HttpResponseMessage result = null;
            try
            {
                result = await _Client.PostAsync(url, new FormUrlEncodedContent(values));
                result.EnsureSuccessStatusCode();
                output = new HttpResponse(url, result.Content, result.IsSuccessStatusCode, (int)result.StatusCode);
            }
            catch (Exception error)
            {
                output = new HttpResponse(url, result.Content, false, (int)result.StatusCode, error.Message);
            }
            return output;
        }
    }

    public class HttpResponse
    {
        public readonly bool Ok;
        public readonly string Url;
        public readonly int StatusCode;
        public readonly string? Error;
        public readonly HttpContent Content;

        public HttpResponse(string url, HttpContent content, bool ok, int statusCode, string error = null)
        {
            this.Ok = ok;
            this.Url = url;
            this.StatusCode = statusCode;
            this.Error = error;
            this.Content = content;
        }
    }
}