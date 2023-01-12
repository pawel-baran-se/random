namespace Client.Static
{
	internal static class APIEndpoints
	{
#if DEBUG
		internal const string ServerBaseUrl = "https://localhost:7025";
#else
		internal const string ServerBaseUrl = "https://localhost:7025d";
#endif
		internal readonly static string s_categories = $"{ServerBaseUrl}/api/categories";
	}
}
