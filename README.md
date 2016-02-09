# WUnderground.Client
A .NET portable client library for the WeatherUnderground APIs

```csharp
	WUnderground.Config.ApiKey = "<your WeatherUnderground API Key >";
	Task<WeatherResponse> t = WUndergroundClient.getConditionsForCityAsync("Utrecht", "NL");
	t.Wait();
	//From here on, t.Result is what you need :)
```
