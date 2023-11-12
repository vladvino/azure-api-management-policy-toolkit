using Mielek.Expressions.Context;

namespace Mielek.Testing.Expressions.Mocks;

public class MockUrl : IUrl
{
    public string Host { get; set; } = "example.com";
    public string Path { get; set; } = "/mock/op";
    public string Port { get; set; } = "443";
    public IReadOnlyDictionary<string, string[]> Query => MockQuery;
    public Dictionary<string, string[]> MockQuery { get; set; } = new Dictionary<string, string[]> { { "p1", new[] { "v1" } }, { "p2", new[] { "v1", "v2" } } };
    public string QueryString { get; set; } = "p1=v1&p2=v1&p2=v2";
    public string Scheme { get; set; } = "https";
}