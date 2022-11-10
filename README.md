# About 
Like shodan but better for recon and osint. the censys bots scrape the web looking for all devices even beind protected services like cloudflare then store then within their database.
This allows you to check your host and see if your backend ipaddress is exposed to the public. 

###### Usage:
```cs
        Task.Run(() => API.Search(hostBox.Text)); 
```

###### Requirements:
```
https://www.nuget.org/packages/Newtonsoft.Json/
https://www.nuget.org/packages/System.Net.Http/
```
