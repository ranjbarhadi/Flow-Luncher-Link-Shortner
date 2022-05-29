using System;
using System.Collections.Generic;
using Flow.Launcher.Plugin;

namespace Flow.Launcher.Plugin.minify
{
    public class minify : IPlugin
    {
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        // when the user input a url we want to return a minifued link
        public List<Result> Query(Query query)
        {
            // get the url from query and minify it using bitly.com
            var url = query.Search;
            var minifiedUrl = MinifyUrl(url);
            return new List<Result>();
        }

        // minify the url using bitly.com
        private string MinifyUrl(string url)
        {
            // connect to bitly.com and get the minified url
            var bitly = new Bitly();
            var minifiedUrl = bitly.Shorten(url);
            return minifiedUrl;
        }
    }

    public class Bitly
    {
        // get the minified url using bitly.com
        public string Shorten(string url)
        {
            // open http web request to bitly api
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://api.bitly.com/v3/shorten?login=o_6d0q0jq8&apiKey=R_c8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8f8&longUrl=" + url);
            request.Method = "GET";
            request.ContentType = "application/json";

            // get the result
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            
            // find the url in response 
            var start = responseString.IndexOf("\"url\": \"") + 8;
            var end = responseString.IndexOf("\"", start);
            var minifiedUrl = responseString.Substring(start, end - start);
            return minifiedUrl;
        }
    }

}